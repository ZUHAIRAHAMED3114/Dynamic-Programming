using System;
namespace DynamicProgrammingPratice
{
   public class DynamicProgramming
    {

        public static int Knapsncak1(int[] costs,int[] weights,int availableWeightSize) {

            int maxCost = int.MinValue;
            int currentCost = 0;
            bool[] selected = new bool[costs.Length];
          
            for (int currentPostion=0;currentPostion<weights.Length;currentPostion++) {
                
                if (weights[currentPostion]<=availableWeightSize) {
                    selected[currentPostion]= true;
                    currentCost = method1(costs, weights, currentPostion, availableWeightSize - weights[currentPostion], selected);
                     maxCost = Math.Max(currentCost, maxCost);
                    selected[currentPostion] = false;

                }

            }

            return maxCost;
        }
        // this problem time complexity is to 0(n!)
        private static int method1(int[] costs, int[] weights,int currentPostion,int targetWeight,bool[] selected) {
            if (currentPostion == costs.Length || targetWeight == 0) {
                var sum = 0;
                for (int i=0;i<selected.Length;i++) {
                    if (selected[i]) 
                        sum += costs[i];
                }
                return sum;
             }
            

            int maxCost = int.MinValue;
            int currentCost = 0;


            for (int i=0;i<costs.Length;i++) {
                if (weights[currentPostion] <= targetWeight && !selected[i]) {
                        selected[i] = true;
                    currentCost = method1(costs, weights, currentPostion + 1, targetWeight - weights[i], selected);
                    maxCost = Math.Max(currentCost, maxCost);
                        selected[i] = false;
                    
                }
            }

            return maxCost;
        
        }

        // by using the another approach .....

        public static int Knapsnack2(int[] costs,int[] weights,int availableWeightsize,int count) {
            if (count==0 || availableWeightsize==0) { return 0; }

            if (weights[count-1]<=availableWeightsize) {
                return Math.Max(Knapsnack2(costs,weights,availableWeightsize,count-1), 
                    costs[count-1]+ Knapsnack2(costs,weights,availableWeightsize-weights[count-1],count-1));
            }

            return Knapsnack2(costs, weights, availableWeightsize, count - 1);
        }

        public static bool subsetSumUsingBacktracking(int[] items,int sum) {
            var selected = new bool[items.Length];
            for (int i=0;i<items.Length;i++) {
                selected[i] = true;
                if (subsetSum(items,sum-items[i],selected,1)) {
                    return true;
                }
                selected[i] = false;
            }

            return false;
        }

        private static bool subsetSum(int[] items,int sum,bool[] selected,int currentPosition) {
            if (sum == 0) return true;
            if (sum < 0) return false;
            if (currentPosition > items.Length) return false;

            for (int i=0;i<items.Length;i++){
                if (!selected[i]) {
                    selected[i] = true;
                    if (subsetSum(items,sum-items[i],selected,currentPosition+1)) {
                        return true;
                    }
                    selected[i] = false;
                    
                }
            }
            return false;
        }

        public static bool partitionEqulsubsetSum(int[] items) {
            if (items.Length % 2 != 0) return false;
            var listofitems = items.ToList();
            var sum = listofitems.Sum();
            if (subsetSumUsingBacktracking(items, sum / 2)) { return true; }
            else
                return false;
        }

        public static int rodCuttion(int[] costs,int avalilabeSize){

            int currentCost = int.MinValue;
            int maxCost = int.MinValue;
            for (int i = 0; i < costs.Length; i++)
            {
                currentCost = findCostofRod(costs, avalilabeSize, 0);
                maxCost = Math.Max(currentCost, maxCost);


            }
            return maxCost;
        }
        
        private static int findCostofRod(int[] costs,int availableSize,int depth) {
            if (availableSize == 0) return 0;
            if (depth == costs.Length) return 0;

            int currentCost = int.MinValue;
            int maxCost = int.MinValue;
            for (int i = 0; i < costs.Length; i++) {
                int length = i + 1;
                if (length <= availableSize) {
                    currentCost = costs[i] + findCostofRod(costs,availableSize-length,depth+1);
                    maxCost = Math.Max(currentCost, maxCost);
                }
                
            }
            return maxCost;         
        }

        // to the rodCutting problem make a maximum output recursion solutuion...
        

        // minimum number of coins selection....
        private static int CoinChangeProblem(int[] coins,int totalcost,int depth) {
            if (totalcost == 0) return 0;
            if (depth == coins.Length) return 0;

            int numberofCoins= 0;
            int minimumCoins = int.MaxValue;
            for (int i = 0; i < coins.Length; i++) {
                if (coins[i] <= totalcost) {
                    numberofCoins = 1 + CoinChangeProblem(coins, totalcost - coins[i], depth + 1);
                    minimumCoins = Math.Min(numberofCoins,minimumCoins);
                }
            }

            return minimumCoins;
        }

        public static int CoinChangeproblem_minimum_numberofCoins(int[] coins,int totalcost) {
            int numberofCoins = 0;
            int minimumCoins = int.MaxValue;
            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i] <= totalcost)
                {
                    numberofCoins = 1 + CoinChangeProblem(coins,totalcost-coins[i],0);
                    minimumCoins = Math.Min(numberofCoins, minimumCoins);
                }
            }

            return minimumCoins;
        }

        // to the above problem make the optimal solution 

        public static int longestCommonSubSequence(string word1,string word2) {

            var listOfsubSequence_for_word1 = new List<string>();
            var listOfsubSequence_for_word2 = new List<string>();
            subSequence(word1, word1.Length, "", listOfsubSequence_for_word1);
            subSequence(word2, word2.Length, "", listOfsubSequence_for_word2);

            HashSet<string> set = new HashSet<string>();
            listOfsubSequence_for_word1.ForEach(word =>set.Add(word));
            listOfsubSequence_for_word2.ForEach(word => set.Add(word));
            Console.ReadKey();
              var listofdata = set.ToList();
            listofdata.ForEach(x => { Console.WriteLine(x); });
            Console.WriteLine("====================");
            listofdata.Sort(new stringCOmparer());
            listofdata.ForEach(x => { Console.WriteLine(x); });
          return  listofdata.First().Length;
        }
        private class stringCOmparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return x.Length >= y.Length ? 1: 0;
            }
        }
        private static void subSequence(string word,int currentIndex,String HelperWOrd,List<string> dictionary) {
            if (currentIndex == 0) {
                dictionary.Add(HelperWOrd);
                return;
            }

            string currentWord = word[currentIndex-1].ToString();
            currentIndex--;
            // either selecting the currentWord or
            // not selecting the currentWord
            // selecting the current word...
            subSequence(word, currentIndex, currentWord + HelperWOrd, dictionary);

            // not selecting the curretn word...
            subSequence(word, currentIndex, HelperWOrd, dictionary);

        }

        //original longest common subsequence problem
        public static int LongestCommonSUbSequence(string word1,string word2) {
            if (word1==null || word2==null) { return 0; }
            //--here write the solution...
            return lognestcomsubsequence(word1, word2, word1.Length, word2.Length);

        }

        private static int lognestcomsubsequence(string word1,string word2,int n1,int n2) {
            if (n1==0 || n2==0) {
                return 0;
            }
            if (word1[n1-1] == word2[n2-1])
            {
                return 1+lognestcomsubsequence(word1, word2, n1 - 1, n2 - 1);
            }
            else {
                return Math.Max(
                       lognestcomsubsequence(word1, word2, n1, n2 - 1),
                       lognestcomsubsequence(word1, word2, n1 - 1, n2)
                    );
            }
        }


        public static int matrixChainMultiplication(int[] items)
        {
            return matiricChainmultioplicaiton(items, 1, items.Length - 1);
        }

        private static int matiricChainmultioplicaiton(int[] items, int start, int end)
        {
            if (start >= end) { return 0; }
            // if-> start==end that means array contain only one index so it will not became the matirx 
            int maximumMultiplication = int.MaxValue;
            for (int k = start; k < end; k++)
            {
                var leftMatrixResult = matiricChainmultioplicaiton(items, start, k);
                var rightMatrixResult = matiricChainmultioplicaiton(items, k+1, end);
                var currentResult = items[start-1] * items[k] * items[end] + leftMatrixResult + rightMatrixResult;
                maximumMultiplication = Math.Min(currentResult, maximumMultiplication);

            }
            return maximumMultiplication;
        }
        /*
        public static int matrixChainMultiplication(int[] items) {
            return matiricChainmultioplicaiton(items, 0, items.Length-1);
        }

        private static int matiricChainmultioplicaiton(int[] items,int start,int end) {
            if (start>=end ) { return 0; }
            // if-> start==end that means array contain only one index so it will not became the matirx 
            int maximumMultiplication = int.MaxValue;
            for (int k = start +1; k < end; k++) {
                var leftMatrixResult = matiricChainmultioplicaiton(items, start, k);
                var rightMatrixResult = matiricChainmultioplicaiton(items,k,end);
                var currentResult = items[start] * items[k] * items[end] + leftMatrixResult + rightMatrixResult;
                maximumMultiplication = Math.Min(currentResult,maximumMultiplication);
                
            }
            return maximumMultiplication;
        }




*/

        // finding for the palindrom

        private static bool isPalindrome(string word) {
            int start = 0;
            int end = word.Length - 1;

            while (word[start]==word[end]) {
                    start++;
                    end--;
                
                if (end < start) 
                    return true;
            }

            

            return false;
        }


        public static int minimumPalindromePartition(string word) {

            return palindromPartition(word, 0, word.Length - 1);
        }
    
        private static int palindromPartition(string word,int start,int end) {
            if (end == start) return 0;
            if (isPalindrome(word.Substring(start, (end - start) + 1))) {
                return 0;
            }
            int minpartiion = int.MaxValue;

            for (int k=start;k<end;k++) {
                int leftpartiton = palindromPartition(word, start, k);
                int rightPartiton = palindromPartition(word, k + 1, end);
                minpartiion = Math.Min(minpartiion, 1 + leftpartiton + rightPartiton);
            }

            return minpartiion;
        }
    
    
    
    
    
    
    }
}