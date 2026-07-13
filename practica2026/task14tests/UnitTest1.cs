
using System.Diagnostics;
using task14;
using Xunit;
using ScottPlot;

namespace task14tests
{
    public class UnitTest1
    {
        [Fact]
        public void IntegralXFromMinus1To1Returns0()
        {
            var X = (double x) => x;
            double result = DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2);
            Assert.Equal(0, result, 4);
        }

        [Fact]
        public void IntegralSinFromMinus1To1Returns0()
        {
            var SIN = (double x) => Math.Sin(x);
            double result = DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8);
            Assert.Equal(0, result, 4);
        }

        [Fact]
        public void IntegralXFrom0To5Returns12point5()
        {
            var X = (double x) => x;
            double result = DefiniteIntegral.Solve(0, 5, X, 1e-6, 8);
            Assert.Equal(12.5, result, 4);
        }

        [Fact]
        public void FindStepWithProfit()
        {
            Func<double, double> X = Math.Sin;
            double a = -100;
            double b = 100;
            double precision = 1e-4;
            double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
            int repeats = 5;
            double testStep = 1e-3;
            int[] threadCounts = { 1, 2, 4, 8, 16, 32 };
            int bestThreads = 1;
            double bestMultTime = double.MaxValue;

            foreach (int t in threadCounts)
            {
                double time = MeasureTime(() => DefiniteIntegral.Solve(a, b, X, testStep, t), repeats);
                if (time < bestMultTime)
                {
                    bestMultTime = time;
                    bestThreads = t;
                }
            }

            foreach (var step in steps)
            {
                double result = DefiniteIntegral.Solve(a, b, X, step, 1);
                double error = Math.Abs(result - 0.0);
                if (error > precision) continue;
                double singleTime = MeasureTime(() => ComputeSingleThread(a, b, X, step), repeats);
                double multiTime = MeasureTime(() => DefiniteIntegral.Solve(a, b, X, step, bestThreads), repeats);
                double profit = (singleTime - multiTime) / singleTime * 100;

                if (profit > 15)
                {
                    Console.WriteLine($"Оптимальный шаг: {step:E1}");
                    Console.WriteLine($"Оптимальное число потоков: {bestThreads}");
                    Console.WriteLine($"Время работы однопоточной версии: {singleTime:F4} мс");
                    Console.WriteLine($"Время работы лучшей многопоточной версии: {multiTime:F4} мс");
                    Console.WriteLine($"Разница в процентах: {profit:F2}%");

                    string filePath = "profit_result.txt";
                    using (var writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Оптимальный шаг: {step:E1}");
                        writer.WriteLine($"Оптимальное число потоков: {bestThreads}");
                        writer.WriteLine($"Время работы однопоточной версии: {singleTime:F4} мс");
                        writer.WriteLine($"Время работы лучшей многопоточной версии: {multiTime:F4} мс");
                        writer.WriteLine($"Разница в процентах: {profit:F2}%");
                    }
                    return;
                }
            }
            Console.WriteLine("\nНе найден шаг, где многопоточная версия работает на 15% быстрее однопоточной");
            Assert.Fail("Ни для одного шага ускорение не превысило 15%");
        }

        [Fact]
        public void PerformanceTest()
        {
            Func<double, double> f = Math.Sin;
            double a = -100;
            double b = 100;
            double precision = 1e-4;
            double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
            int[] threadCounts = { 1, 2, 4, 8, 16, 32 };
            int repeats = 5;
            string filePath = "results.txt";

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Исследование производительности интеграла sin(x) [-100,100]\n");
            }
            double bestStep = steps[0];
            double bestTime = double.MaxValue;

            foreach (var step in steps)
            {
                double result = DefiniteIntegral.Solve(a, b, f, step, 1);
                double error = Math.Abs(result - 0.0);

                if (error > precision)
                {
                    Console.WriteLine($"Шаг {step}: точность {error:E2} > {precision} не подходит");
                    continue;
                }

                double avgTime = MeasureTime(() => DefiniteIntegral.Solve(a, b, f, step, 1), repeats);
                Console.WriteLine($"Шаг {step}: время = {avgTime:F2} мс, точность = {error:E2}");

                if (avgTime < bestTime)
                {
                    bestTime = avgTime;
                    bestStep = step;
                }
            }

            Console.WriteLine($"\nОптимальный шаг: {bestStep} (время {bestTime:F2} мс)\n");
            var threadResults = new List<(int threads, double time, double result)>();

            foreach (int threads in threadCounts)
            {
                double avgTime = MeasureTime(() => DefiniteIntegral.Solve(a, b, f, bestStep, threads), repeats);
                double result = DefiniteIntegral.Solve(a, b, f, bestStep, threads);
                threadResults.Add((threads, avgTime, result));
                Console.WriteLine($"Потоков: {threads}, время: {avgTime:F2} мс, результат: {result:F6}");
            }

            var timeData = threadResults.Select(t => t.time).ToArray();
            var threadData = threadResults.Select(t => (double)t.threads).ToArray();
            var plt = new ScottPlot.Plot();
            plt.Add.Scatter(timeData, threadData);
            plt.Title("замеры производительностит многопоточности");
            plt.XLabel("Время выполнения, мс");
            plt.YLabel("Количество потоков");
            plt.SavePng("graph.png", 800, 600);
            Console.WriteLine("График сохранён как graph.png");
            double singleTime = MeasureTime(() => ComputeSingleThread(a, b, f, bestStep), repeats);
            Console.WriteLine($"\nОднопоточная версия: {singleTime:F2} мс");
            var bestThread = threadResults.OrderBy(t => t.time).First();
            Console.WriteLine($"\nЛучшее число потоков: {bestThread.threads}, время: {bestThread.time:F2} мс");

            double diffPercent = (singleTime - bestThread.time) / singleTime * 100;
            Console.WriteLine($"\nМногопоточный быстрее на {diffPercent:F1}%");

            using (var writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine($"Оптимальный шаг: {bestStep}");
                writer.WriteLine($"Оптимальное число потоков: {bestThread.threads}");
                writer.WriteLine($"Время многопоточной версии: {bestThread.time:F2} мс");
                writer.WriteLine($"Время однопоточной версии: {singleTime:F2} мс");
                writer.WriteLine($"Разница: {diffPercent:F1}%");
                writer.WriteLine("Замеры по потокам:");
                foreach (var t in threadResults)
                    writer.WriteLine($"{t.threads} поток(ов): {t.time:F2} мс");
            }

            if (diffPercent < 15)
            {
                Console.WriteLine("\nУскорение меньше 15% — многопоточная версия требует оптимизации");
            }
            else
            {
                Console.WriteLine($"\nМногопоточная версия даёт выигрыш {diffPercent:F1}%");
            }
        }

        private double MeasureTime(Func<double> action, int repeats)
        {
            var times = new List<double>();
            for (int i = 0; i < repeats; i++)
            {
                var sw = Stopwatch.StartNew();
                action();
                sw.Stop();
                times.Add(sw.Elapsed.TotalMilliseconds);
            }
            times.Sort();
            times.RemoveAt(0);
            times.RemoveAt(times.Count - 1);
            return times.Average();
        }

        private double ComputeSingleThread(double a, double b, Func<double, double> f, double step)
        {
            int segments = (int)Math.Ceiling((b - a) / step);
            double actualStep = (b - a) / segments;
            double sum = 0.0;
            for (int i = 1; i < segments; i++)
            {
                double x = a + i * actualStep;
                sum += f(x);
            }
            return actualStep * ((f(a) + f(b)) / 2.0 + sum);
        }
    }
}

