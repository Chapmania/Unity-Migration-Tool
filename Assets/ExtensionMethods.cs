using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static string GetName(this YamlDocument document)
        {
            return (string) ((YamlMappingNode) document.RootNode).Children.First().Key;
        }

        public static IDictionary<YamlNode, YamlNode> GetChildren(this YamlNode node)
        {
            return ((YamlMappingNode) node).Children;
        }
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}