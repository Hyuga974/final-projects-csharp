using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace csharp_project
{
    class CountWindow : Window
    {
        [UI] private Label label1 = null;
        [UI] private Button More = null;
        [UI] private Button Less = null;

        private int _counter;

        public CountWindow() : this(new Builder("CountWindow.glade")) { }

        public CountWindow(Builder builder) : base(builder.GetObject("countWindow").Handle)
        {
            builder.Autoconnect(this);

            More.Clicked += ButtonMore_Clicked;
            Less.Clicked += ButtonLess_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            this.Destroy();
        }

        private void ButtonMore_Clicked(object sender, EventArgs a)
        {
            _counter++;
            label1.Text = "You have " + _counter + " point(s).";
        }

        private void ButtonLess_Clicked(object sender, EventArgs a)
        {
            _counter--;
            label1.Text = "You have " + _counter + " point(s).";
        }
    }
}
