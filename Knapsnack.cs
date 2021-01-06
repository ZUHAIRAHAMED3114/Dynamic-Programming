using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicPrograming
{
    public class Knapsack01 {

        /* 
           Given items of certain weights/values and maximum allowed weight
           pick items from this set to maximizw sum of values of itesm such i.e sum of weights is
           less than or equal to maximum allowed weight

        */
       
        
        // time Complexit using permutaion and combination is O(N * N!) because there are (N*N!) sets are 
        // there.. in those set we have to find those set which give optimal cost
        // this method is not optimal because same set of items is used  number of times with 
        // only order of sets is changed .. 
        public static int usingPermutation(int[] costs, int[] weights, int TargetWeight)
        {
            int maximumprofixt = -1;
            int numberofitems = weights.Length;
            bool[] visisted = new bool[numberofitems];
            for (int index = 0; index < numberofitems; index++)
            {
                if (!visisted[index] && weights[index] < TargetWeight)
                {
                    visisted[index] = true;
                    var remainingweight = TargetWeight - weights[index];
                    maximumprofixt = Math.Max(maximumprofixt, costs[index] + dfs(costs, weights, remainingweight, visisted));
                    visisted[index] = false;

                }
            }


            return maximumprofixt;
        }

        private static int dfs(int[] costs, int[] weights, int remainingweight, bool[] visisted)
        {
            int profit = 0;

            if (remainingweight == 0)
            {
                return profit;
            }
            else
            {
                for (int index = 0; index < weights.Length; index++)
                {

                    if (!visisted[index] && weights[index] <=remainingweight)
                    {
                        visisted[index] = true;
                        var newWeight = remainingweight - weights[index];
                        var newprofit = costs[index] + dfs(costs, weights, newWeight, visisted);
                        visisted[index] = false;
                        profit = Math.Max(newprofit, profit);
                    }
                }
                return profit;
            }

        }


        // we can reduce the number of sets using  choosing approach
        // i.e is either selecting  the partiuclar item or not  based on this approach
        // we get 2^n sets      --> --> --> --> --> // --> --> // --> --> --> 
        // so time complexity for finding optimal set is O(2^n)


        public static int OptimalRecursion(int[] costOfItems, int[] weightOfItmes, int TargetWeight)
        {
            return findMaxProfit(costOfItems.Length - 1, costOfItems, weightOfItmes, TargetWeight);
        }

        private static int findMaxProfit(int currentItem, int[] costOfItems, int[] weightOfItems,
            int TargetWeight)
        {

            if (costOfItems.Length == 0 || weightOfItems.Length == 0 || currentItem < 0)
            {
                return 0;
            }

            int currentItemWeight = weightOfItems[currentItem];
            if (currentItemWeight <= TargetWeight)
            {
                // maximum profit if we considered the current items

                var includeCurretnItemWeightProfit = costOfItems[currentItem] + findMaxProfit(
                    currentItem - 1, costOfItems, weightOfItems, TargetWeight - currentItemWeight
                    );
                var withoutIncludingCurrentItemWeightProfit = findMaxProfit(
                    currentItem - 1, costOfItems, weightOfItems, TargetWeight);

                return Math.Max(includeCurretnItemWeightProfit, withoutIncludingCurrentItemWeightProfit);
            }
            else
            {
                return findMaxProfit(
                     currentItem - 1, costOfItems, weightOfItems, TargetWeight
                    );
            }


        }


        // To the above optimal recursion problem we can improve the performance
        // by memorize the recursion call
      
        public static int RecursionWithMemoization(int[] costs,int[] weights,int targetWeight) { 
            
           int  x_axis=targetWeight+1;
           int  y_axix=weights.Length+1;

            int[,] memoization = new int[x_axis, y_axix];
            for (int i = 0; i < x_axis; i++) {
                for (int j=0;j<y_axix;j++) {

                    memoization[i, j] = -1;
                }
                
            }
          return  recursion_with_memoization(costs, weights, targetWeight, weights.Length,memoization);


          


        }
        private static int recursion_with_memoization(int[] costs, int[] weights, int targetWeight, int length, int[,] memoization)
        {
            if (targetWeight == 0 || length == 0)
            {
                return 0;
            }

            else {


                if (memoization[targetWeight, length] != -1)
                {
                    return memoization[targetWeight, length];
                }

                if (weights[length - 1] <= targetWeight)
                {

                    var includedprofit = costs[length - 1] + recursion_with_memoization(costs, weights, targetWeight - weights[length - 1], length - 1, memoization);
                    var notincludedprofit = recursion_with_memoization(costs, weights, targetWeight, length - 1, memoization);

                    memoization[targetWeight, length] = Math.Max(includedprofit, notincludedprofit);
                   
                }
                else {

                    memoization[targetWeight, length] = recursion_with_memoization(costs, weights, targetWeight, length - 1, memoization);
                }

                return memoization[targetWeight, length];


            }


        }


        // To the above Recursion problem with memoization we can some what improve the 
        // performance by avoiding the recursion call ... b/z it will make stackOverFlowExcpetion
        // so this problem is to be solved through Iteration approach i.e is top down approach


        public static int TopDown(int[] costs,int[] weights,int targetWeight) {

            int x_axis = targetWeight + 1;
            int y_axis = weights.Length +1;

            int[,] table = new int[x_axis, y_axis];

            // y-> indiicat the number of items are there
            // x-> indicate the availble weight
            for (int y=0;y<y_axis;y++) {
                for (int x=0;x<x_axis;x++) {
                    // intialization of the table
                    if (x == 0 || y == 0)
                    {
                        table[x, y] = 0;
                    }
                    else {
                        /// this is heart of the TopDown Approac

                        if (weights[y - 1] <= x)
                        {
                            // selecting this y-> items
                            var includedValue = costs[y - 1] + table[x - weights[y - 1], y - 1];
                            var notIncludedValue = table[x, y - 1];

                            table[x, y] = Math.Max(includedValue, notIncludedValue);

                        }
                        else {

                            table[x, y] = table[x, y - 1];
                        }
                    
                         
                    
                    
                    }
                }
            }


            return table[targetWeight, weights.Length];

        }
    
    }
}
