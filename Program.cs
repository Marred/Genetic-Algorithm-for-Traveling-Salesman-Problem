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
            ISelectionService selectionService = RouletteService.Instance;
            ISelectionService secondarySelectionService = TournamentService.Instance;
            PmxCrossoverService crossoverService = PmxCrossoverService.Instance;

            Subject min = new Subject();
            int minDitanceInIntance = 0;

            instances = pathService.RandomizeInstances(40);

            for(int i = 0; i < 100000; i++) {
                Random r = new Random();
                instances = (r.Next(100) > 97) ? selectionService.Select(instances) : secondarySelectionService.Select(instances);
                //instances = secondarySelectionService.Select(instances);
                //instances = selectionService.Select(instances);
                if (i< 80000) instances = crossoverService.CrossPopulation(instances, 80, 45);
                else instances = crossoverService.CrossPopulation(instances, 90, 15);
                minDitanceInIntance = instances.Min(x => x.Distance);
                if (min.Cities == null || min.Distance > minDitanceInIntance)
                {
                    min = instances.First(x => x.Distance == minDitanceInIntance);
                    Console.WriteLine($"Generacja: {i}, wynik: {min.Distance}");
                }
            }
            
            Console.WriteLine("Time elapsed: " + watch.Elapsed);

            pathService.SaveInstance(min);

            Console.ReadKey();
        }
    }
}
