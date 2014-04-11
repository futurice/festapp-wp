using System;
using System.Windows.Documents;
using HtmlAgilityPack;
using Microsoft.Phone.Tasks;

namespace BF.SL
{
    public class AToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "a"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var url = htmlNode.Attributes["href"];
            if (url == null)
                throw new Exception("請輸入href");

            RichTextboxStyle.GetDefault(htmlNode);

            var target = htmlNode.Attributes["target"];
            var h = new Hyperlink {CommandParameter = url.Value};
            if (target != null)
                h.TargetName = target.Value;
            h.Click += h_Click;
            var r = new Run();
            h.Inlines.Add(r);
            r.Text = GetCleanContent(htmlNode.InnerText);
            (block as Paragraph).Inlines.Add(h);
        }

        void h_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var rSender = sender as Hyperlink;
            var webBrowserTask = new WebBrowserTask
                                                {
                                                    Uri =
                                                        new Uri(rSender.CommandParameter.ToString())
                                                };

            webBrowserTask.Show();
        }
    }
}
