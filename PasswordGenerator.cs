using System;
using System.Text;
using SplashKitSDK;
#nullable disable

namespace PasswordManager2
{
    public class PasswordGenerator
    {
        public PasswordGenerator()
        {

        }

        public string GeneratePassword()
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numericChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+";
            const string allChars = lowercaseChars + uppercaseChars + numericChars + specialChars;
            
            StringBuilder password = new StringBuilder();
            Random random = new Random();
            int length = random.Next(11,17);

            password.Append(lowercaseChars[random.Next(lowercaseChars.Length)]);
            password.Append(uppercaseChars[random.Next(uppercaseChars.Length)]);
            password.Append(numericChars[random.Next(numericChars.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            for (int i = 4; i < length; i++)
            {
                int index = random.Next(allChars.Length);
                password.Append(allChars[index]);
            }

            string shuffledPassword = ShuffleString(password.ToString());

            return shuffledPassword;
        }

        public string ShuffleString(string str)
        {
            char[] charArray = str.ToCharArray();
            Random random = new Random();
            int length = charArray.Length;

            for (int i = 0; i < length; i++)
            {
                int randIndex = random.Next(length);
                char temp = charArray[i];
                charArray[i] = charArray[randIndex];
                charArray[randIndex] = temp;
            }

            return new string(charArray);
        }
    }
}

