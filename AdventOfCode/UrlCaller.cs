using System.IO;
using System.Net;

namespace AdventOfCode
{
    class UrlCaller
    {
        public static string GetTextInput(int day)
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://adventofcode.com/day/{day}/input");
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("session", "", "/", "adventofcode.com"));
            using (var response = request.GetResponse())
            {
                var input = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return input;
            }
        }
    }
}
