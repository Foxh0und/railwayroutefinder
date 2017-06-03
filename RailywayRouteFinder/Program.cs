using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailywayRouteFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            BluePrint lBluePrint = new BluePrint( args[0] );
            Conductor lConductor = new Conductor( lBluePrint );

            lConductor.askQuestions();

            Console.Read(); 
        }
    }
}
