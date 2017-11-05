using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSCode
{
    class Program
    {
        static int[,] arr;
        static Random random = new Random();

        static void Main(string[] args)
        {
            arr = ReadFromFile();

            int[][] instances = GetRandomInstances(arr.GetLength(0), 40);

            foreach (var instance in instances)
            {
                Console.WriteLine(GetSumDistance(instance));
            }

            Console.ReadKey();
        }

        private static int[,] ReadFromFile()
        {
            StreamReader reader = new StreamReader(@"C:\berlin52.txt");
            int size = Int32.Parse(reader.ReadLine());
            int[,] temp = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                string line = reader.ReadLine().Trim();
                string[] values = line.Split(' ');
                for (int j = 0; j < values.Length; j++)
                {
                    temp[i, j] = Int32.Parse(values[j]);
                }
            }
            return temp;
        }

        private static int[][] GetRandomInstances(int numOfCities, int size)
        {
            int[][] arr = new int[size][];
            for (int i = 0; i < size; i++)
            {
                arr[i] = new int[numOfCities];
                List<int> list = Enumerable.Range(0, 52).ToList();
                int temp;
                for (int j = 0; j < numOfCities; j++)
                {
                    temp = random.Next(list.Count);
                    arr[i][j] = list[temp];
                    list.RemoveAt(temp);
                }
            }
            return arr;
        }

        private static int GetSumDistance(int[] instance)
        {
            int sum = 0;
            for (int i = 1; i < instance.Length; i++)
            {
                sum += GetSingleDistance(instance[i - 1], instance[i]);
            }
            sum += GetSingleDistance(instance[0], instance[instance.Length - 1]);
            return sum;
        }

        private static int GetSingleDistance(int a, int b)
        {
            return a > b ? arr[a, b] : arr[b, a];
        }
    }
}
