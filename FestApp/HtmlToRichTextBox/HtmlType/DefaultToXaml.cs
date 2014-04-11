using System;
using System.Windows.Documents;
using HtmlAgilityPack;

namespace BF.SL
{
    public class DefaultToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "#text"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var s = RichTextboxStyle.GetDefault(htmlNode);
            TextToRun(htmlNode.InnerText, s, block);
        }
    }
}
