using System;
using System.Windows.Documents;
using HtmlAgilityPack;
using System.Windows.Controls;
using System.Linq;

namespace BF.SL
{
    public class TableToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "table"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var rows = htmlNode.ChildNodes
                .Where(a => a.Name.ToLower() == "tr");
            if (!rows.Any())
                return;

            var cols = rows.First().ChildNodes
                .Where(a => a.Name.ToLower() == "td");
            if (!cols.Any())
                return;

            var tableGrid = new Grid();
            //tableGrid.ShowGridLines = true;

            var style = htmlNode.Attributes["style"];
            if (style != null)
            {
                var cssDic = RichTextboxStyle.GetCssStyle(style.Value);
                if (cssDic.ContainsKey("width"))
                {
                    string width = cssDic["width"].Replace("px", "").Replace("pt", "");
                    double ww;
                    double.TryParse(width, out ww);
                    tableGrid.Width = ww;
                }
            }

            for (var i = 0; i < rows.Count(); i++)
                tableGrid.RowDefinitions.Add(new RowDefinition());
            for (var i = 0; i < cols.Count(); i++)
                tableGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (var i = 0; i < rows.Count(); i++)
            {
                for (var j = 0; j < cols.Count(); j++)
                {
                    try
                    {
                        var tb = new TextBlock();
                        var htmlNodeCol = htmlNode
                            .ChildNodes.Where(a => a.Name.ToLower() == "tr").ElementAt(i)
                            .ChildNodes.Where(a1 => a1.Name.ToLower() == "td").ElementAt(j);
                        tb.Text = htmlNodeCol.InnerText;
                        tb.SetValue(Grid.RowProperty, i);
                        tb.SetValue(Grid.ColumnProperty, j);
                        tableGrid.Children.Add(tb);
                    }
                    catch
                    { }
                }
            }

            var ilContainer = new InlineUIContainer {Child = tableGrid};
            (block as Paragraph).Inlines.Add(ilContainer);
        }
    }
}
