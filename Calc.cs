using System;
using System.Linq;

namespace QuestCalc
{
    class Calc
    {
        private string[] _signs = {"+", "-", "x", "/", "%"};
        public Calc(){}

        public string Operator(string value){
            float nb1 = 0, nb2 = 0, nbSign = 0;
            string op = "";
            for (int i = 0; i < _signs.Length; i++){
                if (nbSign > 1 || value.Split(_signs[i]).Length > 2)
                {
                    return "Only one operation please";
                }
                if (value.Contains(_signs[i]))
                {
                    nbSign ++;
                    op = _signs[i];
                }
                
            }
            if (op == ""){
                return "Please enter an operation";
            }
            nb1 = float.Parse(value.Substring(0, value.IndexOf(op)));
            nb2 = float.Parse(value.Substring(value.IndexOf(op)+1));
            switch (op){
                case "+": 
                    return (nb1 + nb2).ToString();
                case "-": 
                    return (nb1 - nb2).ToString();
                case "x": 
                    return (nb1 * nb2).ToString();
                case "/": 
                    if (nb2 == 0)
                    {
                        return "Division by 0 is IMPOSSIBLE";
                    }
                    return (nb1 / nb2).ToString();
                case "%": 
                    if (nb2 == 0)
                    {
                        return "Modulo by 0 is IMPOSSIBLE";
                    }
                    return (nb1 % nb2).ToString();
                default: return ("");
            }
            return "";
        }
    }
}