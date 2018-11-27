using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class CheckBrackets : Processor
    {
        public CheckBrackets(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string str)
        {
            Stack<Bracket> stackOfBrackets = new Stack<Bracket>();
            char[] arrayOfBrackects = str.ToCharArray();
            for (int i = 0; i < arrayOfBrackects.Length; i++)
            {
                if(arrayOfBrackects[i] == '(' || arrayOfBrackects[i] == '[' || arrayOfBrackects[i] == '{' )
                {
                    stackOfBrackets.Push(new Bracket(arrayOfBrackects[i] , i));
                }

                else if(
                    ( stackOfBrackets.Count != 0 && arrayOfBrackects[i] == ')' && stackOfBrackets.Peek().Type == '(' ) ||
                    ( stackOfBrackets.Count != 0 && arrayOfBrackects[i] == ']' && stackOfBrackets.Peek().Type == '[' ) ||
                    ( stackOfBrackets.Count != 0 && arrayOfBrackects[i] == '}' && stackOfBrackets.Peek().Type == '{' ) 
                    )
                    {
                        stackOfBrackets.Pop();
                    }
                    else if(arrayOfBrackects[i] == ')' || arrayOfBrackects[i] == ']' || arrayOfBrackects[i] == '}')
                    {
                        return i + 1;
                    }
            }
            if(stackOfBrackets.Count != 0)
            {
                return stackOfBrackets.Peek().Index + 1;
            }
            return -1;
        }
        class Bracket
        {
            public Bracket(char type , long index)
            {
                this.Type = type;
                this.Index = index;
            }
            public char Type { set; get; }
            public long Index { set; get; }
        }
    }
}
