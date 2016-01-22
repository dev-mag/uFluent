
namespace uFluentExample728.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncate a string to cut off at a whole word
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int length)
        {
            if(!string.IsNullOrEmpty(value) && value.Length > length)
            {
                value = value.Substring(0, length);
                int nextSpace = value.LastIndexOf(" ", length);
                value = value.Substring(0, (nextSpace > 0) ? nextSpace : length).Trim(new char[] { ',', ' ' });
            }

            return value;
        }
    }
}