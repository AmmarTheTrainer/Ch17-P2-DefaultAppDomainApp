using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ch17_P2_DefaultAppDomainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the default AppDomain *****\n");
            //InternetSurfuing();
            //DisplayDADStats();
            //ListAllAssembliesInAppDomain();
            //ListAllAssembliesInAppDomainUsingLINQ();

            InitDAD();

            Console.ReadLine();
        }

        private static void InitDAD()
        {
            // This logic will print out the name of any assembly
            // loaded into the applicaion domain, after it has been
            // created.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            defaultAD.AssemblyLoad += (o, s) =>
            {
                Console.WriteLine("{0} has been loaded!", s.LoadedAssembly.GetName().Name);
            };

            defaultAD.Load("Ch14-P2-CarLibrary");

            //defaultAD.AssemblyLoad += DefaultAD_AssemblyLoad;

        }

        private static void DefaultAD_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("{0} has been loaded!", args.LoadedAssembly.GetName().Name);
        }

        private static void InternetSurfuing()
        {
        }

        static void ListAllAssembliesInAppDomain()
        {
            // Get access to the AppDomain for the current thread.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            // Now get all loaded assemblies in the default AppDomain.
            Assembly[] loadedAssemblies = defaultAD.GetAssemblies();
            Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n",
            defaultAD.FriendlyName);
            foreach (Assembly a in loadedAssemblies)
            {
                Console.WriteLine("-> Name: {0}", a.GetName().Name);
                Console.WriteLine("-> Version: {0}\n", a.GetName().Version);
            }
        }
        static void ListAllAssembliesInAppDomainUsingLINQ()
        {
            // Get access to the AppDomain for the current thread.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            // Now get all loaded assemblies in the default AppDomain.
            var loadedAssemblies = from a in defaultAD.GetAssemblies()
                                   orderby a.GetName().Name
                                   select a;
            Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n",
            defaultAD.FriendlyName);
            foreach (var a in loadedAssemblies)
            {
                Console.WriteLine("-> Name: {0}", a.GetName().Name);
                Console.WriteLine("-> Version: {0}\n", a.GetName().Version);
            }
        }

        private static void DisplayDADStats()
        {
            // Get access to the AppDomain for the current thread.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            // Print out various stats about this domain.
            Console.WriteLine("Name of this domain: {0}", defaultAD.FriendlyName);
            Console.WriteLine("ID of domain in this process: {0}", defaultAD.Id);
            Console.WriteLine("Is this the default domain?: {0}", defaultAD.IsDefaultAppDomain());
            Console.WriteLine("Base directory of this domain: {0}", defaultAD.BaseDirectory);
        }
    }
}
