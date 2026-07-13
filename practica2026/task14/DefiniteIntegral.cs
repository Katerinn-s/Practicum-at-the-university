using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task14
{
    public static class DefiniteIntegral
    {
        public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
        {
            double totalSum = 0.0;

            if (threadsNumber < 1)
                threadsNumber = 1;

            double segmentLength = (b - a) / threadsNumber;
            Barrier barrier = new Barrier(threadsNumber + 1);

            for (int i = 0; i < threadsNumber; i++)
            {
                int localIndex = i;
                double start = a + localIndex * segmentLength;
                double end = (localIndex == threadsNumber - 1) ? b : start + segmentLength;

                new Thread(() =>
                {
                    double localSum = ComputeSegment(start, end, function, step);
                    double current;
                    double newTotal;
                    do
                    {
                        current = totalSum;
                        newTotal = current + localSum;
                    }
                    while (Interlocked.CompareExchange(ref totalSum, newTotal, current) != current);
                    barrier.SignalAndWait();
                }).Start();
            }
            barrier.SignalAndWait();
            return totalSum;
        }

        private static double ComputeSegment(double a, double b, Func<double, double> function, double step)
        {
            int segments = (int)Math.Ceiling((b - a)/step);
            double nowStep = (b - a)/segments;
            double sum = 0.0;
            for (int i = 1; i < segments; i++)
            {
                double x = a + i * nowStep;
                sum += function(x);
            }
            return nowStep * ((function(a) + function(b)) / 2.0 + sum);
        }
    }
}
