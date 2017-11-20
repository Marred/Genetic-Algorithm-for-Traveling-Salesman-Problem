using System;
using System.Collections.Generic;
using System.Linq;
using WdTIGS.Models;

namespace WdTIGS
{
    class RouletteUtils
    {
        private RouletteUtils() {
        }

        public static RouletteUtils Instance { get { return Nested.instance;  } }

        private class Nested
        {
            static Nested() { }
            internal static readonly RouletteUtils instance = new RouletteUtils();
        }

        public Instance[] Roulette(Instance[] baseInstances, int groupSize = 4) {
            Instance[] winners = new Instance[baseInstances.Length];
            Random random = new Random();
            for(int i = 0; i < baseInstances.Length; i++) {
                Instance[] group = new Instance[groupSize];
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