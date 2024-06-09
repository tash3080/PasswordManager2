using System;
using System.Text;
using SplashKitSDK;
#nullable disable

namespace PasswordManager2
{
    public class Program
    {
        public static void Main()
        {
            Window window = new Window("Password Manager", 600, 600);
            PasswordManager pwManager = new PasswordManager(window);

            pwManager.ReadDatabase();

            while (!window.CloseRequested)
            {
                SplashKit.ProcessEvents();

                pwManager.UpdateCreds();
                pwManager.UpdateSaveCreds();
                pwManager.ExecuteButton();

                window.Clear(Color.AntiqueWhite);

                pwManager.DrawInterface();
                pwManager.DrawButton();
                pwManager.DrawInputs();
                pwManager.DrawRetrieves();
                pwManager.DrawConfirmation();

                window.Refresh(60);
            }
        }
    }
}

