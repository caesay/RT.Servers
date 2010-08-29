﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RT.TagSoup;
using RT.Util.Streams;
using RT.Util.ExtensionMethods;
using System.Linq;

namespace RT.Servers
{
    /// <summary>
    /// Encapsulates all supported HTTP response headers. A request handler can set these
    /// appropriately to cause the server to emit the required headers. See Remarks
    /// for a list of headers which are set by default.
    /// </summary>
    /// <remarks>
    /// By default, ContentType is set to "text/html; charset=utf-8".
    /// </remarks>
    public sealed class HttpResponseHeaders
    {

#pragma warning disable 1591    // Missing XML comment for publicly visible type or member

        public HttpAcceptRanges AcceptRanges;
        public int? Age; // in seconds
        public string[] Allow;  // usually: { "GET", "HEAD", "POST" }
        public HttpCacheControl[] CacheControl;
        public HttpConnection Connection;
        public HttpContentEncoding ContentEncoding;
        public string ContentLanguage;
        public long? ContentLength;
        public HttpContentDisposition ContentDisposition;
        public string ContentMD5;
        public HttpContentRange? ContentRange;
        public string ContentType = "text/html; charset=utf-8";
        public DateTime? Date;
        public string ETag;
        public DateTime? Expires;
        public DateTime? LastModified;
        public string Location; // used in redirection
        public string Pragma;
        public string Server;
        public List<Cookie> SetCookie;
        public HttpTransferEncoding TransferEncoding;

#pragma warning restore 1591    // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Returns the HTTP-compliant ASCII representation of all response headers that have been set.
        /// </summary>
        /// <returns>The HTTP-compliant ASCII representation of all response headers that have been set.</returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            if (AcceptRanges != HttpAcceptRanges.None)
                b.Append("Accept-Ranges: bytes\r\n");
            if (Age != null)
                b.Append("Age: " + Age.Value + "\r\n");
            if (Allow != null)
                b.Append("Allow: " + string.Join(", ", Allow));
            if (CacheControl != null)
            {
                var coll = CacheControl.Where(c => c.State != HttpCacheControlState.None).SelectMany(c =>
                    c.State == HttpCacheControlState.MaxAge && c.IntParameter != null ? new[] { "max-age=" + c.IntParameter.Value } :
                    c.State == HttpCacheControlState.MaxStale && c.IntParameter != null ? new[] { "max-stale=" + c.IntParameter.Value } :
                    c.State == HttpCacheControlState.MaxStale ? new[] { "max-stale" } :
                    c.State == HttpCacheControlState.MinFresh && c.IntParameter != null ? new[] { "min-fresh=" + c.IntParameter.Value } :
                    c.State == HttpCacheControlState.MustRevalidate ? new[] { "must-revalidate" } :
                    c.State == HttpCacheControlState.NoCache ? new[] { "no-cache" } :
                    c.State == HttpCacheControlState.NoStore ? new[] { "no-store" } :
                    c.State == HttpCacheControlState.NoTransform ? new[] { "no-transform" } :
                    c.State == HttpCacheControlState.OnlyIfCached ? new[] { "only-if-cached" } :
                    c.State == HttpCacheControlState.PostCheck && c.IntParameter != null ? new[] { "post-check=" + c.IntParameter.Value } :
                    c.State == HttpCacheControlState.PreCheck && c.IntParameter != null ? new[] { "pre-check=" + c.IntParameter.Value } :
                    c.State == HttpCacheControlState.Private && c.StringParameter != null ? new[] { "private=\"" + c.StringParameter + "\"" } :
                    c.State == HttpCacheControlState.ProxyRevalidate ? new[] { "proxy-revalidate" } :
                    c.State == HttpCacheControlState.Public ? new[] { "public" } :
                    c.State == HttpCacheControlState.SMaxAge && c.IntParameter != null ? new[] { "s-maxage=" + c.IntParameter.Value } :
                    new string[0]
                );
                if (coll.Any())
                {
                    b.Append("Cache-Control: ");
                    b.Append(coll.JoinString(", "));
                    b.Append("\r\n");
                }
            }
            if (Connection != HttpConnection.None)
            {
                if (Connection == HttpConnection.Close)
                    b.Append("Connection: close\r\n");
                else
                    b.Append("Connection: keep-alive\r\n");
            }
            if (ContentEncoding != HttpContentEncoding.Identity)
                b.Append("Content-Encoding: " + ContentEncoding.ToString().ToLowerInvariant() + "\r\n");
            if (ContentLanguage != null)
                b.Append("Content-Language: " + ContentLanguage + "\r\n");
            if (ContentLength != null)
                b.Append("Content-Length: " + ContentLength.Value + "\r\n");
            if (ContentDisposition.Mode != HttpContentDispositionMode.None && ContentDisposition.Filename != null)
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
            if (Expires != null)
                b.Append("Expires: " + Expires.Value.ToString("r" /* = RFC1123 */) + "\r\n");
            if (LastModified != null)
                b.Append("Last-Modified: " + LastModified.Value.ToString("r" /* = RFC1123 */) + "\r\n");
            if (Location != null)
                b.Append("Location: " + Location + "\r\n");
            if (Pragma != null)
                b.Append("Pragma: " + Pragma + "\r\n");
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
            if (TransferEncoding != HttpTransferEncoding.None)
                b.Append("Transfer-Encoding: chunked\r\n");
            return b.ToString();
        }
    }

    /// <summary>
    /// Encapsulates an HTTP response, to be sent by <see cref="HttpServer"/> to the HTTP client that sent the original request.
    /// A request handler must return an HttpResponse object to the <see cref="HttpServer"/> when handling a request.
    /// </summary>
    public sealed class HttpResponse
    {
        /// <summary>
        /// The HTTP status code. For example, 200 OK, 404 Not Found, 500 Internal Server Error.
        /// Default is 200 OK.
        /// </summary>
        public HttpStatusCode Status = HttpStatusCode._200_OK;

        /// <summary>
        /// The HTTP response headers which are to be sent back to the HTTP client as part of this HTTP response.
        /// If not set or modified, will default to a standard set of headers - see <see cref="HttpResponseHeaders"/>.
        /// </summary>
        public HttpResponseHeaders Headers = new HttpResponseHeaders();

        /// <summary>
        /// A stream object providing read access to the content returned. For static files, use <see cref="FileStream"/>.
        /// For objects cached in memory, use <see cref="MemoryStream"/>.
        /// For dynamic websites, consider using <see cref="RT.Util.Streams.DynamicContentStream"/>.
        /// </summary>
        public Stream Content;

        /// <summary>
        /// Specifies whether gzip should be used.
        /// </summary>
        public UseGzipOption UseGzip = UseGzipOption.AutoDetect;

        private HttpResponse() { }

        /// <summary>Returns the specified file from the local file system using the specified MIME content type to the client.</summary>
        /// <param name="filePath">Full path and filename of the file to return.</param>
        /// <param name="contentType">MIME type to use in the Content-Type header.</param>
        public static HttpResponse File(string filePath, string contentType)
        {
            try
            {
                FileStream fileStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                return new HttpResponse
                {
                    Status = HttpStatusCode._200_OK,
                    Content = fileStream,
                    Headers = new HttpResponseHeaders { ContentType = contentType }
                };
            }
            catch (FileNotFoundException)
            {
                return Error(HttpStatusCode._404_NotFound, "The requested file does not exist.");
            }
            catch (IOException e)
            {
                return Error(HttpStatusCode._500_InternalServerError, "File could not be opened in the file system: " + e.Message);
            }
        }

        /// <summary>Returns the specified content to the client with the MIME type “text/html; charset=utf-8”.</summary>
        /// <param name="content">Content to return to the client.</param>
        public static HttpResponse Html(string content)
        {
            return Create(content, "text/html; charset=utf-8");
        }

        /// <summary>Returns the specified content to the client as a single concatenated piece of text with the MIME type “text/html; charset=utf-8”.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="buffered">If true (default), the output is buffered for performance; otherwise, all text is transmitted as soon as possible.</param>
        public static HttpResponse Html(IEnumerable<string> content, bool buffered = true)
        {
            return Create(content, "text/html; charset=utf-8", buffered: buffered);
        }

        /// <summary>Returns the specified content to the client with the MIME type “text/plain; charset=utf-8”.</summary>
        /// <param name="content">Content to return to the client.</param>
        public static HttpResponse Plaintext(string content)
        {
            return Create(content, "text/plain; charset=utf-8");
        }

        /// <summary>Returns the specified content to the client as a single concatenated piece of text with the MIME type “text/plain; charset=utf-8”.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="buffered">If true (default), the output is buffered for performance; otherwise, all text is transmitted as soon as possible.</param>
        public static HttpResponse Plaintext(IEnumerable<string> content, bool buffered = true)
        {
            return Create(content, "text/plain; charset=utf-8", buffered: buffered);
        }

        /// <summary>Redirects the client to a new URL, using the HTTP status code 301 Moved Permanently.</summary>
        /// <param name="newUrl">URL to redirect the client to.</param>
        public static HttpResponse Redirect(string newUrl)
        {
            return new HttpResponse
            {
                Headers = new HttpResponseHeaders { Location = newUrl },
                Status = HttpStatusCode._301_MovedPermanently
            };
        }

        /// <summary>Generates a simple response with the specified HTTP status code, headers and message.
        /// Generally used for error conditions.</summary>
        /// <param name="statusCode">HTTP status code to use in the response.</param>
        /// <param name="errorMessage">Message to display along with the HTTP status code.</param>
        /// <param name="headers">Headers to use in the response, or null to use default values.</param>
        /// <param name="connectionClose">If true, the “Connection: Close” header is included, causing the HTTP connection to close after this response.
        /// Otherwise, the behaviour depends on the client’s original request.</param>
        /// <returns>A minimalist <see cref="HttpResponse"/> with the specified HTTP status code, headers and message.</returns>
        public static HttpResponse Error(HttpStatusCode statusCode = HttpStatusCode._500_InternalServerError, string errorMessage = null, HttpResponseHeaders headers = null, bool connectionClose = false)
        {
            if (headers == null)
                headers = new HttpResponseHeaders();

            string statusCodeNameHtml = string.Concat(((int) statusCode).ToString(), " ", statusCode.ToText()).HtmlEscape();

            if (connectionClose)
                headers.Connection = HttpConnection.Close;

            string contentStr =
                "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">\n" +
                "<html>\n <head>\n  <title>HTTP " + statusCodeNameHtml + "</title>\n </head>\n <body>\n  <h1>" + statusCodeNameHtml + "</h1>\n" +
                (errorMessage != null ? "  <p>" + errorMessage.HtmlEscape() + "</p>" : "") + "\n </body>\n</html>";
            return new HttpResponse
            {
                Status = statusCode,
                Headers = headers,
                Content = new MemoryStream(contentStr.ToUtf8())
            };
        }

        /// <summary>Returns a response to the client consisting of an empty body.</summary>
        /// <param name="status">HTTP status code to use in the response.</param>
        /// <param name="headers">Headers to use in the response, or null to use default values.</param>
        public static HttpResponse Create(HttpStatusCode status = HttpStatusCode._200_OK, HttpResponseHeaders headers = null)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = headers ?? new HttpResponseHeaders()
            };
        }

        /// <summary>Returns the specified string to the client, designating it as a specific MIME type.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="contentType">MIME type of the content.</param>
        /// <param name="statusCode">HTTP status code to use in the response.</param>
        public static HttpResponse Create(string content, string contentType, HttpStatusCode statusCode = HttpStatusCode._200_OK)
        {
            return new HttpResponse
            {
                Content = new MemoryStream(content.ToUtf8()),
                Headers = new HttpResponseHeaders { ContentType = contentType },
                Status = statusCode
            };
        }

        /// <summary>Returns the specified content to the client.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="status">HTTP status code to use in the response.</param>
        /// <param name="headers">Headers to use in the response, or null to use default values.</param>
        public static HttpResponse Create(string content, HttpStatusCode status = HttpStatusCode._200_OK, HttpResponseHeaders headers = null)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = headers ?? new HttpResponseHeaders(),
                Content = new MemoryStream(content.ToUtf8())
            };
        }

        /// <summary>Returns the specified content to the client as a single concatenated piece of text.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="contentType">MIME type of the content.</param>
        /// <param name="status">HTTP status code to use in the response.</param>
        /// <param name="buffered">If true (default), the output is buffered for performance; otherwise, all text is transmitted as soon as possible.</param>
        public static HttpResponse Create(IEnumerable<string> content, string contentType, HttpStatusCode status = HttpStatusCode._200_OK, bool buffered = true)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = new HttpResponseHeaders() { ContentType = contentType },
                Content = new DynamicContentStream(content, buffered)
            };
        }

        /// <summary>Returns the specified content to the client as a single concatenated piece of text.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="status">HTTP status code to use in the response.</param>
        /// <param name="headers">Headers to use in the response, or null to use default values.</param>
        /// <param name="buffered">If true (default), the output is buffered for performance; otherwise, all text is transmitted as soon as possible.</param>
        public static HttpResponse Create(IEnumerable<string> content, HttpStatusCode status = HttpStatusCode._200_OK, HttpResponseHeaders headers = null, bool buffered = true)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = headers ?? new HttpResponseHeaders(),
                Content = new DynamicContentStream(content, buffered)
            };
        }

        /// <summary>Returns the specified tag content to the client.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="contentType">MIME type of the content.</param>
        /// <param name="status">HTTP status code to use in the response.</param>
        /// <param name="buffered">If true (default), the output is buffered for performance; otherwise, all text is transmitted as soon as possible.</param>
        public static HttpResponse Create(Tag content, string contentType, HttpStatusCode status = HttpStatusCode._200_OK, bool buffered = true)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = new HttpResponseHeaders() { ContentType = contentType },
                Content = new DynamicContentStream(content.ToEnumerable(), buffered)
            };
        }

        /// <summary>Returns the specified tag content to the client.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="status">HTTP status code to use in the response.</param>
        /// <param name="headers">Headers to use in the response, or null to use default values.</param>
        /// <param name="buffered">If true (default), the output is buffered for performance; otherwise, all text is transmitted as soon as possible.</param>
        public static HttpResponse Create(Tag content, HttpStatusCode status = HttpStatusCode._200_OK, HttpResponseHeaders headers = null, bool buffered = true)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = headers ?? new HttpResponseHeaders(),
                Content = new DynamicContentStream(content.ToEnumerable(), buffered)
            };
        }

        /// <summary>Returns the contents of the specified stream to the client.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="contentType">MIME type of the content.</param>
        /// <param name="status">HTTP status code to use in the response.</param>
        public static HttpResponse Create(Stream content, string contentType, HttpStatusCode status = HttpStatusCode._200_OK)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = new HttpResponseHeaders() { ContentType = contentType },
                Content = content
            };
        }

        /// <summary>Returns the contents of the specified stream to the client.</summary>
        /// <param name="content">Content to return to the client.</param>
        /// <param name="status">HTTP status code to use in the response.</param>
        /// <param name="headers">Headers to use in the response, or null to use default values.</param>
        public static HttpResponse Create(Stream content, HttpStatusCode status = HttpStatusCode._200_OK, HttpResponseHeaders headers = null)
        {
            return new HttpResponse
            {
                Status = status,
                Headers = headers ?? new HttpResponseHeaders(),
                Content = content
            };
        }

        /// <summary>Generates a 500 Internal Server Error response which formats the specified exception as HTML.</summary>
        /// <param name="exception">Exception to format.</param>
        public static HttpResponse Exception(Exception exception)
        {
            return Create(ExceptionAsString(exception, true), HttpStatusCode._500_InternalServerError);
        }

        /// <summary>Generates a string describing the <paramref name="exception"/>, including the type, message
        /// and stack trace, and iterating over the InnerException chain.</summary>
        /// <param name="exception">The exception to be described.</param>
        /// <param name="html">If true, an HTML "DIV" tag will be returned with formatted info. Otherwise, a plaintext message is generated.</param>
        public static string ExceptionAsString(Exception exception, bool html)
        {
            bool first = true;
            if (html)
            {
                string exceptionHtml = "";
                while (exception != null)
                {
                    var exc = "<h3>" + exception.GetType().FullName.HtmlEscape() + "</h3>";
                    exc += "<p>" + exception.Message.HtmlEscape() + "</p>";
                    exc += "<pre>" + exception.StackTrace.HtmlEscape() + "</pre>";
                    exc += first ? "" : "<hr />";
                    exceptionHtml = exc + exceptionHtml;
                    exception = exception.InnerException;
                    first = false;
                }
                return "<div class='exception'>" + exceptionHtml + "</div>";
            }

            // Plain text
            string exceptionText = "";
            while (exception != null)
            {
                var exc = exception.GetType().FullName + "\n\n";
                exc += exception.Message + "\n\n";
                exc += exception.StackTrace + "\n\n";
                exc += first ? "\n\n\n" : "\n----------------------------------------------------------------------\n";
                exceptionText = exc + exceptionText;
                exception = exception.InnerException;
                first = false;
            }
            return exceptionText;
        }

        /// <summary>Modifies the <see cref="UseGzip"/> option in this response object and returns the same object.</summary>
        /// <param name="option">The new value for the <see cref="UseGzip"/> option.</param>
        public HttpResponse Set(UseGzipOption option)
        {
            UseGzip = option;
            return this;
        }
    }
}
