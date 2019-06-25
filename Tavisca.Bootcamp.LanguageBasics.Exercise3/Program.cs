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

         public static int[] find_max(int[] index_array, int[] diet)
        {
            List<int> new_index_array = new List<int>();// To maintain list of new diet plan
            int temp = diet[index_array[0]];
           new_index_array.Add(index_array[0]);
            //find list of high diets
            for(int i=1; i<index_array.Length;i++)
            {
                if(diet[index_array[i]]>temp)
                {
                    temp=diet[index_array[i]];
                    //Array.Clear(new_index_array);
                    new_index_array.Clear();
                    new_index_array.Add(index_array[i]);
                    
                }
                else if(diet[index_array[i]]==temp)
                {
                   new_index_array.Add(index_array[i]);
                }
            }
            new_index_array.Sort();// sort diet plan list according to index
            return new_index_array.ToArray();
        }
        public static int[] find_min(int[] index_array, int[] diet)
        {
            List<int> new_index_array = new List<int>();// To maintain list of new diet plan
            int temp = diet[index_array[0]];
           new_index_array.Add(index_array[0]);
            //find list of low diets
            for(int i=1; i<index_array.Length;i++)
            {
                if(diet[index_array[i]]<temp)
                {
                    temp=diet[index_array[i]];
                    new_index_array.Clear();
                    new_index_array.Add(index_array[i]);
                    
                }
                else if(diet[index_array[i]]==temp)
                {
                   new_index_array.Add(index_array[i]);
                }
            }
            new_index_array.Sort();// sort diet plan list according to index
            return new_index_array.ToArray();
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
                int[] index_array =new int[protein.Length];// To store all possible diet plans index
                //choose all diet plans initially
                for(int j=0;j<protein.Length;j++)
                    {
                      index_array[j]= j;
                    }
                //Choose best diet plans according to diet code  one by one
                //Possibly Reducing index_array size after each loop
                for(int j=0;j<dietPlans[i].Length;j++)
                {
                    char diet_code= dietPlans[i][j];
                    
                    if(diet_code== 'P')
                    {
                        index_array = find_max(index_array, protein);
                    }
                    else if(diet_code== 'p')
                    {
                        index_array = find_min(index_array, protein);
                    }
                    else if(diet_code== 'C')
                    {
                        index_array = find_max(index_array, carbs);
                    }
                    else if(diet_code== 'c')
                    {
                        index_array = find_min(index_array, carbs);
                    }
                    else if(diet_code== 'F')
                    {
                        index_array = find_max(index_array, fat);
                    }
                    else if(diet_code== 'f')
                    {
                        index_array = find_min(index_array, fat);
                    }
                    else if(diet_code== 'T')
                    {
                        index_array = find_max(index_array, calorie);
                    }
                    else if(diet_code== 't')
                    {
                        index_array = find_min(index_array, calorie);
                    }
                    //break loop if no further diet code check required
                    if(index_array.Length==1)
                        break;
                }
                //store best diet plan of each member in result array
                result[r++]=index_array[0];
            }
            return result;
        }
    }
}
