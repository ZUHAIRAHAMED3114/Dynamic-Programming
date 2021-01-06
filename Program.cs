using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicPrograming
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] costs = { 22, 20, 15, 30, 24, 54, 21, 32, 18, 25 };
            int[] weight = { 4, 2, 3, 5, 5, 6, 9, 7, 8, 10 };
            int targetweight= 40;
            var probablilityValue= Knapsack01.usingPermutation(costs, weight, targetweight);
            Console.WriteLine(probablilityValue);
            Console.ReadLine();

            var optimalRecursion = Knapsack01.OptimalRecursion(costs, weight, targetweight);
            Console.WriteLine(optimalRecursion);
            Console.ReadLine();

            var recursionWithMemoization = Knapsack01.RecursionWithMemoization(costs, weight, targetweight);
            Console.WriteLine(recursionWithMemoization);
            Console.ReadLine();

            var Topdowm = Knapsack01.TopDown(costs, weight, targetweight);
            Console.WriteLine(Topdowm);
            Console.ReadLine();

        }
    }
}
