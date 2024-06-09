using System;
using System.Text;
using SplashKitSDK;
#nullable disable

namespace PasswordManager2
{
    public class DatabaseManager
    {
        private string[] _OutArr = { "", "", "", "" };
        private Database _PasswordManagerDB;
        public bool Success { get; set; }
        public string confirmationMsg { get; set; }

        public DatabaseManager()
        {
            _PasswordManagerDB = SplashKit.OpenDatabase("PasswordManagerDB", "PasswordManagerDB");
            _PasswordManagerDB.RunSql("CREATE TABLE IF NOT EXISTS PasswordManager (siteName TEXT PRIMARY KEY, email TEXT, username TEXT, password TEXT);");

            Success = false;
            confirmationMsg = "";
        }

        public void InsertIntoDatabase(string siteName, string email, string username, string password)
        {
            QueryResult result;
            string sqlStatement = "INSERT INTO PasswordManager VALUES (\'" + siteName + "\', \'" + email + "\', \'" + username + "\', \'" + password + "\');";

            if (siteName.Length > 0)
            {
                result = _PasswordManagerDB.RunSql(sqlStatement);
                Success = result.Successful;
                Console.WriteLine(result.Successful);
            }
            else
            {
                Success = false;
            }

            Console.WriteLine(Success);

            if (Success)
            {
                confirmationMsg = "Saving is successful";
            }
            else
            {
                confirmationMsg = "Saving is unsuccessful. Enter all values.";
            }
        }

        public void ReadDatabase()
        {
            QueryResult result;
            result = _PasswordManagerDB.RunSql("SELECT * FROM PasswordManager;");
            Console.WriteLine(result.Successful);

            do
            {
                string siteName = result.QueryColumnForString(0);
                string email = result.QueryColumnForString(1);
                string username = result.QueryColumnForString(2);
                string password = result.QueryColumnForString(3);

                Console.WriteLine($"siteName: {siteName} email: {email} username: {username} password: {password}");

            } while (SplashKit.GetNextRow(result));
        }

        public string[] SearchData(string searchSiteName)
        {
            QueryResult result;
            string sqlStatement = "SELECT * FROM PasswordManager WHERE siteName = \'" + searchSiteName + "\';";
            result = _PasswordManagerDB.RunSql(sqlStatement);

            string outSiteName;
            string outEmail;
            string outUsername;
            string outPassword;

            outSiteName = result.QueryColumnForString(0);
            outEmail = result.QueryColumnForString(1);
            outUsername = result.QueryColumnForString(2);
            outPassword = result.QueryColumnForString(3);

            if (outSiteName.Length > 0)
            {
                _OutArr[0] = outSiteName;
                _OutArr[1] = outEmail;
                _OutArr[2] = outUsername;
                _OutArr[3] = outPassword;

                Success = true;
            }
            else
            {
                Success = false;
            }

            Console.WriteLine(Success);

            if (Success)
            {
                confirmationMsg = "Retrieve is successful";
            }
            else
            {
                confirmationMsg = "Retrieve is unsuccessful. Enter accurate site Name.";
            }

            return _OutArr;
        }

        public void UpdateData(string upSiteName, string upPassword)
        {
            QueryResult result1;
            QueryResult result2;

            string sqlStatement1 = "SELECT * FROM PasswordManager WHERE siteName = \'" + upSiteName + "\';";
            string sqlStatement2 = "UPDATE PasswordManager SET password = \'" + upPassword + "\' WHERE siteName = \'" + upSiteName + "\';";

            if (upSiteName.Length > 0)
            {
                result1 = _PasswordManagerDB.RunSql(sqlStatement1);

                if (result1.QueryColumnForString(0).Length > 0)
                {
                    result2 = _PasswordManagerDB.RunSql(sqlStatement2);
                    Success = result2.Successful;
                }
                else
                {
                    Success = false;
                }
            }
            else
            {
                Success = false;
            }

            Console.WriteLine(Success);

            if (Success)
            {
                confirmationMsg = "Update is successful";
            }
            else
            {
                confirmationMsg = "Update is unsuccessful. Enter accurate site name.";
            }
        }

        public void DeleteData(string delSiteName)
        {
            QueryResult result1;
            QueryResult result2;

            string sqlStatement1 = "SELECT * FROM PasswordManager WHERE siteName = \'" + delSiteName + "\';";
            string sqlStatement2 = "DELETE FROM PasswordManager WHERE siteName = \'" + delSiteName + "\';";

            if (delSiteName.Length > 0)
            {
                result1 = _PasswordManagerDB.RunSql(sqlStatement1);

                if (result1.QueryColumnForString(0).Length > 0)
                {
                    result2 = _PasswordManagerDB.RunSql(sqlStatement2);
                    Success = result2.Successful;
                }
                else
                {
                    Success = false;
                }
            }
            else
            {
                Success = false;
            }

            Console.WriteLine(Success);

            if (Success)
            {
                confirmationMsg = "Deletion is successful";
            }
            else
            {
                confirmationMsg = "Deletion is unsuccessful. Enter accurate site name.";
            }
        }
    }
}

