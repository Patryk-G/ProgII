using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            double outcome = 0.0;
            string equation;
            Matxt.RPN onp = new Matxt.RPN();
            equation = args[0].Replace("x", args[1]);
            onp.Parse(equation);
            Console.WriteLine(onp.modifPhrase);
            Console.WriteLine(onp.PostFixPhrase);
            outcome = onp.evaluate();
            Console.WriteLine(outcome);
            int quantity = int.Parse(args[4]);
            double[] numberz = new double[quantity];
            numberz[0] = double.Parse(args[2]);
            numberz[quantity - 1] = double.Parse(args[3]);
            double step = (numberz[quantity - 1] - numberz[0]) / (quantity - 1);
            for (int i = 1; i < quantity - 1; i++)
            {
                numberz[i] = numberz[0] + step * i;
            }
            string[] numberz2 = new string[quantity];
            for (int i = 0; i < quantity; i++)
            {
                numberz2[i] = numberz[i].ToString();
                numberz2[i] = numberz2[i].Replace(',', '.');
                equation = args[0].Replace("x", numberz2[i]);
                onp.Parse(equation);
                outcome = onp.evaluate();
                Console.WriteLine("{0} => {1}", numberz2[i], outcome);
            }
            Console.ReadLine();
        }
    }
}