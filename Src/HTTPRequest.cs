﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Servers
{
    public delegate HTTPResponse HTTPRequestHandler(HTTPRequest Request);

    public struct HTTPRequestHeaders
    {
        public string[] Accept;
        public string[] AcceptCharset;
        public HTTPContentEncoding[] AcceptEncoding;
        public string[] AcceptLanguage;
        // Authorization?
        public HTTPConnection Connection;
        public int? ContentLength;      // required only for POST
        public string ContentType;      // required only for POST
        public Dictionary<string, Cookie> Cookie;
        public string Host;
        public DateTime? IfModifiedSince;
        public string IfNoneMatch;
        public HTTPRange[] Range;
        public string UserAgent;
        public Dictionary<string, string> UnrecognisedHeaders;
    }

    public struct HTTPRequest
    {
        public HTTPMethod Method;
        public string URL;
        public string RestURL;  // after subtracting the part of the path where the handler is hooked
        public HTTPRequestHeaders Headers;
        public Stream Content;
        internal HTTPRequestHandler Handler;  // used only internally
    }

    public class InvalidRequestException : Exception
    {
        public HTTPResponse Response;
        public InvalidRequestException(HTTPResponse Response) { this.Response = Response; }
    }
}
