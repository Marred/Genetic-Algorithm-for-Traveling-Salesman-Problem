using System;
using System.Collections.Generic;
using System.Linq;
using WdTIGS.Models;

namespace WdTIGS.Services
{
    class TournamentService : ISelectionService
    {
        private TournamentService() {
        }

        public static TournamentService Instance { get { return Nested.instance;  } }

        private class Nested
        {
            static Nested() { }
            internal static readonly TournamentService instance = new TournamentService();
        }

        public Subject[] Select(Subject[] baseInstances, int groupSize = 4) {
            Subject[] winners = new Subject[baseInstances.Length];
            Random random = new Random();
            Subject winner = new Subject();
            for(int i = 0; i < winners.Length; i++) {
                //List<int> list = Enumerable.Range(0, baseInstances.Length).ToList();
                //int temp;
                //Instance[] group = new Instance[groupSize];
                int minDistance = Int32.MaxValue;
                Subject temp;

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