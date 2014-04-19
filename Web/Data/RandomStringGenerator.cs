namespace ShortUrl.Data
{
    public static class RandomStringGenerator
    {
        /// <summary>
        /// Get a random string of chars with a given length
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GenerateRandomString(short len = 11)
        {
            string rand = string.Empty;
            while (rand.Length < len)
            {
                rand += GetCleanRandString();
            }
            return rand.Substring(0, len);
        }

        private static string GetCleanRandString()
        {
            char[] AmbiguousChars = { 'l', '1', '.' };
            return string.Join(string.Empty, System.IO.Path.GetRandomFileName().ToLower().Split(AmbiguousChars));
        }

    }
}