﻿using System.IO;
using System.Text;
using System.Xml;
using System.Globalization;
using System;
using System.Collections.Generic;

namespace Servers
{
    /// <summary>
    /// Internal class containing global static objects.
    /// </summary>
    public static class HTTPInternalObjects
    {
        /// <summary>
        /// XSL to use for directory listings. This will be converted to UTF-8, whitespace-optimised and cached before being output.
        /// This is the file that is returned at the URL /$/directory-listing/xsl.
        /// </summary>
        private static string DirectoryListingXSLString = @"<?xml version=""1.0"" encoding=""UTF-8""?>

            <xsl:stylesheet version=""1.0""
                xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""
                xmlns=""http://www.w3.org/1999/xhtml"">

                <xsl:output method=""html""/>

                <xsl:template match=""/directory"">
                    <html>
                    <head>
                        <title>Directory listing</title>
                        <style type=""text/css"">" +
                            @"body{margin:10pt;font-family:""Verdana"",sans-serif;font-size:10pt}" +
                            @"table{border-collapse:collapse}" +
                            @".l{border-top:2px solid #339}" +
                            @".h th{padding:3pt 0}" +
                            @".n{padding-right:5pt;border-right:1px solid #ccc}" +
                            @".s{padding:0 5pt}" +
                            @".s.d{text-align:center;font-style:italic}" +
                            @".s.f{text-align:right}" +
                            @".r:hover{background:#eef}" +
                            @".r .n a{display:block}" +
                            @".t{font-size:80%;font-style:italic;color:#888}" +
                        @"</style>
                    </head>
                    <body>
                        <table>
                            <tr class=""l"">
                                <table>
                                    <tr>
                                        <td rowspan=""2""><img src=""{@img}"" alt=""folder""/></td>
                                        <td style=""font-size: 180%"">Directory listing</td>
                                    </tr>
                                    <tr>
                                        <td><span title=""{@url}""><xsl:value-of select=""@unescapedurl""/></span></td>
                                    </tr>
                                </table>
                            </tr>

                            <tr class=""l""><td colspan=""2"">
                                <table style=""width: 100%"">
                                    <tr class=""h""><th class=""n"" colspan=""2"">Name</th><th>Size</th></tr>
                                    <xsl:if test=""@url!='/'"">
                                        <tr><td></td><td class=""n""><a href="".."">..</a></td><td class=""s d"">Folder</td></tr>
                                    </xsl:if>
                                    <xsl:apply-templates select=""dir"" />
                                    <xsl:apply-templates select=""file"" />
                                    <tr style=""height: 3pt""><td class=""n"" colspan=""2""/><td/></tr>
                                </table>
                            </td></tr>

                            <tr class=""l t"">
                                <td colspan=""2"">Folder contains <xsl:value-of select=""@numdirs""/> sub-folders and <xsl:value-of select=""@numfiles""/> files.</td>
                            </tr>
                        </table>
                    </body>
                    </html>
                </xsl:template>

                <xsl:template match=""dir"">
                    <tr>
                        <td><img src=""{@img}"" alt=""folder""/></td>
                        <td class=""n""><a href=""{@link}""><xsl:value-of select="".""/></a></td>
                        <td class=""s d"">Folder</td>
                    </tr>
                </xsl:template>

                <xsl:template match=""file"">
                    <tr class=""r"">
                        <td><img src=""{@img}"" alt=""file""/></td>
                        <td class=""n"" style=""min-width: 150pt;""><a href=""{@link}""><xsl:value-of select="".""/></a></td>
                        <td class=""s f""><xsl:value-of select=""@nicesize""/></td>
                    </tr>
                </xsl:template>

            </xsl:stylesheet>
        ";

        /// <summary>
        /// Caches the UTF-8-encoded version of the directory-listing XSL file.
        /// </summary>
        private static byte[] DirectoryListingXSLByteArray = null;

        /// <summary>
        /// Contains string representations of the HTTP status codes defined in HTTP/1.1.
        /// </summary>
        public static Dictionary<HTTPStatusCode, string> StatusCodeNames = new Dictionary<HTTPStatusCode, string>
        {
            { HTTPStatusCode._100_Continue, "Continue" },
            { HTTPStatusCode._101_SwitchingProtocols, "Switching Protocols" },
            { HTTPStatusCode._200_OK, "OK" },
            { HTTPStatusCode._201_Created, "Created" },
            { HTTPStatusCode._202_Accepted, "Accepted" },
            { HTTPStatusCode._203_NonAuthoritativeInformation, "Non-Authoritative Information" },
            { HTTPStatusCode._204_NoContent, "No Content" },
            { HTTPStatusCode._205_ResetContent, "Reset Content" },
            { HTTPStatusCode._206_PartialContent, "Partial Content" },
            { HTTPStatusCode._300_MultipleChoices, "Multiple Choices" },
            { HTTPStatusCode._301_MovedPermanently, "Moved Permanently" },
            { HTTPStatusCode._302_Found, "Found" },
            { HTTPStatusCode._303_SeeOther, "See Other" },
            { HTTPStatusCode._304_NotModified, "Not Modified" },
            { HTTPStatusCode._305_UseProxy, "Use Proxy" },
            { HTTPStatusCode._306__Unused, "(Unused)" },
            { HTTPStatusCode._307_TemporaryRedirect, "Temporary Redirect" },
            { HTTPStatusCode._400_BadRequest, "Bad Request" },
            { HTTPStatusCode._401_Unauthorized, "Unauthorized" },
            { HTTPStatusCode._402_PaymentRequired, "Payment Required" },
            { HTTPStatusCode._403_Forbidden, "Forbidden" },
            { HTTPStatusCode._404_NotFound, "Not Found" },
            { HTTPStatusCode._405_MethodNotAllowed, "Method Not Allowed" },
            { HTTPStatusCode._406_NotAcceptable, "Not Acceptable" },
            { HTTPStatusCode._407_ProxyAuthenticationRequired, "Proxy Authentication Required" },
            { HTTPStatusCode._408_RequestTimeout, "Request Timeout" },
            { HTTPStatusCode._409_Conflict, "Conflict" },
            { HTTPStatusCode._410_Gone, "Gone" },
            { HTTPStatusCode._411_LengthRequired, "Length Required" },
            { HTTPStatusCode._412_PreconditionFailed, "Precondition Failed" },
            { HTTPStatusCode._413_RequestEntityTooLarge, "Request Entity Too Large" },
            { HTTPStatusCode._414_RequestURITooLong, "Request URI Too Long" },
            { HTTPStatusCode._415_UnsupportedMediaType, "Unsupported Media Type" },
            { HTTPStatusCode._416_RequestedRangeNotSatisfiable, "Requested Range Not Satisfiable" },
            { HTTPStatusCode._417_ExpectationFailed, "Expectation Failed" },
            { HTTPStatusCode._500_InternalServerError, "Internal Server Error" },
            { HTTPStatusCode._501_NotImplemented, "Not Implemented" },
            { HTTPStatusCode._502_BadGateway, "Bad Gateway" },
            { HTTPStatusCode._503_ServiceUnavailable, "Service Unavailable" },
            { HTTPStatusCode._504_GatewayTimeout, "Gateway Timeout" },
            { HTTPStatusCode._505_HTTPVersionNotSupported, "HTTP Version Not Supported" }
        };

        /// <summary>
        /// Returns a string representation of the specified HTTP status code.
        /// </summary>
        /// <param name="StatusCode">The status code to return a string representation for.</param>
        /// <returns>A string representation of the specified HTTP status code.</returns>
        public static string GetStatusCodeName(HTTPStatusCode StatusCode)
        {
            return StatusCodeNames.ContainsKey(StatusCode) ? StatusCodeNames[StatusCode] : "Unknown status code";
        }

        /// <summary>
        /// A random number generator used throughout the core server code.
        /// </summary>
        public static Random Rnd = new Random();

        /// <summary>
        /// Produces a single random hexadecimal digit and returns it as a byte.
        /// </summary>
        /// <returns>A single random hexadecimal digit as a byte.</returns>
        public static byte RandomHexDigit()
        {
            lock (Rnd)
            {
                int r = Rnd.Next(16);
                return r < 10 ? ((byte) (r + '0')) : ((byte) (r + 'A' - 10));
            }
        }

        /// <summary>
        /// Generates a random filename for a temporary file in the specified directory.
        /// </summary>
        /// <param name="TempDir">Directory to generate a temporary file in.</param>
        /// <param name="FStream">Will be set to a writable FileStream pointing at the newly-created, empty temporary file.</param>
        /// <returns>The full path and filename of the temporary file.</returns>
        public static string RandomTempFilepath(string TempDir, out Stream FStream)
        {
            string Dir = TempDir + (TempDir.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" : Path.DirectorySeparatorChar.ToString());
            lock (HTTPInternalObjects.Rnd)
            {
                int Counter = HTTPInternalObjects.Rnd.Next(1000);
                // This seemingly bizarre construct tries to prevent race conditions between several threads/processes trying to create the same file.
                while (true)
                {
                    if (Counter > 100000)
                        throw new IOException("Could not generate a new temporary filename in the directory " + TempDir +
                            ". Make sure that the directory exists. You may need to clear out this directory if it is full.");
                    try
                    {
                        string Filepath = Dir + "http_upload_" + Counter;
                        FStream = File.Open(Filepath, FileMode.CreateNew, FileAccess.Write);
                        return Filepath;
                    }
                    catch (IOException)
                    {
                        Counter += HTTPInternalObjects.Rnd.Next(1000);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a byte array containing the UTF-8-encoded directory-listing XSL.
        /// </summary>
        /// <returns>A byte array containing the UTF-8-encoded directory-listing XSL.</returns>
        public static byte[] DirectoryListingXSL()
        {
            if (DirectoryListingXSLByteArray != null)
                return DirectoryListingXSLByteArray;

            // This removes all the unnecessary whitespace from the XML and outputs it as UTF-8
            XmlDocument x = new XmlDocument();
            x.LoadXml(DirectoryListingXSLString);
            DirectoryListingXSLString = null; // free some memory?
            using (MemoryStream m = new MemoryStream())
            {
                using (XmlWriter w = new XmlTextWriter(m, Encoding.UTF8))
                {
                    x.WriteTo(w);
                    w.Close();
                }
                m.Close();
                DirectoryListingXSLByteArray = m.ToArray();
            }
            return DirectoryListingXSLByteArray;
        }

        /// <summary>
        /// Returns a byte array representing an icon in PNG format that corresponds to the specified file extension.
        /// </summary>
        /// <param name="Ext">The file extension for which to retrieve an icon in PNG format.</param>
        /// <returns>A byte array representing an icon in PNG format that corresponds to the specified file extension.</returns>
        public static byte[] GetDirectoryListingIcon(string Ext)
        {
            if (Ext == "folder") return Resources.folder_16;
            if (Ext == "folderbig") return Resources.folder_48;

            if (Ext == "bmp") return Resources.bmp_16;
            if (Ext == "csv") return Resources.csv_16;
            if (Ext == "doc") return Resources.doc_16;
            if (Ext == "exe") return Resources.exe_16;
            if (Ext == "faq") return Resources.faq_16;
            if (Ext == "gz") return Resources.zip_16;
            if (Ext == "jpg") return Resources.jpg_16;
            if (Ext == "pdf") return Resources.pdf_16;
            if (Ext == "pic") return Resources.pic_16;
            if (Ext == "png") return Resources.png_16;
            if (Ext == "pps") return Resources.pps_16;
            if (Ext == "ppt") return Resources.ppt_16;
            if (Ext == "txt") return Resources.txt_16;
            if (Ext == "xls") return Resources.xls_16;
            if (Ext == "zip") return Resources.zip_16;
            if (Ext == "rar") return Resources.gz_16;

            return Resources.txt_16;
        }

        /// <summary>
        /// Returns a file extension whose icon is used in directory listings to represent files of the specified file extension.
        /// </summary>
        /// <param name="Ext">The extension of the actual file for which to display an icon.</param>
        /// <returns>The file extension whose icon is used in directory listings to represent files of the specified file extension.</returns>
        public static string GetDirectoryListingIconStr(string Ext)
        {
            if (Ext == "folder") return Ext;
            if (Ext == "folderbig") return Ext;

            if (Ext == "bmp") return Ext;
            if (Ext == "csv") return Ext;
            if (Ext == "doc") return Ext;
            if (Ext == "exe") return Ext;
            if (Ext == "faq") return Ext;
            if (Ext == "gz") return "zip";
            if (Ext == "jpg") return Ext;
            if (Ext == "pdf") return Ext;
            if (Ext == "pic") return Ext;
            if (Ext == "png") return Ext;
            if (Ext == "pps") return Ext;
            if (Ext == "ppt") return Ext;
            if (Ext == "txt") return Ext;
            if (Ext == "xls") return Ext;
            if (Ext == "zip") return Ext;
            if (Ext == "rar") return Ext;

            return "txt";
        }

        /// <summary>
        /// Escapes all necessary characters in the specified string so as to make it usable safely in an HTML or XML context.
        /// </summary>
        /// <param name="Input">The string to apply HTML or XML escaping to.</param>
        /// <returns>The specified string with the necessary HTML or XML escaping applied.</returns>
        public static string HTMLEscape(this string Input)
        {
            return Input.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").Replace("\"", "&quot;");
        }

        /// <summary>
        /// Escapes all necessary characters in the specified string so as to make it usable safely in a URL.
        /// </summary>
        /// <param name="Input">The string to apply URL escaping to.</param>
        /// <returns>The specified string with the necessary URL escaping applied.</returns>
        public static string URLEscape(this string Input)
        {
            byte[] UTF8 = Input.ToUTF8();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in UTF8)
                sb.Append((b >= 'a' && b <= 'z') || (b >= 'A' && b <= 'Z') || (b >= '0' && b <= '9')
                    || (b == '$') || (b == '-') || (b == '_') || (b == '.') || (b == '!') || (b == '/')
                    || (b == '*') || (b == '\'') || (b == '(') || (b == ')') || (b == ',')
                    ? ((char) b).ToString() : string.Format("%{0:X2}", b));
            return sb.ToString();
        }

        /// <summary>
        /// Reverses the escaping performed by <see cref="URLEscape"/>() by decoding hexadecimal URL escape sequences into their original characters.
        /// </summary>
        /// <param name="Input">String containing URL escape sequences to be decoded.</param>
        /// <returns>The specified string with all URL escape sequences decoded.</returns>
        public static string URLUnescape(this string Input)
        {
            if (Input.Length < 3)
                return Input;
            int BufferSize = 0;
            int i = 0;
            while (i < Input.Length)
            {
                BufferSize++;
                if (Input[i] == '%') { i += 2; }
                i++;
            }
            byte[] Buffer = new byte[BufferSize];
            BufferSize = 0;
            i = 0;
            while (i < Input.Length)
            {
                if (Input[i] == '%' && i < Input.Length - 2)
                {
                    try
                    {
                        Buffer[BufferSize] = byte.Parse("" + Input[i + 1] + Input[i + 2], NumberStyles.HexNumber);
                        BufferSize++;
                    }
                    catch (Exception) { }
                    i += 3;
                }
                else
                {
                    Buffer[BufferSize] = Input[i] == '+' ? (byte) ' ' : (byte) Input[i];
                    BufferSize++;
                    i++;
                }
            }
            return Encoding.UTF8.GetString(Buffer, 0, BufferSize);
        }

        /// <summary>
        /// Converts the specified string to UTF-8.
        /// </summary>
        /// <param name="Input">String to convert to UTF-8.</param>
        /// <returns>The specified string, converted to a byte-array containing the UTF-8 encoding of the string.</returns>
        public static byte[] ToUTF8(this string Input)
        {
            return Encoding.UTF8.GetBytes(Input);
        }

        /// <summary>
        /// Determines the length of the UTF-8 encoding of the specified string.
        /// </summary>
        /// <param name="Input">String to determined UTF-8 length of.</param>
        /// <returns>The length of the string in bytes when encoded as UTF-8.</returns>
        public static int UTF8Length(this string Input)
        {
            return Encoding.UTF8.GetByteCount(Input);
        }

        /// <summary>
        /// Converts the specified string to a byte array. Non-ASCII characters are replaced with question marks ('?').
        /// </summary>
        /// <param name="Input">String to convert to a byte array.</param>
        /// <returns>The specified string, converted to a byte-array with non-ASCII characters replaced with question marks ('?').</returns>
        public static byte[] ToASCII(this string Input)
        {
            return Encoding.ASCII.GetBytes(Input);
        }
    }
}
