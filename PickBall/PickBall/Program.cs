﻿using System;
using System.Threading;

namespace PickBallGame
{
    class Program
    {
        private static void Main()
        {
            // initialize groups ball
            int[] groups = new int[] { 3, 4, 6 };
            PrintGame(groups);

            // loop main game
            while (true)
            {
                try
                {
                    HumanMove(groups);
                    if (Has0Group(groups))
                    {
                        Console.WriteLine("You lost!!");
                        break;
                    }
                    PrintGame(groups);
                    ComputerMove(groups);
                    PrintGame(groups);
                    if (Has0Group(groups))
                    {
                        Console.WriteLine("You won!!");
                        break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("An error has been occurred, please input a valid number");
                }
            }
        }

        // Print balls in all group
        static void PrintGame(int[] groups)
        {
            for (int i = 0; i < groups.Length; i++)
            {
                Console.Write("Group{0} : ", i + 1);
                for (int j = 0; j < groups[i]; j++)
                {
                    Console.Write("o ");
                }
                Console.WriteLine();
            }
        }

        // Picks ball from group
        static void PickBall(int[] groups, int group, int n)
        {
            groups[group] -= n;
        }

        // Determines 0 group remaining
        static bool Has0Group(int[] group)
        {
            return CountGroup(group) == 0;
        }

        // Determines 1 group remaining
        static bool Has1Group(int[] group)
        {
            return CountGroup(group) == 1;
        }

        // Determines 2 group remaining
        static bool Has2Group(int[] group)
        {
            return CountGroup(group) == 2;
        }

        // Determines 3 group remaining
        static bool Has3Group(int[] group)
        {
            return CountGroup(group) == 3;
        }
        
        // Counts how many groups which is remaining
        static int CountGroup(int[] group)
        {
            int count = 0;
            for (int i = 0; i < group.Length; i++)
            {
                count += group[i] != 0 ? 1 : 0;
            }
            return count;
        }

        // Gets 1 group which is remaining
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
            }
        }

        // Gets all group excepts the group has been expired
        static void Get2Group(int[] groups, out int group1, out int group2)
        {
            group1 = -1;
            group2 = -1;
            if (groups[0] == 0)
            {
                group1 = 1;
                group2 = 2;
            }
            else if (groups[1] == 0)
            {
                group1 = 0;
                group2 = 2;
            }
            else if (groups[2] == 0)
            {
                group1 = 0;
                group2 = 1;
            }
        }

        static void HumanMove(int[] groups)
        {
            Console.WriteLine("Your turn");
            Console.Write("Which group do you choose : ");
            bool isValidGroup = int.TryParse(Console.ReadLine(), out int group);
            Console.Write("How many balls do you pick : ");
            bool isValidN = int.TryParse(Console.ReadLine(), out int n);
            if (n > groups[group - 1] || n <= 0 || !isValidGroup || !isValidN)
            {
                throw new Exception();
            }
            PickBall(groups, group - 1, n);
        }

        static void ComputerMove(int[] groups)
        {
            Console.WriteLine("Computer is thinking...");
            Thread.Sleep(new Random().Next(50, 750));
            if (Has1Group(groups))
            {
                int group;
                Get1Group(groups, out group);
                if (groups[group] > 1)
                {
                    int ball = groups[group] - 1;
                    PickBall(groups, group, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, group + 1);
                }else
                {
                    PickBall(groups, group, 1);
                    Console.WriteLine("Computer has picked 1 ball from group {0}", group + 1);
                }
            }else if (Has2Group(groups))
            {
                int group1, group2;
                Get2Group(groups, out group1, out group2);
                if (groups[group1] != groups[group2])
                {
                    if (groups[group1] == 1 || groups[group2] == 1)
                    {
                        if (groups[group1] == 1)
                        {
                            int ball = groups[group2];
                            PickBall(groups, group2, ball);
                            Console.WriteLine("Computer has pciked {0} balls from group {1}", ball, group2 + 1);
                        }
                        else
                        {
                            int ball = groups[group1];
                            PickBall(groups, group1, ball);
                            Console.WriteLine("Computer has pciked {0} balls from group {1}", ball, group1 + 1);
                        }
                    }
                    else if (groups[group1] > groups[group2])
                    {
                        int ball = groups[group1] - groups[group2];
                        PickBall(groups, group1, ball);
                        Console.WriteLine("Computer has pciked {0} balls from group {1}", ball, group1 + 1);
                    }
                    else if (groups[group1] < groups[group2])
                    {
                        int ball = groups[group2] - groups[group1];
                        PickBall(groups, group2, ball);
                        Console.WriteLine("Computer has picked {0} balls from group {1}", ball, group2 + 1);
                    }
                }else
                {
                    int ball = groups[group1];
                    PickBall(groups, group1, groups[group1]);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, group1 + 1);
                }
            }else if (Has3Group(groups))
            {
                if ((groups[0] == 1 && groups[1] == 2 && groups[2] > 3) || (groups[0] == 2 && groups[1] == 1 && groups[2] > 3))
                {
                    int ball = groups[2] - 3;
                    PickBall(groups, 2, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 3);
                }
                else if ((groups[1] == 1 && groups[1] > 3 && groups[2] == 2) || (groups[0] == 2 && groups[1] > 3 && groups[2] == 1))
                {
                    int ball = groups[1] - 3;
                    PickBall(groups, 1, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 2);
                }
                else if (groups[0] == 3 && groups[1] > 2 && groups[2] == 1)
                {
                    int ball = groups[1] - 2;
                    PickBall(groups, 1, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 2);
                }
                else if ((groups[0] == 3 && groups[1] == 2 && groups[2] > 1) || (groups[0] == 2 && groups[1] == 3 && groups[2] > 1))
                {
                    int ball = groups[2] - 1;
                    PickBall(groups, 1, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 3);
                }
                else if (groups[0] == 1 && groups[1] == 1 && groups[2] > 1)
                {
                    int ball = groups[2] - 1;
                    PickBall(groups, 2, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 3);
                }
                else if (groups[0] == 1 && groups[1] > 1 && groups[2] == 1)
                {
                    int ball = groups[1] - 1;
                    PickBall(groups, 1, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 2);
                }
                else if (groups[0] > 1 && groups[1] == 1 && groups[2] == 1)
                {
                    int ball = groups[0] - 1;
                    PickBall(groups, 0, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 1);
                }
                else if (groups[0] == 3 && groups[1] == 1 && groups[2] > 2)
                {
                    int ball = groups[2] - 2;
                    PickBall(groups, 2, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, 3);
                }
                else
                {
                    Random rd = new Random();
                    int group = rd.Next(0, 3);
                    int ball = rd.Next(1, groups[group] + 1);
                    PickBall(groups, group, ball);
                    Console.WriteLine("Computer has picked {0} balls from group {1}", ball, group + 1);
                }
            }
        }
    }
}
