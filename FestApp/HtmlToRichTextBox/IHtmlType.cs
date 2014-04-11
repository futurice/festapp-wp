using System;
using System.Windows.Documents;
using HtmlAgilityPack;

namespace BF.SL
{
    public interface IHtmlType
    {
        string TagName { get; }
        void ApplyType(HtmlNode htmlNode, Block block);
    }
}
