using System;

namespace QuestCalc
{
    class Program
    {
        static void Main(string[] str)
        {
            Calc c = new Calc();
            Console.WriteLine(c.Operator("18+7"));  
            Console.WriteLine(c.Operator("100%3"));
            Console.WriteLine(c.Operator("73/0"));
            Console.WriteLine(c.Operator(""));
            Console.WriteLine(c.Operator("18+32/5%3.14"));
        }
        
    }
    
}
