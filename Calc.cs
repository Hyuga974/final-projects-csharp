
using System;
using System.Windows;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace CalculatorQuest
{
    class Calc : Window
    {
        [UI] public Label result, entry;
        [UI] public Button more, less, time, divide, mod, equal, sign = null;
        [UI] public Button zero, one, two, three, four, five, six, seven, eight, nine, point = null;
        [UI] public Button clearEntry, clearAll, del;

        private char[] signs = {'+', '-', 'x', '/', '%'};
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
            point.Clicked += ButtonPoint_Clicked;

            clearEntry.Clicked += ButtonClearEntry_Clicked;
            clearAll.Clicked += ButtonClearAll_Clicked;
            del.Clicked += ButtonDel_Clicked;

            more.Clicked += ButtonMore_Clicked;
            less.Clicked += ButtonLess_Clicked;
            divide.Clicked += ButtonDivide_Clicked;
            time.Clicked += ButtonTimes_Clicked;
    	    mod.Clicked += ButtonMod_Clicked;
            equal.Clicked += ButtonEqual_Clicked;
            sign.Clicked += ButtonSign_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
        private void _Display(object sender, EventArgs e) {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += ((Button) sender).Label;
        }


        private bool CheckLastIsSign(string value)
        {
            if (value.Length <= 1) {
                return false;
            }
            for (int i = 0; i < signs.Length; i++)
            {
                if (value[value.Length - 1] == signs[i])
                {
                    return true;
                }
            }
            return false;
        }
        private string Res(string value)
        {
            if (value == "")
            {
                return "";
            }

            int position = -1;
            for (int i = 0; i < signs.Length; i++)
            {
                if (value.Contains(signs[i]))
                {
                    position = value.IndexOf(signs[i]);
                    break;
                }
            }
            if (CheckLastIsSign(value))
            {
                value += entry.Text;
            }
            if (value == "0")
            {
                return entry.Text;
            }
            
            if (position == value.Length - 1 || position == -1)
            {
                return value;
            }
            float resInt = float.Parse(value.Substring(0, position));
            float nb = float.Parse(value.Substring(position + 1));
            switch (value[position])
            {
                case '+':
                    resInt += nb;
                    break;
                case '-':
                    resInt -= nb;
                    break;
                case 'x':
                    resInt *= nb;
                    break;
                case '/':
                    if (nb == 0)
                    {
                        return "Division by 0 is impossible";
                    } else {
                        resInt /= nb;
                    }
                    break;
                case '%':
                    if (nb == 0)
                    {
                        return "Division by 0 is impossible";
                    } else {
                        resInt %= nb;
                    }
                    break;
            }
            return string.Format("{0}", resInt);
        }

        private void ButtonPoint_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="")
            {
                entry.Text = "";
            }

            if (entry.Text.Length>0 && !entry.Text.Contains(","))
            {
                entry.Text += ",";
            }
        }
        private void ButtonMore_Clicked(object sender, EventArgs e)
        {
            result.Text += entry.Text;
            if (CheckLastIsSign(result.Text) && result.Text.Length > 1)
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
            }
            
            string res = Res(result.Text);
            if (res != ""){
                result.Text = res;
            }
            result.Text += "+";
            entry.Text = "";
            
        }

        private void ButtonLess_Clicked(object sender, EventArgs e)
        {
            result.Text += entry.Text;
            if (CheckLastIsSign(result.Text) && result.Text.Length > 1)
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
            }
            
            string res = Res(result.Text);
            if (res != ""){
                result.Text = res;
            }
            result.Text += "-";
            entry.Text = "";
        }
        private void ButtonDivide_Clicked(object sender, EventArgs e)
        {
            result.Text += entry.Text;
            if (CheckLastIsSign(result.Text) && result.Text.Length > 1)
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
            }
            
            string res = Res(result.Text);
            if (res != ""){
                result.Text = res;
            }
            result.Text += "/";
            entry.Text = "";
        }
        private void ButtonTimes_Clicked(object sender, EventArgs e)
        {
            result.Text += entry.Text;
            if (CheckLastIsSign(result.Text) && result.Text.Length > 1)
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
            }
            
            string res = Res(result.Text);
            if (res != ""){
                result.Text = res;
            }
            result.Text += "x";
            entry.Text = "";
        }
        private void ButtonMod_Clicked(object sender, EventArgs e)
        {
            result.Text += entry.Text;
            if (CheckLastIsSign(result.Text) && result.Text.Length > 1)
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
            }
            
            string res = Res(result.Text);
            if (res != ""){
                result.Text = res;
            }
            result.Text += "%";
            entry.Text = "";
        }
        private void ButtonEqual_Clicked(object sender, EventArgs e)
        {
            if (!CheckLastIsSign(result.Text))
            {
                result.Text = entry.Text;
            } else {
                if (CheckLastIsSign(result.Text))
                {
                    result.Text += entry.Text;
                }
                result.Text = Res(result.Text);
            }
            
            entry.Text = "";
        }
        
        private void ButtonSign_Clicked(object sender, EventArgs e)
        {
            if (result.Text[0] == '-')
            {
                result.Text = result.Text.Substring(1, result.Text.Length - 1);
            } else {
                result.Text = "-" + result.Text;
            }
        }
        private void ButtonInverse_Clicked(object sender, EventArgs e)
        {
            if (entry.Text.Contains("/"))
            {
                entry.Text = Res(entry.Text);
                result.Text = "";
            }
            if (entry.Text == "")
            {
                result.Text = Res("1/" + result.Text);
            } else {
                entry.Text = "1/" + entry.Text;
                result.Text += Res(entry.Text);
            }
            entry.Text = "";
        }
        private void ButtonClearEntry_Clicked(object sender, EventArgs e)
        {
            entry.Text = "0";
        }
        private void ButtonClearAll_Clicked(object sender, EventArgs e)
        {
            result.Text = "0";
            entry.Text = "";
        }
        private void ButtonDel_Clicked(object sender, EventArgs e)
        {
            if (entry.Text!= "")
            {
                entry.Text = entry.Text.Substring(0, entry.Text.Length -1);
            }
        }


    }
}