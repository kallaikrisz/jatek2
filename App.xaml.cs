using DotNetEnv;
using System.Configuration;
using System.Data;
using System.Windows;

namespace jatek
{
    public partial class App : Application
    {
        public static string ConnStr { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // .env betöltése
            DotNetEnv.Env.Load();
            ConnStr = $"server={Env.GetString("DB_HOST")};user={Env.GetString("DB_USER")};password={Env.GetString("DB_PASS")};database={Env.GetString("DB_NAME")}";

            // Regisztrációs ablak megnyitása
            Regisztracio ablak = new Regisztracio();
            MainWindow = ablak;
            ablak.Show();
        }
    }
}
