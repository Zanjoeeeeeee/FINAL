using System;
using System.Windows.Forms;

namespace DriveMate
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                Database.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not connect to SQL Server.\n\nCheck the 'Server' value in Database.cs " +
                    "(default is LocalDB: (localdb)\\MSSQLLocalDB).\n\n" + ex.Message,
                    "DriveMate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            while (true)
            {
                using var login = new LoginForm();
                if (login.ShowDialog() != DialogResult.OK || login.User is null) break;

                using var main = new MainForm(login.User);
                Application.Run(main);
                if (!main.LoggedOut) break;
            }
        }
    }
}