using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WdTIGS;

namespace VSCode
{
    class Program
    {
        static void Main(string[] args)
        {
            PathUtils utils = PathUtils.Instance;

            utils.RandomizeInstances(10);
            utils.SaveInstance(1);
        }
    }
}
