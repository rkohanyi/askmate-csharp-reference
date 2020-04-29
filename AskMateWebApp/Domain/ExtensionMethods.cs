using System.Linq;

namespace AskMateWebApp.Domain
{
    public static class ExtensionMethods
    {
        public static string ToCamelCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
