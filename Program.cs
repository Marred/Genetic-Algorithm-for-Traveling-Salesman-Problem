using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WdTIGS;
using WdTIGS.Models;

namespace VSCode
{
    class Program
    {
        static Instance[] instances;
        static void Main(string[] args)
        {
            PathUtils pathUtils = PathUtils.Instance;
            TournamentUtils tournamentUtils = TournamentUtils.Instance;

            instances = pathUtils.RandomizeInstances(20);

            Instance[] winners = tournamentUtils.Tournament(instances, 4);
            
            int minDistance = winners.Min(y => y.Distance);
            pathUtils.SaveInstance(winners.First(x => x.Distance == minDistance));
        }
    }
}
