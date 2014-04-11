using System.Windows.Documents;
using HtmlAgilityPack;
using System.Windows;
using System.Windows.Controls;

namespace BF.SL
{
    public class BlockQuoteToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "blockquote"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var ilContainer = new InlineUIContainer();
            var tbItem = new TextBlock
                             {
                                 Margin = new Thickness(25, 0, 0, 0),
                                 TextWrapping = TextWrapping.Wrap,
                                 FontStyle = FontStyles.Italic,
                                 Text = htmlNode.InnerText
                             };
            ilContainer.Child = tbItem;
            (block as Paragraph).Inlines.Add(ilContainer);
        }
    }
}
