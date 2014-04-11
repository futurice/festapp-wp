using System.Linq;
using System.Windows.Documents;
using HtmlAgilityPack;
using System.Windows;
using System.Windows.Controls;

namespace BF.SL
{
    public class ULToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "ul"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var ilContainer = new InlineUIContainer();
            var spUL = new StackPanel();
            ilContainer.Child = spUL;
            spUL.Margin = new Thickness(25, 0, 0, 0);
            foreach (var tbItem in from itm in htmlNode.ChildNodes where itm.Name.Equals("li") select new TextBlock {Text = "« " + itm.InnerText})
            {
                spUL.Children.Add(tbItem);
            }

            (block as Paragraph).Inlines.Add(ilContainer);
        }
    }
}
