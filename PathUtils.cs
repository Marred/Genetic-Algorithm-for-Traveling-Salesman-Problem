﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WdTIGS.Models;

namespace WdTIGS
{
    class PathUtils
    {
        private PathUtils() {
            ReadDistances();
        }

        public static PathUtils Instance { get { return Nested.instance;  } }

        private class Nested
        {
            static Nested() { }
            internal static readonly PathUtils instance = new PathUtils();
        }

        static int numOfCities;
        static int[,] distances;

        private static void ReadDistances()
        {
            StreamReader reader = new StreamReader(@"berlin52.txt");
            numOfCities = Int32.Parse(reader.ReadLine());
            distances = new int[numOfCities, numOfCities];

            for (int i = 0; i < numOfCities; i++)
            {
                string line = reader.ReadLine().Trim();
                string[] values = line.Split(' ');
                for (int j = 0; j < values.Length; j++)
                {
                    distances[i, j] = Int32.Parse(values[j]);
                }
            }
        }

        public Instance[] RandomizeInstances(int numberOfInstances)
        {
            var instances = new Instance[numberOfInstances];
            Random random = new Random();
            for (int i = 0; i < numberOfInstances; i++)
            {
                instances[i].Cities = new int[numOfCities];
                List<int> list = Enumerable.Range(0, numOfCities).ToList();
                int temp;
                for (int j = 0; j < numOfCities; j++)
                {
                    temp = random.Next(list.Count);
                    instances[i].Cities[j] = list[temp];
                    list.RemoveAt(temp);
                }
                instances[i].Distance = GetSumDistance(instances[i].Cities);
            }
            return instances;
        }

        public void SaveInstance(Instance instance)
        {
            string text = string.Empty;
            foreach(var city in instance.Cities)
            {
                text += city + "-";
            }
            text = text.Remove(text.Length - 1, 1);
            text += " " + instance.Distance;
            System.IO.File.WriteAllText(@"odleglosci/result.txt", text);
        }

        public static int GetSumDistance(int[] instance)
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
            return a > b ? distances[a, b] : distances[b, a];
        }
    }
}
