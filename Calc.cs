
using System;
using System.Windows;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace CalculatorQuest
{
    class Calc : Window
    {
        [UI] public Label result = null;
        [UI] public Button more = null;
        [UI] public Button less = null;
        [UI] public Button time = null;
        [UI] public Button divide = null;
        [UI] public Button equal = null;
        [UI] public Button point = null;
        [UI] public Button zero = null;
        [UI] public Button one = null;
        [UI] public Button two = null;
        [UI] public Button three = null;
        [UI] public Button four = null;
        [UI] public Button five = null;
        [UI] public Button six = null;
        [UI] public Button seven = null;
        [UI] public Button eight = null;
        [UI] public Button nine = null;
        [UI] public Button mod = null;
        [UI] public Button clearEntry = null;
        [UI] public Button clearAll = null;
        [UI] public Button del = null;
        public Calc() : this(new Builder("CalcScreen.glade")) {
            this.result.Text = "";
        }
        public Calc(Builder builder) : base(builder.GetObject("calc").Handle){
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;

            one.Clicked += _Display;
            two.Clicked += _Display;
            three.Clicked += _Display;
            four.Clicked += _Display;
            five.Clicked += _Display;
            six.Clicked += _Display;
            seven.Clicked += _Display;
            eight.Clicked += _Display;
            nine.Clicked += _Display;
            zero.Clicked += _Display;
            point.Clicked += _Display;

            clearEntry.Clicked += _Display;
            clearAll.Clicked += _Display;
            del.Clicked += _Display;

            more.Clicked += _Display;
            less.Clicked += _Display;
            divide.Clicked += _Display;
            time.Clicked += _Display;
    	    mod.Clicked += _Display;
            equal.Clicked += _Display;
        }

        private void _Display(object sender, EventArgs e) {
            result.Text = ((Button) sender).Label;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }
}