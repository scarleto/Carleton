using System.Security.Cryptography;
using System.Text;

namespace Carleton.API.Helpers
{
    public static class RandomNumber
    {
        public static string Create()
        {
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string s = x.ToString("000000");
            return s;
        }
    }
}
