using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WdTIGS;
using WdTIGS.Models;
using WdTIGS.Services;

namespace VSCode
{
    class Program
    {
        static Subject[] instances;
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            PathService pathService = PathService.Instance;
            ISelectionService selectionService = TournamentService.Instance;
            PmxCrossoverService crossoverService = PmxCrossoverService.Instance;

            instances = pathService.RandomizeInstances(40);

            for(int i = 0; i < 400000; i++) {
                instances = selectionService.Select(instances, 4);
                instances = crossoverService.CrossPopulation(instances, 50, 10);
            }
            
            Console.WriteLine("Time elapsed: " + watch.Elapsed);
            
            int minDistance = instances.Min(y => y.Distance);
            double avarage = instances.Average(y => y.Distance);
            Console.WriteLine("Avarage distance: " + avarage);

            pathService.SaveInstance(instances.First(x => x.Distance == minDistance));
        }
    }
}
