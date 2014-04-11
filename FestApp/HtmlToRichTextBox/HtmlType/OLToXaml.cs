using System;
using System.Linq;
using System.Windows.Documents;
using HtmlAgilityPack;
using System.Windows;
using System.Windows.Controls;

namespace BF.SL
{
    public class OLToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "ol"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var ilContainer = new InlineUIContainer();
            var spUL = new StackPanel();
            ilContainer.Child = spUL;
            spUL.Margin = new Thickness(25, 0, 0, 0);
            var i = 0;
            foreach (var itm in htmlNode.ChildNodes.Where(itm => itm.Name.Equals("li")))
            {
                i++;
                var tbItem = new TextBlock {Text = i + ".  " + itm.InnerText};
                //tbItem.FontStyle = FontStyles.Italic;
                spUL.Children.Add(tbItem);
            }
            (block as Paragraph).Inlines.Add(ilContainer);
        }
    }
}
