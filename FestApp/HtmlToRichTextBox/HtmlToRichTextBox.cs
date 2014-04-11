using System.Windows.Controls;
using System.Windows.Documents;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace BF.SL
{
    public class HtmlToRichTextBox
    {
        HtmlDocument _htmlDoc;
        readonly Dictionary<string, IHtmlType> _ruleDic = new Dictionary<string, IHtmlType>();

        public HtmlToRichTextBox(string html)
        {
            Init(html);
        }

        private void Init(string html)
        {
            //Initialize HtmlAgilityPack
            _htmlDoc = new HtmlDocument
                          {
                              OptionCheckSyntax = true,
                              OptionFixNestedTags = false,
                              OptionAutoCloseOnEnd = true
                          };
            _htmlDoc.LoadHtml(html);

            //add tag type
            AppendHtmlType(new HToXaml("h1"));
            AppendHtmlType(new HToXaml("h2"));
            AppendHtmlType(new HToXaml("h3"));
            AppendHtmlType(new HToXaml("h4"));
            AppendHtmlType(new HToXaml("h5"));
            AppendHtmlType(new HToXaml("h6"));
            AppendHtmlType(new HToXaml("h7"));
            AppendHtmlType(new BrToXaml());
            AppendHtmlType(new IToXaml());
            AppendHtmlType(new StrongToXaml("b"));
            AppendHtmlType(new StrongToXaml("strong"));
            AppendHtmlType(new AToXaml());

            //add special type
            AppendHtmlType(new ImageToXaml());
            AppendHtmlType(new ULToXaml());
            AppendHtmlType(new OLToXaml());
            AppendHtmlType(new DLToXaml());
            AppendHtmlType(new BlockQuoteToXaml());
            AppendHtmlType(new TableToXaml());

            //add default type
            AppendHtmlType(new DefaultToXaml());
        }

        /// <summary>
        /// Apply RichTextBox
        /// </summary>
        /// <param name="rtb"></param>
        public void ApplyHtmlToRichTextBox(RichTextBox rtb)
        {
            rtb.Blocks.Clear();
            var block = new Paragraph();
            foreach (var node in _htmlDoc.DocumentNode.ChildNodes)
                ConvertHtmlNode(node, block);
            rtb.Blocks.Add(block);
        }

        /// <summary>
        /// Append Html Type
        /// </summary>
        /// <param name="hType"></param>
        public void AppendHtmlType(IHtmlType hType)
        {

            if (_ruleDic.ContainsKey(hType.TagName))
                _ruleDic[hType.TagName] = hType;
            else
                _ruleDic.Add(hType.TagName, hType);
        }

        private void ConvertHtmlNode(HtmlNode htmlNode, Block block)
        {
            var htmlNodeName = htmlNode.Name.ToLower();

            if (new[] { "p", "div" }.Contains(htmlNodeName))
            {
                foreach (var childHtmlNode in htmlNode.ChildNodes.Where(childHtmlNode => !string.IsNullOrEmpty(htmlNode.InnerHtml)))
                {
                    ConvertHtmlNode(childHtmlNode, block);
                }
                var br = new LineBreak();
                (block as Paragraph).Inlines.Add(br);
            }

            if (new[] { "span" }.Contains(htmlNodeName))
            {
                foreach (var childHtmlNode in htmlNode.ChildNodes.Where(childHtmlNode => !string.IsNullOrEmpty(htmlNode.InnerHtml)))
                {
                    ConvertHtmlNode(childHtmlNode, block);
                }
            }

            if (_ruleDic.ContainsKey(htmlNodeName))
                _ruleDic[htmlNodeName].ApplyType(htmlNode, block);
        }
    }
}
