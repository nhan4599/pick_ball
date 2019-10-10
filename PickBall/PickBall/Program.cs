using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickBall
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] group = new int[] { 3, 4, 6 };
            while (true)
            {
                HumanMove(group);
                if (Has0Group(group))
                {
                    Console.WriteLine("You lose!!");
                    break;
                }
                PrintGame(group);
            }
        }

        // Print ball in all group
        static void PrintGame(int[] group)
        {
            for (int i = 0; i < group.Length; i++)
            {
                Console.Write("Group{0} : ", i + 1);
                for (int j = 0; j < group[i]; j++)
                {
                    Console.Write("o ");
                }
                Console.WriteLine();
            }
        }

        // Pick ball from group
        static void PickBall(int[] group, int g, int n)
        {
            group[g] -= n;
        }

        static bool Has0Group(int[] group)
        {
            return CountGroup(group) == 0;
        }

        static bool Has1Group(int[] group)
        {
            return CountGroup(group) == 1;
        }

        static bool Has2Group(int[] group)
        {
            return CountGroup(group) == 2;
        }

        static bool Has3Group(int[] group)
        {
            return CountGroup(group) == 3;
        }
        
        static int CountGroup(int[] group)
        {
            int count = 0;
            for (int i = 0; i < group.Length; i++)
            {
                count += group[i] != 0 ? 1 : 0;
            }
            return count;
        }

        static void Get1Group(int[] group, out int i)
        {
            i = -1;
            if (group[0] == 0 && group[1] == 0)
            {
                i = 2;
            }
            else if (group[0] == 0 && group[2] == 0)
            {
                i = 1;
            }
            else if (group[1] == 0 && group[2] == 0)
            {
                i = 0;
                return;
            }
        }

        static void Get2Group(int[] group, out int a, int b)
        {
            a = -1;
            b = -1;
            if (group[0] == 0)
            {
                a = 1;
                b = 2;
            }
            else if (group[1] == 0)
            {
                a = 0;
                b = 2;
            }
            else if (group[2] == 0)
            {
                a = 0;
                b = 1;
            }
        }

        static void HumanMove(int[] group)
        {
            Console.Write("Which group do you choose : ");
            int g = int.Parse(Console.ReadLine());
            Console.Write("How many balls do you pick : ");
            int n = int.Parse(Console.ReadLine());
            PickBall(group, g, n);
        }

        static void ComputerMove(int[] group)
        {
            if (Has1Group(group))
            {
                int g;
                Get1Group(group, out g);
            }
        }
    }
}
