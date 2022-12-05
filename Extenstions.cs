using System.Net;

namespace AdventOfCode;

public static class Extenstions
{
    public static CookieContainer ToCookieContainer<T>(this IEnumerable<T> enumeration, Func<T, string> getName, Func<T, string> getValue, Uri uri)
    {
        var container = new CookieContainer();
        foreach(var item in enumeration)
        {
            var name = getName(item);
            var value = getValue(item);
            
            container.Add(new Cookie(name, value) { Domain = uri.Host });
        }

        return container;
    }
}