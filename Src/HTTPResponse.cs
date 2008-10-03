﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RT.Servers
{
    /// <summary>
    /// Encapsulates all supported HTTP response headers. A request handler can set these appropriately to cause the server to emit the required headers.
    /// </summary>
    public struct HTTPResponseHeaders
    {

#pragma warning disable 1591    // Missing XML comment for publicly visible type or member

        public HTTPAcceptRanges AcceptRanges;
        public int? Age; // in seconds
        public string[] Allow;  // usually: { "GET", "HEAD", "POST" }
        public HTTPCacheControl CacheControl;
        public HTTPConnection Connection;
        public HTTPContentEncoding ContentEncoding;
        public string ContentLanguage;
        public long? ContentLength;
        public HTTPContentDisposition ContentDisposition;
        public string ContentMD5;
        public HTTPContentRange? ContentRange;
        public string ContentType;
        public DateTime? Date;
        public string ETag;
        public DateTime? LastModified;
        public string Location; // used in redirection
        public string Server;
        public List<Cookie> SetCookie;
        public HTTPTransferEncoding TransferEncoding;

#pragma warning restore 1591    // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Returns the HTTP-compliant ASCII representation of all response headers that have been set.
        /// </summary>
        /// <returns>The HTTP-compliant ASCII representation of all response headers that have been set.</returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            if (AcceptRanges != HTTPAcceptRanges.None)
                b.Append("Accept-Ranges: bytes\r\n");
            if (Age != null)
                b.Append("Age: " + Age.Value + "\r\n");
            if (Allow != null)
                b.Append("Allow: " + string.Join(", ", Allow));
            if (CacheControl.State != HTTPCacheControlState.None)
            {
                b.Append("Cache-Control: ");
                if (CacheControl.State == HTTPCacheControlState.MaxAge && CacheControl.IntParameter != null)
                    b.Append("max-age=" + CacheControl.IntParameter.Value);
                else if (CacheControl.State == HTTPCacheControlState.MaxStale && CacheControl.IntParameter != null)
                    b.Append("max-stale=" + CacheControl.IntParameter.Value);
                else if (CacheControl.State == HTTPCacheControlState.MaxStale)
                    b.Append("max-stale");
                else if (CacheControl.State == HTTPCacheControlState.MinFresh && CacheControl.IntParameter != null)
                    b.Append("min-fresh=" + CacheControl.IntParameter.Value);
                else if (CacheControl.State == HTTPCacheControlState.MustRevalidate)
                    b.Append("must-revalidate");
                else if (CacheControl.State == HTTPCacheControlState.NoCache)
                    b.Append("no-cache");
                else if (CacheControl.State == HTTPCacheControlState.NoStore)
                    b.Append("no-store");
                else if (CacheControl.State == HTTPCacheControlState.NoTransform)
                    b.Append("no-transform");
                else if (CacheControl.State == HTTPCacheControlState.OnlyIfCached)
                    b.Append("only-if-cached");
                else if (CacheControl.State == HTTPCacheControlState.Private && CacheControl.StringParameter != null)
                    b.Append("private=\"" + CacheControl.StringParameter + "\"");
                else if (CacheControl.State == HTTPCacheControlState.ProxyRevalidate)
                    b.Append("proxy-revalidate");
                else if (CacheControl.State == HTTPCacheControlState.Public)
                    b.Append("public");
                else if (CacheControl.State == HTTPCacheControlState.SMaxAge && CacheControl.IntParameter != null)
                    b.Append("s-maxage=" + CacheControl.IntParameter.Value);
                b.Append("\r\n");
            }
            if (Connection != HTTPConnection.None)
            {
                if (Connection == HTTPConnection.Close)
                    b.Append("Connection: close\r\n");
                else
                    b.Append("Connection: keep-alive\r\n");
            }
            if (ContentEncoding != HTTPContentEncoding.Identity)
                b.Append("Content-Encoding: " + ContentEncoding.ToString().ToLowerInvariant() + "\r\n");
            if (ContentLanguage != null)
                b.Append("Content-Language: " + ContentLanguage + "\r\n");
            if (ContentLength != null)
                b.Append("Content-Length: " + ContentLength.Value + "\r\n");
            if (ContentDisposition.Mode != HTTPContentDispositionMode.None && ContentDisposition.Filename != null)
                b.Append("Content-Disposition: attachment; filename=" + ContentDisposition.Filename + "\r\n");
            if (ContentMD5 != null)
                b.Append("Content-MD5: " + ContentMD5 + "\r\n");
            if (ContentRange != null)
                b.Append("Content-Range: bytes " + ContentRange.Value.From + "-" + ContentRange.Value.To + "/" + ContentRange.Value.Total + "\r\n");
            if (ContentType != null)
                b.Append("Content-Type: " + ContentType + "\r\n");
            if (Date != null)
                b.Append("Date: " + Date.Value.ToString("r" /* = RFC1123 */) + "\r\n");
            if (ETag != null)
                b.Append("ETag: " + ETag + "\r\n");
            if (LastModified != null)
                b.Append("Last-Modified: " + LastModified.Value.ToString("r" /* = RFC1123 */) + "\r\n");
            if (Location != null)
                b.Append("Location: " + Location + "\r\n");
            if (Server != null)
                b.Append("Server: " + Server + "\r\n");
            if (SetCookie != null)
            {
                foreach (Cookie c in SetCookie)
                {
                    b.Append("Set-Cookie: " + c.Name + "=" + c.Value);
                    if (c.Domain != null)
                        b.Append("; domain=" + c.Domain);
                    if (c.Path != null)
                        b.Append("; path=" + c.Path);
                    if (c.Expires != null)
                        b.Append("; expires=" + c.Expires.Value.ToString("r"));
                    b.Append(c.HttpOnly ? "; httponly\r\n" : "\r\n");
                }
            }
            if (TransferEncoding != HTTPTransferEncoding.None)
                b.Append("Transfer-Encoding: chunked\r\n");
            return b.ToString();
        }
    }

    /// <summary>
    /// Encapsulates an HTTP response, to be sent by <see cref="HTTPServer"/> to the HTTP client that sent the original request.
    /// A request handler must return an HTTPResponse object to the <see cref="HTTPServer"/> when handling a request.
    /// </summary>
    public struct HTTPResponse
    {
        /// <summary>
        /// The HTTP status code. For example, HTTP 200 OK, HTTP 404 Not Found, HTTP 500 Internal Server Error.
        /// If not set, HTTP 200 OK is assumed as the default.
        /// </summary>
        public HTTPStatusCode Status;

        /// <summary>
        /// The HTTP response headers which are to be sent back to the HTTP client as part of this HTTP response.
        /// </summary>
        public HTTPResponseHeaders Headers;

        /// <summary>
        /// A stream object providing read access to the content returned. For static files, use <see cref="FileStream"/>.
        /// For objects cached in memory, use <see cref="MemoryStream"/>.
        /// For dynamic websites, consider using <see cref="RT.Util.Streams.DynamicContentStream"/>.
        /// </summary>
        public Stream Content;

        /// <summary>
        /// Internal field for <see cref="HTTPServer"/> to access the original request that this is the response for.
        /// </summary>
        internal HTTPRequest OriginalRequest;
    }
}
