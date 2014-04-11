using System;
using System.Windows.Documents;
using HtmlAgilityPack;
using System.Windows;

namespace BF.SL
{
    public class IToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "i"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var s = RichTextboxStyle.GetDefault(htmlNode);
            s.FontStyle = FontStyles.Italic;

            TextToRun(htmlNode.InnerText, s, block);
        }
    }
}
