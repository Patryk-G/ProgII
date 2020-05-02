using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Proj2class
{
    class RPN
    {
        private string entryPriv;
        public string entry
        {
        get { return entryPriv; }
        set { entryPriv = value; }
        }
        public RPN (string entryPriv)
        {
            entry = entryPriv;
        }
        private double variablePriv;
        public double variable
        {
            get { return variablePriv; }
            set { variablePriv = value; }
        }
        public string[] tokens(string entryPriv)
        {
        string buffer = entryPriv;
            buffer = Regex.Replace(buffer, @"(?<numer>\d+(\.\d+)?)", " ${numer} ");
            buffer = Regex.Replace(buffer, @"(?<operator>[+\-*/^()])", " ${operator} ");
            buffer = Regex.Replace(buffer, @"(?<funkcja>(abs|exp|sqrt|log|asin|sinh|sin|cosh|acos|cos|atan|tanh|tan))", " ${funkcja} ");
            buffer = Regex.Replace(buffer, @"\s+", " ").Trim();

            buffer = Regex.Replace(buffer, "-", "MINUS");
            buffer = Regex.Replace(buffer, @"(?<numer>(([)]|\d+(\.\d+)?)))\s+MINUS", "${numer} -");
            buffer = Regex.Replace(buffer, @"MINUS\s+(?<numer>(([)]|\d+(\.\d+)?)))", "-${numer}");
            string[] Tokenlist = buffer.Split(" ".ToCharArray());
            return Tokenlist;
        }
                public bool validation(string[] TokenList)
        {
            string lastToken = "";
            int Lparat = 0, Pparat = 0;
            if (op(TokenList.Last())) return false;
            foreach (string a in TokenList)
            {
                if (!Regex.IsMatch(a, @"[a-zA-Z\d\(\)]+$") && !op(a))
                {
                    return false;
                }
                if (Regex.IsMatch(a, @"^[a-zA-Z]+$") && a != "x" && !func(a))
                {
                    return false;
                }
                if (a == "(")
                {
                    Lparat++;
                }
                if (a == ")")
                {
                    Pparat++;
                }
                if (a == ")" && Lparat < Pparat)
                {
                    return false;
                }
                if (op(a) && op(lastToken))
                {
                    return false;
                }
                if (Regex.IsMatch(a, @"\d+$") && lastToken != "" && lastToken != "(" && !func(lastToken) && !op(lastToken))
                {
                    return false;
                }
                if (a == "0" && lastToken == "/")
                {
                    return false;
                }

                lastToken = a;
            }
            return true;
        }
        static bool number(string a)
        {
            if (Regex.IsMatch(a, @"\d+|[x]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool op(string a)
        {
            if (a == "+" || a == "-" || a == "/" || a == "*" || a == "^")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool func(string a)
        {
            if ((a == "abs" || a == "exp" || a == "log" || a == "sqrt" || a == "sin" || a == "sinh" || a == "asin" || a == "cos" || a == "cosh" || a == "acos" || a == "tan" || a == "tanh" || a == "atan") || (a == "-abs" || a == "-exp" || a == "-log" || a == "-sqrt" || a == "-sin" || a == "-sinh" || a == "-asin" || a == "-cos" || a == "-cosh" || a == "-acos" || a == "-tan" || a == "-tanh" || a == "-atan"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static double PrimaryCalc(double firs, double second, string oper)
        {
            if (oper == "+")
            {
                return firs + second;
            }
            if (oper == "-")
            {
                return second - firs;
            }
            if (oper == "*")
            {
                return firs * second;
            }
            if (oper == "/")
            {
                return second / firs;
            }
            if (oper == "^")
            {
                return Math.Pow(second, firs);
            }
            throw new Exception();
        }

        static double MathFunc(string func, double val)
        {
            if (func == "abs")
            {
                return Math.Abs(val);
            }
            if (func == "exp")
            {
                return Math.Exp(val);
            }
            if (func == "log")
            {
                return Math.Log(val);
            }
            if (func == "sqrt")
            {
                return Math.Sqrt(val);
            }
            if (func == "sin")
            {
                return Math.Sin(val);
            }
            if (func == "cos")
            {
                return Math.Cos(val);
            }
            if (func == "tan")
            {
                return Math.Tan(val);
            }
            if (func == "sinh")
            {
                return Math.Sinh(val);
            }
            if (func == "cosh")
            {
                return Math.Cosh(val);
            }
            if (func == "tanh")
            {
                return Math.Tanh(val);
            }
            if (func == "asin")
            {
                return Math.Asin(val);
            }
            if (func == "acos")
            {
                return Math.Acos(val);
            }
            if (func == "atan")
            {
                return Math.Atan(val);
            }

            if (func == "-abs")
            {
                return -Math.Abs(val);
            }
            if (func == "-exp")
            {
                return -Math.Exp(val);
            }
            if (func == "-log")
            {
                return -Math.Log(val);
            }
            if (func == "-sqrt")
            {
                return -Math.Sqrt(val);
            }
            if (func == "-sin")
            {
                return -Math.Sin(val);
            }
            if (func == "-cos")
            {
                return -Math.Cos(val);
            }
            if (func == "-tan")
            {
                return -Math.Tan(val);
            }
            if (func == "-sinh")
            {
                return -Math.Sinh(val);
            }
            if (func == "-cosh")
            {
                return -Math.Cosh(val);
            }
            if (func == "-tanh")
            {
                return -Math.Tanh(val);
            }
            if (func == "-asin")
            {
                return -Math.Asin(val);
            }
            if (func == "-acos")
            {
                return -Math.Acos(val);
            }
            if (func == "-atan")
            {
                return -Math.Atan(val);
            }
            throw new Exception();
        }
    
        
        public List<string> InfixToPostfix(string[] TokenList)
        {
            Stack<string> s = new Stack<string>();
            Queue<string> q = new Queue<string>();
            foreach (string a in TokenList)
            {
                if (a == "(")
                {
                    s.Push(a);
                }
                else if (a == ")")
                {
                    while (s.Peek() != "(")
                    {
                        q.Enqueue(s.Pop());
                    }
                    s.Pop();
                }
                else if (op(a) || func(a))
                {
                    while (s.Count > 0 && Prio(a) <= Prio(s.Peek()))
                    {
                        q.Enqueue(s.Pop());
                    }
                    s.Push(a);
                }
                if (number(a))
                {
                    q.Enqueue(a);
                }
            }
            while (s.Count > 0)
            {
                q.Enqueue(s.Pop());
            }
            var list = q.ToList();
            return list;
        }

        public double InfixToPostfix_calc(List<string> postfix)
        {
            Stack<string> s = new Stack<string>();
            foreach (string p in postfix)
            {
                if (number(p))
                {
                    if (p == "x") s.Push(variable.ToString());
                    else if (p == "-x") s.Push((variable * -1).ToString());
                    else s.Push(p);
                }
                else if (func(p))
                {
                    double val;
                    val = (System.Convert.ToDouble(s.Pop()));
                    double summary = MathFunc(p, val);
                    if (!number(summary.ToString())) throw new Exception("domain of definition");
                    else s.Push(summary.ToString());
                }
                else if (op(p))
                {
                    double a, b;
                    a = System.Convert.ToDouble(s.Pop(), System.Globalization.CultureInfo.InvariantCulture);
                    b = System.Convert.ToDouble(s.Pop(), System.Globalization.CultureInfo.InvariantCulture);
                    s.Push(PrimaryCalc(a, b, p).ToString());
                }
            }
            double result;
            result = System.Convert.ToDouble(s.Pop());
            return result;
        }
        public double[,] compartments(List<string> postfix, double from, double to, int quan)
        {
            double[,] results = new double[2, quan];
            double add = (to - from) / (quan - 1);
            variablePriv = from;
            for (int i = 0; i < quan; i++)
            {
                results[0, i] = variablePriv;
                results[1, i] = InfixToPostfix_calc(postfix);
                variablePriv += add;
            }
            return results;
        }

        public int Prio(string a)
        {
            if (func(a))
            { return 4;}
            else if (a == "^") {return 3;}
            else if (a == "*" || a == "/") {return 2;}
            else if (a == "+" || a == "-") {return 1;}
            else {return 0;}
        }
    }
}