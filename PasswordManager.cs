using System;
using System.Text;
using SplashKitSDK;
#nullable disable

namespace PasswordManager2
{
    public class PasswordManager
    {
        private Window _Window;
        private PasswordGenerator _PwGenerator;
        private DatabaseManager _DatabaseManager;

        private List<Button> _Buttons = new List<Button>();

        private string _Password;
        private string _SiteName;
        private string _Email;
        private string _Username;

        private Rectangle _ConfirmationRec;

        public PasswordManager(Window window)
        {
            _Window = window;
            _PwGenerator = new PasswordGenerator();
            _DatabaseManager = new DatabaseManager();

            _Buttons.Add(new ButtonGenerate(_PwGenerator, window) { X = 20, Y = 510 });
            _Buttons.Add(new ButtonInput(window) { X = 220, Y = 510 });
            _Buttons.Add(new ButtonSave(_DatabaseManager, window) { X = 420, Y = 510 });
            _Buttons.Add(new ButtonRetrieve(_DatabaseManager, window) { X = 20, Y = 555 });
            _Buttons.Add(new ButtonUpdate(_DatabaseManager, window) { X = 220, Y = 555 });
            _Buttons.Add(new ButtonDelete(_DatabaseManager, window) { X = 420, Y = 555 });

            _ConfirmationRec = new Rectangle() { X = 20, Y = 435, Width = 580, Height = 35 };
        }

        public void ReadDatabase()
        {
            _DatabaseManager.ReadDatabase();
        }

        public Button GetButton(string name)
        {
            foreach (Button button in _Buttons)
            {
                if (button.Caption.ToLower() == name.ToLower())
                {
                    return button;
                }
            }

            return null;
        }

        public void UpdateCreds()
        {
            Button buttonGenerate = GetButton("Generate Password");
            Button buttonInput = GetButton("Enter S-E-U");

            _Password = buttonGenerate.Password;
            _SiteName = buttonInput.SiteName;
            _Email = buttonInput.Email;
            _Username = buttonInput.Username;
        }

        public void UpdateSaveCreds()
        {
            Button buttonSave = GetButton("Save Data");

            buttonSave.Password = _Password;
            buttonSave.SiteName = _SiteName;
            buttonSave.Email = _Email;
            buttonSave.Username = _Username;
        }

        public void ExecuteButton()
        {
            foreach (Button button in _Buttons)
            {
                button.Execute();
            }
        }

        public void DrawButton()
        {
            foreach (Button button in _Buttons)
            {
                button.Draw();
            }
        }

        public void DrawInterface()
        {
            _Window.FillRectangle(Color.White, 20, 30, 150, 30);
            _Window.DrawText("Password:", Color.Black, "Aller_Rg.ttf", 20, 30, 30);

            _Window.FillRectangle(Color.White, 20, 80, 150, 30);
            _Window.DrawText("Site Name:", Color.Black, "Aller_Rg.ttf", 20, 30, 80);

            _Window.FillRectangle(Color.White, 20, 130, 150, 30);
            _Window.DrawText("Email:", Color.Black, "Aller_Rg.ttf", 20, 30, 130);

            _Window.FillRectangle(Color.White, 20, 180, 150, 30);
            _Window.DrawText("Username:", Color.Black, "Aller_Rg.ttf", 20, 30, 180);

            _Window.FillRectangle(Color.White, 190, 30, 300, 30);
            _Window.FillRectangle(Color.White, 190, 80, 300, 30);
            _Window.FillRectangle(Color.White, 190, 130, 300, 30);
            _Window.FillRectangle(Color.White, 190, 180, 300, 30);
        }

        public void DrawInputs()
        {
            _Window.DrawText(_Password, Color.Black, "Aller_Rg.ttf", 20, 200, 30);
            _Window.DrawText(_SiteName, Color.Black, "Aller_Rg.ttf", 20, 200, 80);
            _Window.DrawText(_Email, Color.Black, "Aller_Rg.ttf", 20, 200, 130);
            _Window.DrawText(_Username, Color.Black, "Aller_Rg.ttf", 20, 200, 180);
        }

        public void DrawRetrieves()
        {
            Button buttonRetrieve = GetButton("Retrieve Data");

            string textSiteName = "Site: " + buttonRetrieve.RetrieveArray[0];
            string textEmail = "Email: " + buttonRetrieve.RetrieveArray[1];
            string textUsername = "Username: " + buttonRetrieve.RetrieveArray[2];
            string textPassword = "Password: " + buttonRetrieve.RetrieveArray[3];

            _Window.FillRectangle(Color.White, 20, 240, 560, 80);
            _Window.DrawText("Retrieved Data:", Color.Black, "Aller_Rg.ttf", 15, 30, 240);
            _Window.DrawText(textSiteName, Color.Black, "Aller_Rg.ttf", 15, 30, 265);
            _Window.DrawText(textEmail, Color.Black, "Aller_Rg.ttf", 15, 300, 265);
            _Window.DrawText(textUsername, Color.Black, "Aller_Rg.ttf", 15, 30, 295);
            _Window.DrawText(textPassword, Color.Black, "Aller_Rg.ttf", 15, 300, 295);
        }

        public void DrawConfirmation()
        {
            _Window.FillRectangle(Color.AntiqueWhite, _ConfirmationRec);

            if (_DatabaseManager.Success)
            {
                _Window.DrawText(_DatabaseManager.confirmationMsg, Color.Green, "Aller_Rg.ttf", 16, 20, 437);

            }
            else
            {
                _Window.DrawText(_DatabaseManager.confirmationMsg, Color.Red, "Aller_Rg.ttf", 16, 20, 437);

            }
        }
    }
}

