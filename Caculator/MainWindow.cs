using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace csharp_project
{
    class MainWindow : Window
    {
        [UI] private Label _label1 = null;
        [UI] private Button _button1 = null;
        [UI] private Button calc = null;

        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        public MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
            calc.Clicked += Calc_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {
            _counter++;
            Console.WriteLine("Here");
            _label1.Text = "Hello Antho, ça marche !!! This button has been clicked " + _counter + " time(s).";
            CountWindow test = new CountWindow();
            test.Show();
        }
        private void Calc_Clicked(object sender, EventArgs a)
        {
            Calculator win = new Calculator();
            win.Show();
        }
    }
}
