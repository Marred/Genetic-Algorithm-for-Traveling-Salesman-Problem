using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch watch = new Stopwatch();
            watch.Start();
            PathUtils pathUtils = PathUtils.Instance;
            TournamentUtils tournamentUtils = TournamentUtils.Instance;
            RouletteUtils rouletteUtils = RouletteUtils.Instance;


            instances = pathUtils.RandomizeInstances(10000000);


            Instance[] winners = tournamentUtils.Tournament(instances, 4);
            //Instance[] winners = rouletteUtils.Roulette(instances);
            
            Console.WriteLine("Time elapsed: " + watch.Elapsed);
            
            int minDistance = winners.Min(y => y.Distance);

            pathUtils.SaveInstance(winners.First(x => x.Distance == minDistance));
        }
    }
}
