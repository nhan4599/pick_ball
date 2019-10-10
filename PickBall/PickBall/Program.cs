using System;

namespace PickBall
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] groups = new int[] { 3, 4, 6 };
            while (true)
            {
                HumanMove(groups);
                if (Has0Group(groups))
                {
                    Console.WriteLine("You lose!!");
                    break;
                }
                PrintGame(groups);
                ComputerMove(groups);
                if (Has0Group(groups))
                {
                    Console.WriteLine("You won!!");
                    break;
                }
                PrintGame(groups);
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

        // Define 0 group remaining
        static bool Has0Group(int[] group)
        {
            return CountGroup(group) == 0;
        }

        // Define 1 group remaining
        static bool Has1Group(int[] group)
        {
            return CountGroup(group) == 1;
        }

        // Define 2 group remaining
        static bool Has2Group(int[] group)
        {
            return CountGroup(group) == 2;
        }

        // Define 3 group remaining
        static bool Has3Group(int[] group)
        {
            return CountGroup(group) == 3;
        }
        
        // Count how many group which is remaining
        static int CountGroup(int[] group)
        {
            int count = 0;
            for (int i = 0; i < group.Length; i++)
            {
                count += group[i] != 0 ? 1 : 0;
            }
            return count;
        }

        // Get group which is remaining
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

        // Get all group excepts the group has been expired
        static void Get2Group(int[] group, out int a, out int b)
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

        static void HumanMove(int[] groups)
        {
            Console.Write("Which group do you choose : ");
            int group = int.Parse(Console.ReadLine());
            Console.Write("How many balls do you pick : ");
            int n = int.Parse(Console.ReadLine());
            PickBall(groups, group, n);
        }

        static void ComputerMove(int[] groups)
        {
            if (Has1Group(groups))
            {
                int group;
                Get1Group(groups, out group);
                if (groups[group] > 1)
                {
                    int n = groups[group] - 1;
                    PickBall(groups, group, n);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", n, group);
                }else
                {
                    PickBall(groups, group, 1);
                    Console.WriteLine("Computer has picked 1 balls from group {0}", group);
                }
            }else if (Has2Group(groups))
            {

            }
        }
    }
}
