using System.Text;

namespace LibraryManagementPortal.Utilities
{
    public static class EncryptionUtils
    {
        public static string Base64Encode(this string word)
        {
            string result = "";
            if (word != null)
            {
                var encode = Encoding.UTF8.GetBytes(word);
                result = Convert.ToBase64String(encode);
            }
            return result;
        }

        public static string Base64Decode(this string code)
        {
            string result = "";
            if (code != null)
            {
                var wordByte = System.Convert.FromBase64String(code);
                result = System.Text.Encoding.UTF8.GetString(wordByte);
            }
            return result;
        }
    }
}