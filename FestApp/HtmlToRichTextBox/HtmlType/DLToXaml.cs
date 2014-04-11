using System;
using System.Windows.Documents;
using HtmlAgilityPack;
using System.Windows;
using System.Windows.Controls;

namespace BF.SL
{
    public class DLToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "dl"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var ilContainer = new InlineUIContainer();
            var spUl = new StackPanel();
            ilContainer.Child = spUl;

            foreach (var itm in htmlNode.ChildNodes)
            {
                var tbItem = new TextBlock {Text = itm.InnerText, FontStyle = FontStyles.Italic};
                switch (itm.Name.ToLower())
                {
                    case "dt":
                        tbItem.Margin = new Thickness(15, 0, 0, 0);
                        break;

                    case "dd":
                        tbItem.Margin = new Thickness(25, 0, 0, 0);
                        break;
                }
                spUl.Children.Add(tbItem);
            }
            (block as Paragraph).Inlines.Add(ilContainer);
        }
    }
}
