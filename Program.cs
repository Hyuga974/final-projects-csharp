using System;
using Gtk;

namespace CalculatorQuest
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.csharp_project.csharp_project", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new Calc();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
