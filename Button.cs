using System;
using System.Text;
using SplashKitSDK;
#nullable disable

namespace PasswordManager2
{
    public abstract class Button
    {
        public Window _Window;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Caption { get; set; }

        public string Password { get; set; }
        public string SiteName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string[] RetrieveArray { get; set; }

        public Button(Window window)
        {
            _Window = window;
            Width = 145;
            Height = 25;
        }

        public void Draw()
        {
            SplashKit.FillRectangle(Color.LightGray, X, Y, Width, Height);
            SplashKit.DrawText(Caption, Color.Black, X + 5, Y + 5);
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle() { X = X, Y = Y, Width = Width, Height = Height };
            }
        }

        public bool IsMouseHover
        {
            get
            {
                return SplashKit.PointInRectangle(SplashKit.MousePosition(), Rectangle);
            }
        }

        public string ReadInput(string prompt)
        {
            string str;
            Rectangle rec = new Rectangle() { X = 20, Y = 340, Width = 560, Height = 30 };
            SplashKit.StartReadingText(rec);

            do
            {
                SplashKit.ProcessEvents();
                str = SplashKit.TextInput();
                _Window.FillRectangle(Color.White, rec);
                SplashKit.DrawText(prompt, Color.Black, "Aller_Rg.ttf", 20, 30, 340);
                SplashKit.DrawText(str, Color.Black, "Aller_Rg.ttf", 20, 270, 340);
                _Window.FillRectangle(Color.AntiqueWhite, 15, 378, 585, 60);
                SplashKit.DrawText("Press 'Enter' to finish Text Input", Color.Blue, "Aller_Rg.ttf", 16, 20, 380);
                SplashKit.DrawText("Then Press 'Tab' to Move to next step", Color.Blue, "Aller_Rg.ttf", 16, 20, 410);
                _Window.Refresh(60);
            } while (!SplashKit.KeyTyped(KeyCode.TabKey));

            SplashKit.EndReadingText();

            return str;
        }

        public abstract void Execute();
    }

    public class ButtonGenerate : Button
    {
        private PasswordGenerator _PwGenerator;

        public ButtonGenerate(PasswordGenerator pwGenerator, Window window) : base(window)
        {
            _PwGenerator = pwGenerator;
            Caption = "Generate Password";
        }

        public override void Execute()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseHover)
            {
                Password = _PwGenerator.GeneratePassword();
                Console.WriteLine(Password);
            }
        }
    }

    public class ButtonInput : Button
    {
        public ButtonInput(Window window) : base(window)
        {
            Caption = "Enter S-E-U";
            SiteName = "";
        }

        public override void Execute()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseHover)
            {
                SiteName = ReadInput("Enter site name: ");
                Console.WriteLine(SiteName);

                Email = ReadInput("Enter email: ");
                Console.WriteLine(Email);

                Username = ReadInput("Enter username: ");
                Console.WriteLine(Username);
            }
        }
    }

    public class ButtonSave : Button
    {
        private DatabaseManager _DatabaseManager;
        public ButtonSave(DatabaseManager databaseManager, Window window) : base(window)
        {
            _DatabaseManager = databaseManager;
            Caption = "Save Data";
        }

        public override void Execute()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseHover)
            {
                _DatabaseManager.InsertIntoDatabase(SiteName, Email, Username, Password);
            }
        }
    }

    public class ButtonRetrieve : Button
    {
        private DatabaseManager _DatabaseManager;
        private string _SearchSiteName;
        private string[] _RetrieveArray = { "", "", "", "" };

        public ButtonRetrieve(DatabaseManager databaseManager, Window window) : base(window)
        {
            _DatabaseManager = databaseManager;
            RetrieveArray = _RetrieveArray;
            Caption = "Retrieve Data";
        }

        public override void Execute()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseHover)
            {
                string[] outArr;
                _SearchSiteName = ReadInput("Enter site name: ");
                outArr = _DatabaseManager.SearchData(_SearchSiteName);

                RetrieveArray[0] = outArr[0];
                RetrieveArray[1] = outArr[1];
                RetrieveArray[2] = outArr[2];
                RetrieveArray[3] = outArr[3];
            }
        }
    }

    public class ButtonUpdate : Button
    {
        private DatabaseManager _DatabaseManager;
        private string _UpdateSiteName;
        private string _UpdatePassword;

        public ButtonUpdate(DatabaseManager databaseManager, Window window) : base(window)
        {
            _DatabaseManager = databaseManager;
            Caption = "Update Password";
        }

        public override void Execute()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseHover)
            {
                _UpdateSiteName = ReadInput("Enter site name: ");
                _UpdatePassword = ReadInput("Enter new password: ");

                _DatabaseManager.UpdateData(_UpdateSiteName, _UpdatePassword);
            }
        }
    }

    public class ButtonDelete : Button
    {
        private DatabaseManager _DatabaseManager;
        private string _DeleteSiteName;

        public ButtonDelete(DatabaseManager databaseManager, Window window) : base(window)
        {
            _DatabaseManager = databaseManager;
            Caption = "Delete Data";
        }

        public override void Execute()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseHover)
            {
                _DeleteSiteName = ReadInput("Enter site name: ");

                _DatabaseManager.DeleteData(_DeleteSiteName);
            }
        }
    }
}

