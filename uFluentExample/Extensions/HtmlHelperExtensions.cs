using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace uFluentExample.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static Regex ImageReplaceHeightRegex = new Regex(@"(<img [a-zA-Z0-9""\s=.]*)(height\s*=\s*\""*\d*\""*)");
        public static Regex ImageReplaceWidthRegex = new Regex(@"(<img [a-zA-Z0-9""\s=.]*)(width\s*=\s*\""*\d*\""*)");
        public static Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
        public static Regex LastHtmlRegex = new Regex("<.*?>", RegexOptions.RightToLeft);

        public static IHtmlString RichText(this HtmlHelper helper, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new HtmlString(string.Empty);
            }

            var parsedText = Umbraco.Web.Templates.TemplateUtilities.ParseInternalLinks(text);

            var passHeight = ImageReplaceHeightRegex.Replace(parsedText, "$1");
            var passWidth = ImageReplaceWidthRegex.Replace(passHeight, "$1");

            return helper.Raw(passWidth);
        }

        public static string PlainText(this HtmlHelper helper, string text)
        {
            return HtmlRegex.Replace(text, string.Empty);
        }

        public static IHtmlString Excerpt(this HtmlHelper helper, string html, int length)
        {

            html = html.Truncate(length);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            RemoveTags(htmlDoc, "//table", false);
            RemoveTags(htmlDoc, "//img", false);
            RemoveTags(htmlDoc, "//div");

            var excerpt = AppendEllipsisToHtml(htmlDoc.DocumentNode.WriteContentTo().Trim());
            return new HtmlString(excerpt);
        }

        private static string AppendEllipsisToHtml(string html)
        {
            // Html Agility Pack throws stack overflow exception when you attempt to do this so had to use regex.
            Match tag;
            if(EndsWithHtmlTag(html, out tag))
            {
                html = LastHtmlRegex.Replace(html, string.Format("&hellip;{0}", tag.Value), 1);
            }
            else
            {
                html = string.Format("{0}&hellip;", html);
            }
            return html;
        }

        private static bool EndsWithHtmlTag(string html, out Match match)
        {
            match = LastHtmlRegex.Match(html);
            return match.Index + match.Length == html.Length;
        }

        private static void RemoveTags(HtmlDocument htmlDoc, string tagXpath, bool keepContent = true)
        {
            var tags = htmlDoc.DocumentNode.SelectNodes(tagXpath);
            if(tags != null)
            {
                foreach(var tableTag in tags)
                {
                    tableTag.ParentNode.RemoveChild(tableTag, keepContent);
                }
            }
        }
    }
}