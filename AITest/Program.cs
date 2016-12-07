using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Compute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            var cfg = new IgniteConfiguration { BinaryConfiguration = new BinaryConfiguration(typeof(CountFunc)), ClientMode = true };

            using (var ignite = Ignition.Start(cfg))
            {                
                int n = 10;
                int sum = 0;

                int[][] f = new int[n][];

                for(int i = 0; i < n; i++)
                {
                    f[i] = new int[n];

                    for(int j = 0; j < n; j++)
                    {
                        f[i][j] = r.Next(1, 10);

                        Console.Write(f[i][j] + " ");
                    }
                    Console.WriteLine();
                }

                int res = ignite.GetCompute().Apply(new CountFunc(), f);

                foreach (var ul in res)
                {
                    sum += ul;                    
                }

                Console.WriteLine("Sum " + sum);
                Console.Read();
            }            
        }
    }

    internal class CountFunc : IComputeFunc<int[], int>
    {
        public int Invoke(int[] arg)
        {            
            return 0;
        }
    }
}
