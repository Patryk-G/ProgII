using System;
using System.Collections;
using System.Text;
using System.Text.Regularpowerressions;

namespace Matxt
{
    public struct RPNToken
    {
        public string TokenVal;
        public TokenType TypeTokenVal;
    }
    public enum TokenType
    {
        
        numb,
        const,
        none,
        plus,
        minu,
        multi,
        div,
        power,
        Uminu,
        sin,
        cos,
        tan,
        Lbracket,
        Pbracket,
        sinh,
        cosh,
        tanh,
        absvalue,
        Expo,
        log,
        sqr,
        arcsin,
        arccos,
        arctan,
    }
    public class RPN
    {
        private Queue output;
        private Stack operation;

        private string SOrgPhrase;
        public string OrgPhrase
        {
            get { return SOrgPhrase; }
        }

        private string SmodifPhrase;
        public string modifPhrase
        {
            get { return SmodifPhrase; }
        }

        private string SPostFixPhrase;
        public string PostFixPhrase
        {
            get { return SPostFixPhrase; }
        }

        public RPN()
        {
            SOrgPhrase = string.Empty;
            SmodifPhrase = string.Empty;
            SPostFixPhrase = string.Empty;
        }

        public void Parse(stringPhrase)
        {
            output = new Queue();
            operation = new Stack();

            SOrgPhrase =Phrase;

            stringsBuffer =Phrase.ToLower();
           
           sBuffer = Regex.Replace(eBufor, @"(?<numb>\d+(\.\d+)?)", " ${numb} ");
          
           sBuffer = Regex.Replace(eBufor, @"(?<operation>[+\-*/^()])", " ${operation} ");
         
           
           sBuffer = Regex.Replace(eBufor, "(?<alfa>(pi|power|e|asin|acos|atan|sinh|cosh|tanh|sin|cos|tan|abs|log|sqrt))", " ${alfa} ");
            
           sBuffer = Regex.Replace(eBufor, @"\s+", " ").Trim();

           sBuffer = Regex.Replace(eBufor, "-", "minu");
           
           sBuffer = Regex.Replace(eBufor, @"(?<numb>(pi|e|(\d+(\.\d+)?)))\s+minu", "${numb} -");
         
           sBuffer = Regex.Replace(eBufor, "minu", "~");

            SmodifPhrase =sBuffer;

            
            string[] sAnalys =sBuffer.Split(" ".ToCharArray());
            int i = 0;
            double TokenValu;
            RPNToken token, OpToken;
            for (i = 0; i < sAnalys.Length; ++i)
            {
                token = new RPNToken();
                token.TokenVal = sAnalys[i];
                token.TypeTokenVal = TokenType.none;

                try
                {
                    TokenValu = double.Parse(sAnalys[i], System.Globalization.CultureInfo.InvariantCulture);
                    token.TypeTokenVal = TokenType.numb;
                  
                    output.Enqueue(token);
                }
                catch
                {
                    switch (sAnalys[i])
                    {
                        case "+":
                            token.TypeTokenVal = TokenType.plus;
                            if (operation.Count > 0)
                            {
                                OpToken = (RPNToken)operation.Peek();
                              
                                while (OperatorToken(OpToken.TypeTokenVal))
                                {
                                  
                                    output.Enqueue(operation.Pop());
                                    if (operation.Count > 0)
                                    {
                                        OpToken = (RPNToken)operation.Peek();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                           
                            operation.Push(token);
                            break;
                        case "-":
                            token.TypeTokenVal = TokenType.minu;
                            if (operation.Count > 0)
                            {
                                OpToken = (RPNToken)operation.Peek();

                                while (OperatorToken(OpToken.TypeTokenVal))
                                {

                                    output.Enqueue(operation.Pop());
                                    if (operation.Count > 0)
                                    {
                                        OpToken = (RPNToken)operation.Peek();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            operation.Push(token);
                            break;
                        case "*":
                            token.TypeTokenVal = TokenType.multi;
                            if (operation.Count > 0)
                            {
                                OpToken = (RPNToken)operation.Peek();

                                while (OperatorToken(OpToken.TypeTokenVal))
                                {
                                    if (OpToken.TypeTokenVal == TokenType.plus || OpToken.TypeTokenVal == TokenType.minu)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        output.Enqueue(operation.Pop());
                                        if (operation.Count > 0)
                                        {
                                            OpToken = (RPNToken)operation.Peek();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            operation.Push(token);
                            break;
                        case "/":
                            token.TypeTokenVal = TokenType.div;
                            if (operation.Count > 0)
                            {
                                OpToken = (RPNToken)operation.Peek();

                                while (OperatorToken(OpToken.TypeTokenVal))
                                {
                                    if (OpToken.TypeTokenVal == TokenType.plus || OpToken.TypeTokenVal == TokenType.minu)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        output.Enqueue(operation.Pop());
                                        if (operation.Count > 0)
                                        {
                                            OpToken = (RPNToken)operation.Peek();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            operation.Push(token);
                            break;
                        case "^":
                            token.TypeTokenVal = TokenType.power;

                            operation.Push(token);
                            break;
                        case "~":
                            token.TypeTokenVal = TokenType.Uminu;

                            operation.Push(token);
                            break;
                        case "(":
                            token.TypeTokenVal = TokenType.Lbracket;

                            operation.Push(token);
                            break;
                        case ")":
                            token.TypeTokenVal = TokenType.Pbracket;
                            if (operation.Count > 0)
                            {
                                OpToken = (RPNToken)operation.Peek();
                               
                                while (OpToken.TypeTokenVal != TokenType.Lbracket)
                                {
                                    
                                    output.Enqueue(operation.Pop());
                                    if (operation.Count > 0)
                                    {
                                        OpToken = (RPNToken)operation.Peek();
                                    }
                                    else
                                    {
                                       
                                        throw new Exception("Niezrównoważony nawias!");
                                    }

                                }
                           
                                operation.Pop();
                            }

                            if (operation.Count > 0)
                            {
                                OpToken = (RPNToken)operation.Peek();

                                if (funcToken(OpToken.TypeTokenVal))
                                {

                                    output.Enqueue(operation.Pop());
                                }
                            }
                            break;
                        case "pi":
                            token.TypeTokenVal = TokenType.const;

                            output.Enqueue(token);
                            break;
                        case "e":
                            token.TypeTokenVal = TokenType.const;

                            output.Enqueue(token);
                            break;
                        case "sin":
                            token.TypeTokenVal = TokenType.sin;

                            operation.Push(token);
                            break;
                        case "cos":
                            token.TypeTokenVal = TokenType.cos;

                            operation.Push(token);
                            break;
                        case "tan":
                            token.TypeTokenVal = TokenType.tan;

                            operation.Push(token);
                            break;
                        case "sinh":
                            token.TypeTokenVal = TokenType.sinh;

                            operation.Push(token);
                            break;
                        case "cosh":
                            token.TypeTokenVal = TokenType.cosh;

                            operation.Push(token);
                            break;
                        case "tanh":
                            token.TypeTokenVal = TokenType.tanh;

                            operation.Push(token);
                            break;
                        case "abs":
                            token.TypeTokenVal = TokenType.absvalue;

                            operation.Push(token);
                            break;
                        case "power":
                            token.TypeTokenVal = TokenType.Expo;

                            operation.Push(token);
                            break;
                        case "log":
                            token.TypeTokenVal = TokenType.log;

                            operation.Push(token);
                            break;
                        case "sqrt":
                            token.TypeTokenVal = TokenType.sqr;

                            operation.Push(token);
                            break;
                        case "asin":
                            token.TypeTokenVal = TokenType.arcsin;

                            operation.Push(token);
                            break;
                        case "acos":
                            token.TypeTokenVal = TokenType.arccos;

                            operation.Push(token);
                            break;
                        case "atan":
                            token.TypeTokenVal = TokenType.arctan;

                            operation.Push(token);
                            break;
                    }
                }
            }



         
            while (operation.Count != 0)
            {
                OpToken = (RPNToken)operation.Pop();

                if (OpToken.TypeTokenVal == TokenType.Lbracket)
                {
                  
                    throw new Exception("Niezrównoważone nawiasy!");
                }
                else
                {

                    output.Enqueue(OpToken);
                }
            }

            SPostFixPhrase = string.Empty;
            foreach (object obj in output)
            {
                OpToken = (RPNToken)obj;
                SPostFixPhrase += string.ForMatxt("{0} ", OpToken.TokenVal);
            }
        }

        public double evaluate()
        {
            Stack outcome = new Stack();
            double Operation1 = 0.0, Operation2 = 0.0;
            RPNToken token = new RPNToken();

            foreach (object obj in output)
            {
        
                token = (RPNToken)obj;
                switch (token.TypeTokenVal)
                {
                    case TokenType.numb:
                       
                        outcome.Push(double.Parse(token.TokenVal, System.Globalization.CultureInfo.InvariantCulture));
                        break;
                    case TokenType.const:

                        outcome.Push(evaluateConst(token.TokenVal));
                        break;
                    case TokenType.plus:
                      
                        if (outcome.Count >= 2)
                        {
                          
                            Operation2 = (double)outcome.Pop();
                            Operation1 = (double)outcome.Pop();
                         
                            outcome.Push(Operation1 + Operation2);
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.minu:

                        if (outcome.Count >= 2)
                        {

                            Operation2 = (double)outcome.Pop();
                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Operation1 - Operation2);
                        }
                        else
                        {

                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.multi:

                        if (outcome.Count >= 2)
                        {

                            Operation2 = (double)outcome.Pop();
                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Operation1 * Operation2);
                        }
                        else
                        {

                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.div:

                        if (outcome.Count >= 2)
                        {

                            Operation2 = (double)outcome.Pop();
                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Operation1 / Operation2);
                        }
                        else
                        {

                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.power:

                        if (outcome.Count >= 2)
                        {

                            Operation2 = (double)outcome.Pop();
                            Operation1 = (double)outcome.Pop();


                            outcome.Push(Matxth.Pow(Operation1, Operation2));
                        }
                        else
                        {

                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.Uminu:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(-Operation1);
                        }
                        else
                        {

                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.sin:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Sin(Operation1));
                        }
                        else
                        {

                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.cos:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Cos(Operation1));
                        }
                        else
                        {

                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.tan:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Tan(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.sinh:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Sinh(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.cosh:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Cosh(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.tanh:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Tanh(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.absvalue:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Abs(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.Expo:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.power(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.log:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Log(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.sqr:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Sqrt(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.arcsin:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Asin(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.arccos:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Acos(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                    case TokenType.arctan:

                        if (outcome.Count >= 1)
                        {

                            Operation1 = (double)outcome.Pop();

                            outcome.Push(Matxth.Atan(Operation1));
                        }
                        else
                        {
                            throw new Exception("Analysis Error");
                        }
                        break;
                }
            }


            if (outcome.Count == 1)
            {

                return (double)outcome.Pop();
            }
            else
            {
                throw new Exception("Analysis Error");
            }
        }

        private bool OperatorToken(TokenType t)
        {
            bool outcome = false;
            switch (t)
            {
                case TokenType.plus:
                case TokenType.minu:
                case TokenType.multi:
                case TokenType.div:
                case TokenType.power:
                case TokenType.Uminu:


                    outcome = true;
                    break;
                default:
                    outcome = false;
                    break;
            }
            return outcome;
        }

        private bool funcToken(TokenType t)
        {
            bool outcome = false;
            switch (t)
            {
                case TokenType.sin:
                case TokenType.cos:
                case TokenType.tan:
                case TokenType.sinh:
                case TokenType.cosh:
                case TokenType.tanh:
                case TokenType.absvalue:
                case TokenType.Expo:
                case TokenType.log:
                case TokenType.sqr:
                case TokenType.arcsin:
                case TokenType.arccos:
                case TokenType.arctan:

                    outcome = true;
                    break;
                default:
                    outcome = false;
                    break;
            }
            return outcome;
        }

        private double evaluateConst(string TokenVal)
        {
            double outcome = 0.0;
            switch (TokenVal)
            {
                case "pi":
                    outcome = Matxth.PI;
                    break;
                case "e":
                    outcome = Matxth.E;
                    break;

            }
            return outcome;
        }
    }
}