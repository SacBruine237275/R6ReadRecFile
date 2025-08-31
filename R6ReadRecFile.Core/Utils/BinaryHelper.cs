using System.Text;

namespace R6ReadRecFile.Core.Utils
{
    public class BinaryHelper
    {
        public static IEnumerable<string> ExtractStrings(byte[] data, int minLength = 3)
        {
            var results = new List<string>();
            var sb = new StringBuilder();

            foreach (byte b in data)
            {
                if (b >= 32 && b <= 126) // ASCII imprimable
                {
                    sb.Append((char)b);
                }
                else
                {
                    if (sb.Length >= minLength)
                        results.Add(sb.ToString());
                    sb.Clear();
                }
            }

            return results;
        }
    }
}
