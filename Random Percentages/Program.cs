using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Percentages
{
    class Program
    {
        Random rand;

        public Program()
        {
            rand = new Random();
        }

        public int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return (a == 0) ? b : a;
        }

        public bool IsHit(double target)
            /*<summary>IsHit returns whether a randomly
             * generated number between 0 and 1 is less 
             * than or equal to the target.
             * </summary>
             */
        {
            return rand.NextDouble() <= target;
        }

        public bool ShouldHit(double actual, double target, double epsilon)
            /* <summary>ShouldHit returns true as long as the absolute value
             * of the difference between the actual current result and the
             * desired result is not less than or equal to the acceptable 
             * epsilon value.
             * </summary>
             */
        {
            return !(Math.Abs(actual - target) <= epsilon);
        }

        static void Main(string[] args)
            /* <summary>Main requests the desired result and acceptable
             * epsilon value from the user and continuously finds random
             * numbers until the fraction of numbers within range and total
             * number of tries is equal to the desired result. Then stats 
             * are printed about the results.
             * </summary>
             */
        {
            Program prog = new Program();
            bool run;

            do
            {
                run = false;
                double target = 0.0, epsilon = 0.0;
                Console.Write("Enter your desired percentage: ");
                target = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter your epsilon: ");
                epsilon = Convert.ToDouble(Console.ReadLine());
                target /= 100;
                epsilon /= 100;

                double hits = 0.0, runs = 0.0;
                do
                {
                    if (prog.IsHit(target)) hits++;
                    runs++;

                } while (prog.ShouldHit(hits / runs, target, epsilon));

                Console.WriteLine("Your desired result of {0}% was achieved after {1} rolls.", target * 100, runs);
                Console.WriteLine("The final result is {0}%.\nMeaning {1} tries [out of {2}] yielded " +
                    "a result less than or equal to {3}%.", hits / runs * 100, hits, runs, target * 100);
                int gcd = prog.GCD((int)hits, (int)runs);
                if (gcd != 1)
                    Console.WriteLine("However a better result could have been found [{0} / {1}]." +
                        "\nThis means the operation took {2} times longer than necesarry.", (int)hits / gcd, (int)runs / gcd, gcd);

                Console.Write("Would you like to try another percentage? [Y/N] ");
                char reply = Convert.ToChar(Console.ReadLine());
                if (reply.Equals('Y') || reply.Equals('y'))
                    run = true;
            } while (run);

            Console.WriteLine("Press enter to continue...");
            Console.ReadKey(true);
        }
    }
}
