﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RT.TagSoup.HtmlTags
{
    /// <summary>Abstract base class for HTML tags.</summary>
    public abstract class HtmlTag : Tag
    {
        /// <summary>Constructs an HTML tag.</summary>
        /// <param name="Contents">Contents of the tag.</param>
        public HtmlTag(params object[] Contents) { TagContents = new List<object>(Contents); }
        /// <summary>Returns false.</summary>
        public override bool AllowXhtmlEmpty { get { return false; } }
    }

#pragma warning disable 1591    // Missing XML comment for publicly visible type or member

    public class A : HtmlTag
    {
        public A(params object[] contents) : base(contents) { }
        public override string TagName { get { return "A"; } }
        public string accesskey;
        public string charset;
        public string class_;
        public string coords;
        public string dir;
        public string href;
        public string hreflang;
        public string id;
        public string lang;
        public string name;
        public string onblur;
        public string onclick;
        public string ondblclick;
        public string onfocus;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string rel;
        public string rev;
        public string shape;
        public string style;
        public string tabindex;
        public string title;
        public string type;
    }
    public class ABBR : HtmlTag
    {
        public ABBR(params object[] contents) : base(contents) { }
        public override string TagName { get { return "ABBR"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class ACRONYM : HtmlTag
    {
        public ACRONYM(params object[] contents) : base(contents) { }
        public override string TagName { get { return "ACRONYM"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class ADDRESS : HtmlTag
    {
        public ADDRESS(params object[] contents) : base(contents) { }
        public override string TagName { get { return "ADDRESS"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class AREA : HtmlTag
    {
        public AREA(params object[] contents) : base(contents) { }
        public override string TagName { get { return "AREA"; } }
        public override bool EndTag { get { return false; } }
        public string accesskey;
        public string alt;
        public string class_;
        public string coords;
        public string dir;
        public string href;
        public string id;
        public string lang;
        public string nohref;
        public string onblur;
        public string onclick;
        public string ondblclick;
        public string onfocus;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string shape;
        public string style;
        public string tabindex;
        public string title;
    }
    public class B : HtmlTag
    {
        public B(params object[] contents) : base(contents) { }
        public override string TagName { get { return "B"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class BASE : HtmlTag
    {
        public BASE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "BASE"; } }
        public override bool EndTag { get { return false; } }
        public string href;
    }
    public class BDO : HtmlTag
    {
        public BDO(params object[] contents) : base(contents) { }
        public override string TagName { get { return "BDO"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string style;
        public string title;
    }
    public class BIG : HtmlTag
    {
        public BIG(params object[] contents) : base(contents) { }
        public override string TagName { get { return "BIG"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class BLOCKQUOTE : HtmlTag
    {
        public BLOCKQUOTE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "BLOCKQUOTE"; } }
        public string cite;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class BODY : HtmlTag
    {
        public BODY(params object[] contents) : base(contents) { }
        public override string TagName { get { return "BODY"; } }
        public override bool StartTag { get { return false; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onload;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string onunload;
        public string style;
        public string title;
    }
    public class BR : HtmlTag
    {
        public BR(params object[] contents) : base(contents) { }
        public override string TagName { get { return "BR"; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string id;
        public string style;
        public string title;
    }
    public class BUTTON : HtmlTag
    {
        public BUTTON(params object[] contents) : base(contents) { }
        public override string TagName { get { return "BUTTON"; } }
        public string accesskey;
        public string class_;
        public string dir;
        public string disabled;
        public string id;
        public string lang;
        public string name;
        public string onblur;
        public string onclick;
        public string ondblclick;
        public string onfocus;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string tabindex;
        public string title;
        public string type;
        public string value;
    }
    public class CAPTION : HtmlTag
    {
        public CAPTION(params object[] contents) : base(contents) { }
        public override string TagName { get { return "CAPTION"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class CITE : HtmlTag
    {
        public CITE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "CITE"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class CODE : HtmlTag
    {
        public CODE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "CODE"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class COL : HtmlTag
    {
        public COL(params object[] contents) : base(contents) { }
        public override string TagName { get { return "COL"; } }
        public override bool EndTag { get { return false; } }
        public string align;
        public string char_;
        public string charoff;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string span;
        public string style;
        public string title;
        public string valign;
        public string width;
    }
    public class COLGROUP : HtmlTag
    {
        public COLGROUP(params object[] contents) : base(contents) { }
        public override string TagName { get { return "COLGROUP"; } }
        public override bool EndTag { get { return false; } }
        public string align;
        public string char_;
        public string charoff;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string span;
        public string style;
        public string title;
        public string valign;
        public string width;
    }
    public class DD : HtmlTag
    {
        public DD(params object[] contents) : base(contents) { }
        public override string TagName { get { return "DD"; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class DEL : HtmlTag
    {
        public DEL(params object[] contents) : base(contents) { }
        public override string TagName { get { return "DEL"; } }
        public string cite;
        public string class_;
        public string datetime;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class DFN : HtmlTag
    {
        public DFN(params object[] contents) : base(contents) { }
        public override string TagName { get { return "DFN"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class DIV : HtmlTag
    {
        public DIV(params object[] contents) : base(contents) { }
        public override string TagName { get { return "DIV"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class DL : HtmlTag
    {
        public DL(params object[] contents) : base(contents) { }
        public override string TagName { get { return "DL"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class DT : HtmlTag
    {
        public DT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "DT"; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class EM : HtmlTag
    {
        public EM(params object[] contents) : base(contents) { }
        public override string TagName { get { return "EM"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class FIELDSET : HtmlTag
    {
        public FIELDSET(params object[] contents) : base(contents) { }
        public override string TagName { get { return "FIELDSET"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class FORM : HtmlTag
    {
        public FORM(params object[] contents) : base(contents) { }
        public override string TagName { get { return "FORM"; } }
        public string accept;
        public string accept_charset;
        public string action;
        public string class_;
        public string dir;
        public string enctype;
        public string id;
        public string lang;
        public string method;
        public string name;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string onreset;
        public string onsubmit;
        public string style;
        public string title;
    }
    public class H1 : HtmlTag
    {
        public H1(params object[] contents) : base(contents) { }
        public override string TagName { get { return "H1"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class H2 : HtmlTag
    {
        public H2(params object[] contents) : base(contents) { }
        public override string TagName { get { return "H2"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class H3 : HtmlTag
    {
        public H3(params object[] contents) : base(contents) { }
        public override string TagName { get { return "H3"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class H4 : HtmlTag
    {
        public H4(params object[] contents) : base(contents) { }
        public override string TagName { get { return "H4"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class H5 : HtmlTag
    {
        public H5(params object[] contents) : base(contents) { }
        public override string TagName { get { return "H5"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class H6 : HtmlTag
    {
        public H6(params object[] contents) : base(contents) { }
        public override string TagName { get { return "H6"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class HEAD : HtmlTag
    {
        public HEAD(params object[] contents) : base(contents) { }
        public override string TagName { get { return "HEAD"; } }
        public override bool StartTag { get { return false; } }
        public override bool EndTag { get { return false; } }
        public string dir;
        public string lang;
        public string profile;
    }
    public class HR : HtmlTag
    {
        public HR(params object[] contents) : base(contents) { }
        public override string TagName { get { return "HR"; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class HTML : HtmlTag
    {
        public HTML(params object[] contents) : base(contents) { }
        public override string TagName { get { return "HTML"; } }
        public override bool StartTag { get { return false; } }
        public override bool EndTag { get { return false; } }
        public override string DocType { get { return @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN"" ""http://www.w3.org/TR/html4/strict.dtd"">"; } }
        public string dir;
        public string lang;
    }
    public class I : HtmlTag
    {
        public I(params object[] contents) : base(contents) { }
        public override string TagName { get { return "I"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class IMG : HtmlTag
    {
        public IMG(params object[] contents) : base(contents) { }
        public override string TagName { get { return "IMG"; } }
        public override bool EndTag { get { return false; } }
        public string alt;
        public string class_;
        public string dir;
        public string height;
        public string id;
        public string ismap;
        public string lang;
        public string longdesc;
        public string name;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string src;
        public string style;
        public string title;
        public string usemap;
        public string width;
    }
    public class INPUT : HtmlTag
    {
        public INPUT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "INPUT"; } }
        public override bool EndTag { get { return false; } }
        public string PASSWORD;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
        public string type;
    }
    public class INS : HtmlTag
    {
        public INS(params object[] contents) : base(contents) { }
        public override string TagName { get { return "INS"; } }
        public string cite;
        public string class_;
        public string datetime;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class KBD : HtmlTag
    {
        public KBD(params object[] contents) : base(contents) { }
        public override string TagName { get { return "KBD"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class LABEL : HtmlTag
    {
        public LABEL(params object[] contents) : base(contents) { }
        public override string TagName { get { return "LABEL"; } }
        public string accesskey;
        public string class_;
        public string dir;
        public string for_;
        public string id;
        public string lang;
        public string onblur;
        public string onclick;
        public string ondblclick;
        public string onfocus;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class LEGEND : HtmlTag
    {
        public LEGEND(params object[] contents) : base(contents) { }
        public override string TagName { get { return "LEGEND"; } }
        public string accesskey;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class LI : HtmlTag
    {
        public LI(params object[] contents) : base(contents) { }
        public override string TagName { get { return "LI"; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class LINK : HtmlTag
    {
        public LINK(params object[] contents) : base(contents) { }
        public override string TagName { get { return "LINK"; } }
        public override bool EndTag { get { return false; } }
        public string charset;
        public string class_;
        public string dir;
        public string href;
        public string hreflang;
        public string id;
        public string lang;
        public string media;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string rel;
        public string rev;
        public string style;
        public string title;
        public string type;
    }
    public class MAP : HtmlTag
    {
        public MAP(params object[] contents) : base(contents) { }
        public override string TagName { get { return "MAP"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string name;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class META : HtmlTag
    {
        public META(params object[] contents) : base(contents) { }
        public override string TagName { get { return "META"; } }
        public override bool EndTag { get { return false; } }
        public string content;
        public string dir;
        public string http_equiv;
        public string lang;
        public string name;
        public string scheme;
    }
    public class NOSCRIPT : HtmlTag
    {
        public NOSCRIPT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "NOSCRIPT"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class OBJECT : HtmlTag
    {
        public OBJECT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "OBJECT"; } }
        public string archive;
        public string class_;
        public string classid;
        public string codebase;
        public string codetype;
        public string data;
        public string declare;
        public string dir;
        public string height;
        public string id;
        public string lang;
        public string name;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string standby;
        public string style;
        public string tabindex;
        public string title;
        public string type;
        public string usemap;
        public string width;
    }
    public class OL : HtmlTag
    {
        public OL(params object[] contents) : base(contents) { }
        public override string TagName { get { return "OL"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class OPTGROUP : HtmlTag
    {
        public OPTGROUP(params object[] contents) : base(contents) { }
        public override string TagName { get { return "OPTGROUP"; } }
        public string class_;
        public string dir;
        public string disabled;
        public string id;
        public string label;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class OPTION : HtmlTag
    {
        public OPTION(params object[] contents) : base(contents) { }
        public override string TagName { get { return "OPTION"; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string dir;
        public string disabled;
        public string id;
        public string label;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string selected;
        public string style;
        public string title;
        public string value;
    }
    public class P : HtmlTag
    {
        public P(params object[] contents) : base(contents) { }
        public override string TagName { get { return "P"; } }
        public override bool EndTag { get { return false; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class PARAM : HtmlTag
    {
        public PARAM(params object[] contents) : base(contents) { }
        public override string TagName { get { return "PARAM"; } }
        public override bool EndTag { get { return false; } }
        public string id;
        public string name;
        public string type;
        public string value;
        public string valuetype;
    }
    public class PRE : HtmlTag
    {
        public PRE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "PRE"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class Q : HtmlTag
    {
        public Q(params object[] contents) : base(contents) { }
        public override string TagName { get { return "Q"; } }
        public string cite;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class SAMP : HtmlTag
    {
        public SAMP(params object[] contents) : base(contents) { }
        public override string TagName { get { return "SAMP"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class SCRIPT : HtmlTag
    {
        public SCRIPT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "SCRIPT"; } }
        public string charset;
        public string defer;
        public string event_;
        public string for_;
        public string src;
        public string type;
    }
    public class SELECT : HtmlTag
    {
        public SELECT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "SELECT"; } }
        public string class_;
        public string dir;
        public string disabled;
        public string id;
        public string lang;
        public string multiple;
        public string name;
        public string onblur;
        public string onchange;
        public string onclick;
        public string ondblclick;
        public string onfocus;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string size;
        public string style;
        public string tabindex;
        public string title;
    }
    public class SMALL : HtmlTag
    {
        public SMALL(params object[] contents) : base(contents) { }
        public override string TagName { get { return "SMALL"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class SPAN : HtmlTag
    {
        public SPAN(params object[] contents) : base(contents) { }
        public override string TagName { get { return "SPAN"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class STRONG : HtmlTag
    {
        public STRONG(params object[] contents) : base(contents) { }
        public override string TagName { get { return "STRONG"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class STYLE : HtmlTag
    {
        public STYLE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "STYLE"; } }
        public string dir;
        public string lang;
        public string media;
        public string title;
        public string type;
    }
    public class SUB : HtmlTag
    {
        public SUB(params object[] contents) : base(contents) { }
        public override string TagName { get { return "SUB"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class SUP : HtmlTag
    {
        public SUP(params object[] contents) : base(contents) { }
        public override string TagName { get { return "SUP"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class TABLE : HtmlTag
    {
        public TABLE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TABLE"; } }
        public string border;
        public string class_;
        public string dir;
        public string frame;
        public string groups;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string rules;
        public string style;
        public string summary;
        public string title;
        public string width;
    }
    public class TBODY : HtmlTag
    {
        public TBODY(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TBODY"; } }
        public override bool StartTag { get { return false; } }
        public override bool EndTag { get { return false; } }
        public string align;
        public string char_;
        public string charoff;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
        public string valign;
    }
    public class TD : HtmlTag
    {
        public TD(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TD"; } }
        public override bool EndTag { get { return false; } }
        public string abbr;
        public string align;
        public string axis;
        public string char_;
        public string charoff;
        public string class_;
        public string colspan;
        public string dir;
        public string headers;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string rowspan;
        public string scope;
        public string style;
        public string title;
        public string valign;
        public string width;
    }
    public class TEXTAREA : HtmlTag
    {
        public TEXTAREA(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TEXTAREA"; } }
        public string accesskey;
        public string class_;
        public string cols;
        public string dir;
        public string disabled;
        public string id;
        public string lang;
        public string name;
        public string onblur;
        public string onchange;
        public string onclick;
        public string ondblclick;
        public string onfocus;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string onselect;
        public string readonly_;
        public string rows;
        public string style;
        public string tabindex;
        public string title;
    }
    public class TFOOT : HtmlTag
    {
        public TFOOT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TFOOT"; } }
        public override bool EndTag { get { return false; } }
        public string align;
        public string char_;
        public string charoff;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
        public string valign;
    }
    public class TH : HtmlTag
    {
        public TH(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TH"; } }
        public override bool EndTag { get { return false; } }
        public string abbr;
        public string align;
        public string axis;
        public string char_;
        public string charoff;
        public string class_;
        public string colspan;
        public string dir;
        public string headers;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string rowspan;
        public string scope;
        public string style;
        public string title;
        public string valign;
    }
    public class THEAD : HtmlTag
    {
        public THEAD(params object[] contents) : base(contents) { }
        public override string TagName { get { return "THEAD"; } }
        public override bool EndTag { get { return false; } }
        public string align;
        public string char_;
        public string charoff;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
        public string valign;
    }
    public class TITLE : HtmlTag
    {
        public TITLE(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TITLE"; } }
    }
    public class TR : HtmlTag
    {
        public TR(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TR"; } }
        public override bool EndTag { get { return false; } }
        public string align;
        public string char_;
        public string charoff;
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
        public string valign;
    }
    public class TT : HtmlTag
    {
        public TT(params object[] contents) : base(contents) { }
        public override string TagName { get { return "TT"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class UL : HtmlTag
    {
        public UL(params object[] contents) : base(contents) { }
        public override string TagName { get { return "UL"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class VAR : HtmlTag
    {
        public VAR(params object[] contents) : base(contents) { }
        public override string TagName { get { return "VAR"; } }
        public string class_;
        public string dir;
        public string id;
        public string lang;
        public string onclick;
        public string ondblclick;
        public string onkeydown;
        public string onkeypress;
        public string onkeyup;
        public string onmousedown;
        public string onmousemove;
        public string onmouseout;
        public string onmouseover;
        public string onmouseup;
        public string style;
        public string title;
    }
    public class WBR : HtmlTag
    {
        public WBR(params object[] contents) : base(contents) { }
        public override string TagName { get { return "WBR"; } }
        public override bool EndTag { get { return false; } }
    }

#pragma warning restore 1591    // Missing XML comment for publicly visible type or member

}