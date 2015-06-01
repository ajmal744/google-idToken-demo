using System;
using System.Text;

namespace Google.IdToken.Core
{
    public class ExtractTokenInfoByDecodingSectionPart : ITokenInfoExtractor
    {
        public static Encoding TextEncoding = Encoding.UTF8;

        private static char Base64PadCharacter = '=';
        private static char Base64Character62 = '+';
        private static char Base64Character63 = '/';
        private static char Base64UrlCharacter62 = '-';
        private static char Base64UrlCharacter63 = '_';

        public string Extract(string idToken)
        {
            if (string.IsNullOrEmpty(idToken))
            {
                throw new ArgumentNullException("idToken", "Id token can not be null or empty.");    
            }

            var tokenSplits = idToken.Split(new char[] { '.' });
            var payload = tokenSplits[1];
            return Base64Decode(payload);
        }

        private static byte[] DecodeBytes(string arg)
        {
            if (String.IsNullOrEmpty(arg))
            {
                throw new ApplicationException("String to decode cannot be null or empty.");
            }

            var stringBuilder = new StringBuilder(arg);
            stringBuilder.Replace(Base64UrlCharacter62, Base64Character62);
            stringBuilder.Replace(Base64UrlCharacter63, Base64Character63);

            int pad = stringBuilder.Length % 4;
            stringBuilder.Append(Base64PadCharacter, (pad == 0) ? 0 : 4 - pad);

            return Convert.FromBase64String(stringBuilder.ToString());
        }

        public static string Base64Decode(string arg)
        {
            var decodedBytes = DecodeBytes(arg);
            var jsonBody =  TextEncoding.GetString(decodedBytes);
            return jsonBody;
        }

    }
}
