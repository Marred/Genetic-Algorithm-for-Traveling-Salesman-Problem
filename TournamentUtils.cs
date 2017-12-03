using System;
using System.Collections.Generic;
using System.Linq;
using WdTIGS.Models;

namespace WdTIGS
{
    class TournamentUtils
    {
        private TournamentUtils() {
        }

        public static TournamentUtils Instance { get { return Nested.instance;  } }

        private class Nested
        {
            static Nested() { }
            internal static readonly TournamentUtils instance = new TournamentUtils();
        }

        public Instance[] Tournament(Instance[] baseInstances, int groupSize = 4) {
            Instance[] winners = new Instance[baseInstances.Length];
            Random random = new Random();
            Instance winner = new Instance();
            for(int i = 0; i < winners.Length; i++) {
                //List<int> list = Enumerable.Range(0, baseInstances.Length).ToList();
                //int temp;
                //Instance[] group = new Instance[groupSize];
                int minDistance = Int32.MaxValue;
                Instance temp;

                for (int j = 0; j < groupSize; j++)
                {
                    // temp = random.Next(list.Count);
                    // group.Add(baseInstances[temp];
                    // list.RemoveAt(temp);
                    temp = baseInstances[random.Next(baseInstances.Length)];
                    if(temp.Distance < minDistance) {
                        minDistance = temp.Distance;
                        winner = temp;
                    }
                }
                // int minDistance = group.Min(y => y.Distance);
                // winners[i] = group.First(x => x.Distance == minDistance);
                winners[i] = winner;
            }
            return winners;
        }
    }
}