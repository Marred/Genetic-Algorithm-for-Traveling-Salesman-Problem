using System;
using WdTIGS.Models;

namespace WdTIGS.Services
 {
    class PmxCrossoverService {
        private PmxCrossoverService() {
        }

        public static PmxCrossoverService Instance { get { return Nested.instance;  } }

        Random r = new Random();
        private int mutationChance;

        private class Nested
        {
            static Nested() { }
            internal static readonly PmxCrossoverService instance = new PmxCrossoverService();
        }

        public Subject[] CrossPopulation(Subject[] parents, int crossoverChance = 85, int mutationChance = 5) {
            this.mutationChance = mutationChance;

            Subject[] children = new Subject[parents.Length];

            for(int i = 0; i < parents.Length; i+=2) {
                if(r.Next(100) < crossoverChance) {
                    children[i] = CrossInstances(parents[i], parents[i + 1]);
                    children[i+1] = CrossInstances(parents[i + 1], parents[i]);
                }
                else {
                    children[i] = parents[i];
                    children[i+1] = parents[i + 1];
                }
            }

            return children;
        }

        private Subject CrossInstances(Subject parent1, Subject parent2) {
            Subject child = new Subject();
            child.Cities = new int[parent1.Cities.Length];

            for(int i = 0; i < child.Cities.Length; i++) {
                child.Cities[i] = -1;
            }
            
            var borders = GetSwatchBorders(child.Cities.Length);
            int swatchStart = borders.start;
            int swatchEnd = borders.end;
            // int swatchStart, swatchEnd, first, second;
            // do {
            //     first = r.Next(child.Cities.Length);
            //     second = r.Next(child.Cities.Length);
            // } while(first == second);
            // if(first < second) {
            //     swatchStart = first;
            //     swatchEnd = second;
            // }
            // else {
            //     swatchStart = second;
            //     swatchEnd = first;
            // }
            // do {
            //     swatchStart = r.Next(child.Cities.Length);
            //     swatchEnd = r.Next(swatchStart, child.Cities.Length);
            // } while(swatchEnd < swatchStart || swatchEnd - swatchStart == child.Cities.Length);

            for(int i = swatchStart; i <= swatchEnd; i++) {
                child.Cities[i] = parent1.Cities[i];
            }

            for(int i = swatchStart; i <= swatchEnd; i++) {
                if(Array.IndexOf(child.Cities, parent2.Cities[i]) == -1) {
                    int currentIndex = i;
                    while(Array.IndexOf(parent1.Cities, parent2.Cities[currentIndex])  < swatchStart 
                        || Array.IndexOf(parent1.Cities, parent2.Cities[currentIndex]) > swatchEnd) {
                        currentIndex = Array.IndexOf(parent1.Cities, parent2.Cities[currentIndex]);
                    }
                    child.Cities[currentIndex] = parent2.Cities[i];
                }
            }

            for(int i = 0; i < child.Cities.Length; i++) {
                if(child.Cities[i] == -1) child.Cities[i] = parent2.Cities[i];
            }
            if(r.Next(100) < mutationChance) {
                InversionMutation(child.Cities);
            }
            
            child.Distance = PathService.GetSumDistance(child.Cities);
            return child;
        }

        private void InversionMutation(int[] cities) {

                // int swatchStart = r.Next(cities.Length - 1);
                // int length = r.Next(2, cities.Length - swatchStart);
                var borders = GetSwatchBorders(cities.Length);
                int swatchStart = borders.start;
                int length = borders.end - borders.start;

                Array.Reverse(cities, swatchStart, length);
        }

        private (int start, int end) GetSwatchBorders(int size) {
            int first, second;
            do {
                first = r.Next(size);
                second = r.Next(size);
            } while(first == second);
            return (first < second) ? (first, second) : (second, first);
        }
    }
}