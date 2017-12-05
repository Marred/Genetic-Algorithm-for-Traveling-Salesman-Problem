using System;
using System.Collections.Generic;
using System.Linq;
using WdTIGS.Models;

namespace WdTIGS.Services
{
    class RouletteService : ISelectionService
    {
        private RouletteService() {
        }

        public static RouletteService Instance { get { return Nested.instance;  } }

        private class Nested
        {
            static Nested() { }
            internal static readonly RouletteService instance = new RouletteService();
        }

        public Subject[] Select(Subject[] baseInstances, int groupSize = 4) {
            Subject[] winners = new Subject[baseInstances.Length];
            Random random = new Random();
            for(int i = 0; i < baseInstances.Length; i++) {
                Subject[] group = new Subject[groupSize];
                int maxDistance = Int32.MinValue;

                for(int j = 0; j < groupSize; j++) {
                    group[j] = baseInstances[random.Next(baseInstances.Length)];
                    if(group[j].Distance > maxDistance) maxDistance = group[j].Distance;
                }

                int[] scores = new int[groupSize];
                int scoresSum = 0;
                
                for(int j = 0; j < groupSize; j++) {
                    scores[j] = maxDistance + 1 - group[j].Distance;
                    scoresSum += scores[j];
                }

                int k = 0;
                int r = random.Next(scoresSum);
                for(int j = 0; j < groupSize; j++) {
                    k+= scores[j];
                    if(r < k) {
                        winners[i] = group[j];
                        break;
                    }
                }
            }
            
            return winners;
        }
    }
}