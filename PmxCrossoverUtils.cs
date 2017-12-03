using System;
using WdTIGS.Models;

namespace WdTIGS {
    class PmxCrossoverUtils {
        private PmxCrossoverUtils() {
        }

        public static PmxCrossoverUtils Instance { get { return Nested.instance;  } }

        Random r = new Random();
        public static int mutationChancePercentage = 0;

        private class Nested
        {
            static Nested() { }
            internal static readonly PmxCrossoverUtils instance = new PmxCrossoverUtils();
        }

        public Instance[] CrossPopulation(Instance[] parents, float mutationChance = 0.05f) {
            mutationChancePercentage = (int)(mutationChance * 100);

            Instance[] children = new Instance[parents.Length];

            for(int i = 0; i < parents.Length; i+=2) {
                children[i] = CrossInstances(parents[i], parents[i + 1]);
                children[i+1] = CrossInstances(parents[i + 1], parents[i]);
            }

            return children;
        }

        private Instance CrossInstances(Instance parent1, Instance parent2) {
            Instance child = new Instance();
            child.Cities = new int[parent1.Cities.Length];

            for(int i = 0; i < child.Cities.Length; i++) {
                child.Cities[i] = -1;
            }

            int swatchStart, swatchEnd;
            do {
                swatchStart = r.Next(child.Cities.Length);
                swatchEnd = r.Next(swatchStart, child.Cities.Length);
            } while(swatchEnd < swatchStart || swatchEnd - swatchStart == child.Cities.Length);

            for(int i = swatchStart; i <= swatchEnd; i++) {
                child.Cities[i] = parent1.Cities[i];
            }

            for(int i = swatchStart; i <= swatchEnd; i++) {
                if(Array.IndexOf(child.Cities, parent2.Cities[i]) == -1) {
                    int currentIndex = i;
                    while(Array.IndexOf(parent1.Cities, parent2.Cities[currentIndex])  < swatchStart || Array.IndexOf(parent1.Cities, parent2.Cities[currentIndex]) > swatchEnd) {
                        currentIndex = Array.IndexOf(parent1.Cities, parent2.Cities[currentIndex]);
                    }
                    child.Cities[currentIndex] = parent2.Cities[i];
                }
            }

            for(int i = 0; i < child.Cities.Length; i++) {
                if(child.Cities[i] == -1) child.Cities[i] = parent2.Cities[i];
            }
            if(r.Next(100) < mutationChancePercentage) {
                InversionMutation(child.Cities);
            }
            
            child.Distance = PathUtils.GetSumDistance(child.Cities);
            return child;
        }

        private void InversionMutation(int[] cities) {

                int swatchStart = r.Next(cities.Length - 1);
                int length = r.Next(2, cities.Length - swatchStart);

                Array.Reverse(cities, swatchStart, length);
        }
    }
}