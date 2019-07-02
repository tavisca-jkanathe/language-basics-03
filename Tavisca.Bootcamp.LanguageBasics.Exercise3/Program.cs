using System;
using System.Linq;
using System.Collections.Generic;//added to use List

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

         public static int[] FindMax(int[] indexArray, int[] diet)
        {
            List<int> newIndexArray = new List<int>();// To maintain list of new diet plan
            int temp = diet[indexArray[0]];
           newIndexArray.Add(indexArray[0]);
            //find list of high diets
            for(int i=1; i<indexArray.Length;i++)
            {
                if(diet[indexArray[i]]>temp)
                {
                    temp=diet[indexArray[i]];
                    //Array.Clear(newIndexArray);
                    newIndexArray.Clear();
                    newIndexArray.Add(indexArray[i]);
                    
                }
                else if(diet[indexArray[i]]==temp)
                {
                   newIndexArray.Add(indexArray[i]);
                }
            }
            newIndexArray.Sort();// sort diet plan list according to index
            return newIndexArray.ToArray();
        }
        public static int[] FindMin(int[] indexArray, int[] diet)
        {
            List<int> newIndexArray = new List<int>();// To maintain list of new diet plan
            int temp = diet[indexArray[0]];
           newIndexArray.Add(indexArray[0]);
            //find list of low diets
            for(int i=1; i<indexArray.Length;i++)
            {
                if(diet[indexArray[i]]<temp)
                {
                    temp=diet[indexArray[i]];
                    newIndexArray.Clear();
                    newIndexArray.Add(indexArray[i]);
                    
                }
                else if(diet[indexArray[i]]==temp)
                {
                   newIndexArray.Add(indexArray[i]);
                }
            }
            newIndexArray.Sort();// sort diet plan list according to index
            return newIndexArray.ToArray();
        }
        
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            int[] calorie =new int[protein.Length];//to store calorie count
            int[] result =new int[dietPlans.Length];// to store final output
            int r=0;
            //To calculate calorie count
            for(int i=0;i<protein.Length;i++)
            {
                calorie[i]=5*(protein[i]+ carbs[i])+(9* fat[i]);
            }
            //diet plan of each member
            for(int i=0;i<dietPlans.Length;i++)
            {
                int[] indexArray =new int[protein.Length];// To store all possible diet plans index
                //choose all diet plans initially
                for(int j=0;j<protein.Length;j++)
                    {
                      indexArray[j]= j;
                    }
                //Choose best diet plans according to diet code  one by one
                //Possibly Reducing indexArray size after each loop
                for(int j=0;j<dietPlans[i].Length;j++)
                {
                    char dietCode= dietPlans[i][j];
                    
                    if(dietCode== 'P')
                    {
                        indexArray = FindMax(indexArray, protein);
                    }
                    else if(dietCode== 'p')
                    {
                        indexArray = FindMin(indexArray, protein);
                    }
                    else if(dietCode== 'C')
                    {
                        indexArray = FindMax(indexArray, carbs);
                    }
                    else if(dietCode== 'c')
                    {
                        indexArray = FindMin(indexArray, carbs);
                    }
                    else if(dietCode== 'F')
                    {
                        indexArray = FindMax(indexArray, fat);
                    }
                    else if(dietCode== 'f')
                    {
                        indexArray = FindMin(indexArray, fat);
                    }
                    else if(dietCode== 'T')
                    {
                        indexArray = FindMax(indexArray, calorie);
                    }
                    else if(dietCode== 't')
                    {
                        indexArray = FindMin(indexArray, calorie);
                    }
                    //break loop if no further diet code check required
                    if(indexArray.Length==1)
                        break;
                }
                //store best diet plan of each member in result array
                result[r++]=indexArray[0];
            }
            return result;
        }
    }
}
