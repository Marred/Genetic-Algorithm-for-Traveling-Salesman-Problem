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

            for(int i = 0; i < 200000; i++) {
                Random r = new Random();
                instances = (r.Next(100) > 95) ? selectionService.Select(instances) : secondarySelectionService.Select(instances);
                //instances = selectionService.Select(instances);
                if(i< 150000) instances = crossoverService.CrossPopulation(instances, 85, 30);
                else instances = crossoverService.CrossPopulation(instances, 80, 15);
                minDitanceInIntance = instances.Min(x => x.Distance);
                if (min.Distance == 0 || min.Distance > minDitanceInIntance)
                {
                    Console.WriteLine($"Generacja: {i}, wynik: {minDitanceInIntance}");
                    min = instances.First(x => x.Distance == minDitanceInIntance);
                }
            }
            
            Console.WriteLine("Time elapsed: " + watch.Elapsed);

            pathService.SaveInstance(min);

            Console.ReadKey();
        }
    }
}
