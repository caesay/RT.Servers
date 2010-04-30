﻿using System;
using System.IO;
using System.Security.Cryptography;
using RT.Util.ExtensionMethods;

namespace RT.Servers
{
    /// <summary>
    /// Encapsulates an HTTP cookie.
    /// </summary>
    public sealed class Cookie
    {
#pragma warning disable 1591    // Missing XML comment for publicly visible type or member
        public string Name;
        public string Value;
        public string Path;
        public string Domain;
        public DateTime? Expires;
        public bool HttpOnly;
#pragma warning restore 1591    // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Encapsulates the possible values of the Cache-Control HTTP request or response header.
    /// </summary>
    public struct HttpCacheControl
    {
        /// <summary>Contains possible values of the Cache-Control header.</summary>
        public HttpCacheControlState State;
        /// <summary>Some values of the Cache-Control header have an integer parameter.</summary>
        public int? IntParameter;
        /// <summary>Some values of the Cache-Control header have a string parameter.</summary>
        public string StringParameter;
    }

    /// <summary>
    /// Encapsulates the possible values of the Content-Disposition HTTP response header.
    /// </summary>
    public struct HttpContentDisposition
    {
        /// <summary>Supports only two values ("None" and "Attachment").</summary>
        public HttpContentDispositionMode Mode;
        /// <summary>If Mode is "Attachment", contains the filename of the attachment.</summary>
        public string Filename;
    }

    /// <summary>
    /// Encapsulates the possible values of the Content-Range HTTP response header.
    /// </summary>
    public struct HttpContentRange
    {
        /// <summary>First byte index of the range. The first byte in the file has index 0.</summary>
        public long From;
        /// <summary>Last byte index of the range. For example, a range from 0 to 0 includes one byte.</summary>
        public long To;
        /// <summary>Total size of the file (not of the range).</summary>
        public long Total;
    }

    /// <summary>
    /// Encapsulates one of the ranges specified in a Range HTTP request header.
    /// </summary>
    public struct HttpRange
    {
        /// <summary>First byte index of the range. The first byte in the file has index 0.</summary>
        public long? From;
        /// <summary>Last byte index of the range. For example, a range from 0 to 0 includes one byte.</summary>
        public long? To;
    }

    /// <summary>
    /// Represents a file upload contained in an HTTP POST request.
    /// </summary>
    public sealed class FileUpload
    {
        /// <summary>The MIME type of the uploaded file.</summary>
        public string ContentType { get; private set; }
        /// <summary>The filename of the uploaded file as supplied by the client.</summary>
        public string Filename { get; private set; }

        /// <summary>Use this if the file upload content is stored on disk.</summary>
        internal string LocalFilename;
        /// <summary>Use this if the file upload content is stored in memory.</summary>
        internal byte[] Data;

        /// <summary>Constructor.</summary>
        internal FileUpload(string contentType, string filename)
        {
            ContentType = contentType;
            Filename = filename;
        }

        /// <summary>Moves the uploaded file to a file in the local file system.</summary>
        /// <remarks>Calling this method twice will move the file around, not create two copies.</remarks>
        public void SaveToFile(string localFilename)
        {
            if (Data != null)
            {
                using (var f = File.Open(localFilename, FileMode.Create, FileAccess.Write, FileShare.Write))
                    f.Write(Data, 0, Data.Length);
                LocalFilename = localFilename;
                Data = null;
            }
            else
            {
                File.Move(LocalFilename, localFilename);
                localFilename = LocalFilename;
            }
        }

        /// <summary>Returns a Stream object for access to the file upload.</summary>
        /// <remarks>The caller is responsible for disposing of the Stream object.</remarks>
        public Stream GetStream()
        {
            if (Data != null)
                return new MemoryStream(Data, false);
            else
                return File.Open(LocalFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>Returns the MD5 hash function of the upload contents.</summary>
        /// <returns>Result of the MD5 hash function as a string of hexadecimal digits.</returns>
        public string GetMd5()
        {
            if (Data != null)
                return MD5.Create().ComputeHash(Data).ToHex();
            else
                using (var f = File.Open(LocalFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    return MD5.Create().ComputeHash(f).ToHex();
        }
    }
}
