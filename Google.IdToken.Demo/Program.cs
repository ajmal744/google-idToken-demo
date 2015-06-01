using System;
using System.IO;
using System.Text;
using Google.IdToken.Core;

namespace Google.IdToken.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the idToken retrieved from OAuth Playground.");
            var idToken = ReadLine();

            try
            {
                string idTokenInfoByDebugUrl = (new ExtractTokenInfoByUrl()).Extract(idToken);
                string idTokenInfoByDecodingTokenPart = (new ExtractTokenInfoByDecodingSectionPart()).Extract(idToken);
                Console.Clear();
                Console.WriteLine("Id Token info extracted using debugging endPoint 'https://www.googleapis.com/oauth2/v1/tokeninfo'.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(idTokenInfoByDebugUrl);
                Console.ResetColor();

                Console.WriteLine("Id Token info extracted by decoding token payload section part");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(idTokenInfoByDecodingTokenPart);
                Console.ResetColor();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Unexpected error occured. Exception Message: " + exception.Message);
            }

            Console.Write("Please enter any key to exit.");
            Console.ReadKey();
        }

        private static string ReadLine()
        {
            Stream inputStream = Console.OpenStandardInput(650);
            byte[] bytes = new byte[650];
            int outputLength = inputStream.Read(bytes, 0, 650);
            char[] chars = Encoding.UTF7.GetChars(bytes, 0, outputLength);
            return new string(chars);
        }
    }
}
