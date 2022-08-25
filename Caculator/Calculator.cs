using System;
using System.Windows;
using NetCoreAudio;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace csharp_project
{
    class Calculator : Window
    {
        [UI] private Label entry = null;
        [UI] private Label result = null;
        [UI] private Button more = null;
        [UI] private Button less = null;
        [UI] private Button times = null;
        [UI] private Button divide = null;
        [UI] private Button equal = null;
        [UI] private Button sign = null;
        [UI] private Button point = null;
        [UI] private Button zero = null;
        [UI] private Button one = null;
        [UI] private Button two = null;
        [UI] private Button three = null;
        [UI] private Button four = null;
        [UI] private Button five = null;
        [UI] private Button six = null;
        [UI] private Button seven = null;
        [UI] private Button eight = null;
        [UI] private Button nine = null;
        [UI] private Button inverse = null;
        // [UI] private Button root = null;
        // [UI] private Button sqrt = null;
        [UI] private Button mod = null;
        [UI] private Button clearEntry = null;
        [UI] private Button clearAll = null;
        [UI] private Button del = null;

        private Player player = new NetCoreAudio.Player();

        public char[] signs = {'+', '-', 'x', '/', '%'};

        public Calculator() : this(new Builder("Calculator.glade")) {
            this.entry.Text = "";
        }

        public Calculator(Builder builder) : base(builder.GetObject("calculator").Handle)
        {
            builder.Autoconnect(this);

            one.Clicked += ButtonOne_Clicked;
            two.Clicked += ButtonTwo_Clicked;
            three.Clicked += ButtonThree_Clicked;
            four.Clicked += ButtonFour_Clicked;
            five.Clicked += ButtonFive_Clicked;
            six.Clicked += ButtonSix_Clicked;
            seven.Clicked += ButtonSeven_Clicked;
            eight.Clicked += ButtonEight_Clicked;
            nine.Clicked += ButtonNine_Clicked;
            zero.Clicked += ButtonZero_Clicked;
            point.Clicked += ButtonPoint_Clicked;

            clearEntry.Clicked += ButtonClearEntry_Clicked;
            clearAll.Clicked += ButtonClearAll_Clicked;
            del.Clicked += ButtonDel_Clicked;

            more.Clicked += ButtonMore_Clicked;
            less.Clicked += ButtonLess_Clicked;
            divide.Clicked += ButtonDivide_Clicked;
            times.Clicked += ButtonTimes_Clicked;
    	    mod.Clicked += ButtonMod_Clicked;
            equal.Clicked += ButtonEqual_Clicked;

            sign.Clicked += ButtonSign_Clicked;
            inverse.Clicked += ButtonInverse_Clicked;
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
                        player.Play("./assset/sound/alarm-police-fire-ambulance-etc-sound-effect-26-11504.mp3");
                        return "Division by 0 is impossible";
                    } else {
                        resInt /= nb;
                    }
                    break;
                case '%':
                    if (nb == 0)
                    {
                        player.Play("./assset/sound/alarm-police-fire-ambulance-etc-sound-effect-26-11504.mp3");
                        return "Division by 0 is impossible";
                    } else {
                        resInt %= nb;
                    }
                    break;
            }
            return string.Format("{0}", resInt);
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs e)
        {
            this.Destroy();
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
        private void ButtonOne_Clicked(object sender,EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "1";
        }
        private void ButtonTwo_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "2";
        }
        private void ButtonThree_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "3";
        }
        private void ButtonFour_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "4";
        }
        private void ButtonFive_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "5";
        }
        private void ButtonSix_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "6";
        }
        private void ButtonSeven_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "7";
        }
        private void ButtonEight_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "8";
        }
        private void ButtonNine_Clicked(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "9";
        }
        private void ButtonZero_Clicked(object sender,EventArgs e)
        {
            if (result.Text == "0" || result.Text == "Division by 0 is impossible")
            {
                result.Text = "";
            }
            if (entry.Text=="0")
            {
                entry.Text = "";
            }
            entry.Text += "0";
        }
        private void ButtonMore_Clicked(object sender, EventArgs e)
        {
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
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
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
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
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
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
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
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
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
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
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
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
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
            if (result.Text[0] == '-')
            {
                result.Text = result.Text.Substring(1, result.Text.Length - 1);
            } else {
                result.Text = "-" + result.Text;
            }
        }
        private void ButtonInverse_Clicked(object sender, EventArgs e)
        {
            player.Play("./assset/sound/gun-dry-firing-11-39784.mp3");
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
            player.Play("./assset/sound/bass-drop.mp3");
            entry.Text = "0";
        }
        private void ButtonClearAll_Clicked(object sender, EventArgs e)
        {
            player.Play("./assset/sound/bass-drop.mp3");
            result.Text = "0";
            entry.Text = "";
        }
        private void ButtonDel_Clicked(object sender, EventArgs e)
        {
            player.Play("./assset/sound/bass-drop.mp3");
            if (entry.Text!= "")
            {
                entry.Text = entry.Text.Substring(0, entry.Text.Length -1);
            }
        }
    }
}
