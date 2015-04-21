using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postfix_to_e_nfa
{
    class Program
    {

        static List<List<Tuple<char, int>>> graph = new List<List<Tuple<char, int>>>();


        static void Main(string[] args)
        {
            
            Console.Out.Write("Enter Your Postfix string :\n");
            String expression;
            expression = Console.In.ReadLine();

            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '|') // +
                {
                    graph.Add(new List<Tuple<char, int>>());
                    graph.Add(new List<Tuple<char, int>>());
                    int SecondNodeStart = stack.Peek().Item1;
                    int SecondNodeEnd = stack.Peek().Item2;
                    stack.Pop();
                    int FirstNodeStart = stack.Peek().Item1;
                    int FirstNodeEnd = stack.Peek().Item2;
                    stack.Pop();
                    graph[graph.Count - 2].Add(new Tuple<char, int>('u', FirstNodeStart));
                    graph[graph.Count - 2].Add(new Tuple<char, int>('u', SecondNodeStart));
                    graph[FirstNodeEnd].Add(new Tuple<char, int>('u', graph.Count - 1));
                    graph[SecondNodeEnd].Add(new Tuple<char, int>('u', graph.Count - 1));
                    stack.Push(new Tuple<int, int>(graph.Count - 2, graph.Count - 1));
                }
                else
                {
                    if (expression[i] == '&') // .
                    {
                        int SecondNodeStart = stack.Peek().Item1;
                        int SecondNodeEnd = stack.Peek().Item2;
                        stack.Pop();
                        int FirstNodeStart = stack.Peek().Item1;
                        int FirstNodeEnd = stack.Peek().Item2;
                        stack.Pop();
                        graph[FirstNodeEnd].Add(new Tuple<char, int>('u', SecondNodeStart));
                        stack.Push(new Tuple<int, int>(FirstNodeStart, SecondNodeEnd));
                    }
                    else
                    {
                        if (expression[i] == '*') // *
                        {
                            graph.Add(new List<Tuple<char, int>>());
                            graph.Add(new List<Tuple<char, int>>());
                            int NodeStart = stack.Peek().Item1;
                            int NodeEnd = stack.Peek().Item2;
                            stack.Pop();
                            graph[graph.Count - 2].Add(new Tuple<char, int>('u', NodeStart));
                            graph[graph.Count - 2].Add(new Tuple<char, int>('u', graph.Count -1));
                            graph[NodeEnd].Add(new Tuple<char, int>('u', graph.Count - 1));
                            graph[graph.Count - 1].Add(new Tuple<char, int>('u', graph.Count - 2));
                            stack.Push(new Tuple<int, int>(graph.Count - 2, graph.Count - 1));
                        }
                        else
                        {
                            graph.Add(new List<Tuple<char, int>>());
                            graph.Add(new List<Tuple<char, int>>());
                            graph[graph.Count - 2].Add(new Tuple<char, int>(expression[i], graph.Count - 1));
                            stack.Push(new Tuple<int, int>(graph.Count - 2, graph.Count - 1));
                        }
                    }
                }
            }
            for (int i = 0; i < graph.Count; i++)
            {
                Console.Out.Write("Node ");
                Console.Out.Write(i);
                Console.Out.Write(" Connections :\n");
                for (int j = 0; j < graph[i].Count; j++)
                {
                    Console.Out.Write("\tAlphabet : ");
                    Console.Out.Write(graph[i][j].Item1);
                    Console.Out.Write(" Node : ");
                    Console.Out.Write(graph[i][j].Item2);
                    Console.Out.Write("\n");
                }
                Console.Out.Write("\n");
            }
            int start = stack.Peek().Item1;
            int end = stack.Peek().Item2;
            Console.Out.Write("Start Node  : ");
            Console.Out.Write(start);
            Console.Out.Write("\nEnd Node : ");
            Console.Out.Write(end);
            Console.Out.Write("\n");
            expression = Console.In.ReadLine();
        }
    }
}
