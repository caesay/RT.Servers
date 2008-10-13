﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RT.Util.ExtensionMethods;

namespace RT.TagSoup
{
    /// <summary>
    /// Abstract base class for an HTML or XHTML tag.
    /// </summary>
    public abstract class TagSoup
    {
        /// <summary>Remembers the contents of this tag.</summary>
        protected List<object> TagContents = null;

        /// <summary>Name of the tag.</summary>
        public abstract string TagName { get; }
        /// <summary>DOCTYPE that is output before the tag. Only used by the &lt;HTML&gt; HTML tag and the &lt;html&gt; XHTML tag.</summary>
        public virtual string DocType { get { return null; } }
        /// <summary>Whether the start tag should be printed. If the tag has attributes, it will be printed regardless.</summary>
        public virtual bool StartTag { get { return true; } }
        /// <summary>Whether the end tag should be printed.</summary>
        public virtual bool EndTag { get { return true; } }
        /// <summary>Whether XHTML-style &lt;/&gt; empty-tag markers are allowed.</summary>
        public abstract bool AllowXHTMLEmpty { get; }

        /// <summary>Sets the contents of the tag. Any objects are allowed.</summary>
        /// <param name="Contents"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>Special support exists for the following types:</para>
        ///     <list type="bullet">
        ///         <item><term><c>string</c></term><description>outputs that string (HTML-escaped, of course)</description></item>
        ///         <item><term><c>IEnumerable&lt;string&gt;</c></term><description>concatenates all contained strings</description></item>
        ///         <item><term><c>Func&lt;string&gt;</c></term><description>calls the function and outputs the returned string</description></item>
        ///         <item><term><c>Func&lt;IEnumerable&lt;string&gt;&gt;</c></term><description>calls the function and concatenates all strings returned</description></item>
        ///         <item><term><c>TagSoup</c></term><description>outputs that tag and its contents</description></item>
        ///         <item><term><c>IEnumerable&lt;TagSoup&gt;</c></term><description>concatenates all contained tags</description></item>
        ///         <item><term><c>Func&lt;TagSoup&gt;</c></term><description>calls the function and outputs the returned tag</description></item>
        ///         <item><term><c>Func&lt;IEnumerable&lt;TagSoup&gt;&gt;</c></term><description>calls the function and concatenates all tags returned</description></item>
        ///     </list>
        ///     <para>Using objects of type <c>Func&lt;...&gt;</c> is a convenient way to defer execution to ensure maximally responsive output.</para>
        /// </remarks>
        public TagSoup _(params object[] Contents)
        {
            if (TagContents == null)
                TagContents = new List<object>(Contents);
            else
                TagContents.AddRange(Contents);
            return this;
        }

        /// <summary>Adds stuff at the end of the contents of this tag (a string, a tag, a collection of strings or of tags).</summary>
        /// <param name="Content">The stuff to add.</param>
        public void Add(object Content) { TagContents.Add(Content); }

        /// <summary>Outputs this tag and all its contents.</summary>
        /// <returns>A collection of strings which, when concatenated, represent this tag and all its contents.</returns>
        public virtual IEnumerable<string> ToEnumerable()
        {
            if (DocType != null)
                yield return DocType;

            if (StartTag)
                yield return "<" + TagName;
            bool TagPrinted = StartTag;

            foreach (var Field in this.GetType().GetFields())
            {
                if (Field.Name.StartsWith("_"))
                    continue;
                object Val = Field.GetValue(this);
                if (Val == null) continue;
                if (Field.FieldType.IsEnum && Val.ToString() == "_")
                    continue;
                if (Val is bool && !((bool) Val))
                    continue;

                if (!TagPrinted)
                {
                    yield return "<" + TagName;
                    TagPrinted = true;
                }

                if (Field.FieldType.IsEnum)
                    yield return " " + FixFieldName(Field.Name) + "=\"" + FixFieldName(Val.ToString()) + "\"";
                else if (Val is bool)
                {
                    string s = FixFieldName(Field.Name);
                    yield return " " + s + "=\"" + s + "\"";
                }
                else
                    yield return " " + FixFieldName(Field.Name) + "=\"" + Val.ToString().HTMLEscape() + "\"";
            }
            if (TagPrinted && AllowXHTMLEmpty && (TagContents == null || TagContents.Count == 0))
            {
                yield return "/>";
                yield break;
            }
            if (TagPrinted)
                yield return ">";
            foreach (object Content in TagContents)
            {
                if (Content == null)
                    continue;
                foreach (string s in Stringify(Content))
                    yield return s;
            }
            if (EndTag)
                yield return "</" + TagName + ">";
        }

        /// <summary>Converts the entire tag tree into a single string.</summary>
        /// <returns>The entire tag tree as a single string.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in ToEnumerable())
                sb.Append(s);
            return sb.ToString();
        }

        private IEnumerable<string> Stringify(object o)
        {
            if (o == null)
                yield break;

            if (o is string)
            {
                yield return (o as string).HTMLEscape();
                yield break;
            }

            if (o is TagSoup)
            {
                foreach (string str in (o as TagSoup).ToEnumerable())
                    yield return str;
                yield break;
            }

            if (o is IEnumerable)
            {
                foreach (object s in (o as IEnumerable))
                    foreach (string str in Stringify(s))
                        yield return str;
                yield break;
            }

            Type t = o.GetType();
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Func<>))
            {
                object Result = t.GetMethod("Invoke").Invoke(o, new object[] { });
                foreach (string str in Stringify(Result))
                    yield return str;
                yield break;
            }

            yield return "Unrecognised object: " + o.ToString();
        }

        /// <summary>Converts a C#-compatible field name into an HTML/XHTML-compatible one.</summary>
        /// <example>
        ///     <list type="bullet">
        ///         <item><c>class_</c> is converted to <c>"class"</c></item>
        ///         <item><c>accept_charset</c> is converted to <c>"accept-charset"</c></item>
        ///         <item><c>xmlLang</c> is converted to <c>"xml:lang"</c></item>
        ///         <item><c>_</c> would be converted to the empty string, but <see cref="ToEnumerable"/> already skips those.</item>
        ///     </list>
        /// </example>
        /// <param name="fn">Field name to convert.</param>
        /// <returns>Converted field name.</returns>
        private static string FixFieldName(string fn)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fn.Length; i++)
                if (fn[i] >= 'A' && fn[i] <= 'Z')
                    sb.Append(":" + char.ToLowerInvariant(fn[i]));
                else if (fn[i] == '_' && i < fn.Length - 1)
                    sb.Append('-');
                else if (fn[i] != '_')
                    sb.Append(fn[i]);
            return sb.ToString();
        }
    }
}
