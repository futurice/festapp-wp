using System;
using System.Windows.Documents;
using HtmlAgilityPack;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BF.SL
{
    public class ImageToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "img"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            var src = htmlNode.Attributes["src"];
            if (src == null)
                throw new Exception("請輸入src");

            var ilContainer = new InlineUIContainer();
            var alt = htmlNode.Attributes["alt"];

            var img = new Image();
            ImageSource imgSource = new BitmapImage(new Uri(src.Value, UriKind.RelativeOrAbsolute));
            img.Source = imgSource;
            img.Stretch = Stretch.None;
            if (alt != null && !string.IsNullOrEmpty(alt.Value))
                ToolTipService.SetToolTip(img, alt);
            ilContainer.Child = img;
            (block as Paragraph).Inlines.Add(ilContainer);
        }
    }
}
