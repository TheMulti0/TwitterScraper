﻿using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace TwitterScraper
{
    internal static class HtmlNodeExtensions
    {
        public static HtmlNode FirstDescendantWithClassOrDefault(this HtmlNode node, string _class)
        {
            return node
                .Descendants()
                .FirstOrDefault(n => n.HasClass(_class));
        }
        
        public static IEnumerable<HtmlNode> WhereDescendantWithClass(this HtmlNode node, string _class)
        {
            return node
                .Descendants()
                .Where(n => n.HasClass(_class));
        }
    }
}