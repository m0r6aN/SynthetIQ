namespace MF.DomainName.Health.Utility
{
    public static class FileHelpers
    {
        public static string Replace(this string original, string search, string replace)
        {
            return original.Replace(search, replace);
        }

        public static string Replace(this string original, string search, string replace, StringComparison comparison)
        {
            return original.Replace(search, replace, comparison);
        }

        public static string LoadTextContent(string filePath)
        {
            string content = "";

            try
            {
                content = File.ReadAllText(filePath);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: File not found.");
            }

            return content;
        }
    }
}