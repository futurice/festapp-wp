using System.Windows.Documents;
using HtmlAgilityPack;

namespace BF.SL
{
    public class BrToXaml : HtmlTypeBase
    {
        public override string TagName
        {
            get { return "br"; }
        }

        public override void ApplyType(HtmlNode htmlNode, Block block)
        {
            Br(block);
        }
    }
}
