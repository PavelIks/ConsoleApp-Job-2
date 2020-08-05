using System;

namespace RuleSimpsonJob
{
    public class Simpson
    {
        public delegate double Function(double x);

        public static double IntegrateEps(Function f, double a, double b, int n, double epsilon)
        {
            double result = 0.0, result1 = 0.0;
            do
            {
                result = Integrate(f, a, b, n);
                result1 = Integrate(f, a, b, n * 10);
                n *= 10;
            }
            while (Math.Abs(result - result1) > epsilon);
            {
                return result;
            }
        }

        public static double Integrate(Function f, double a, double b, int n)
        {
            if (n < 3) return double.NaN; //Need at least 3 points
            double sum = 0.0;
            double h = (b - a) / n;
            if (n % 2 != 0)
            {
                for (int i = 0; i < n - 1; i += 2)
                {
                    sum += h * (f(a + i * h) + 4 * f(a + (i + 1) * h) + f(a + (i + 2) * h)) / 3;
                }
            }
            else
            {
                sum = 3 * h * (f(a) + 3 * f(a + h) + 3 * f(a + 2 * h) + f(a + 3 * h)) / 8;
                for (int i = 3; i < n - 1; i += 2)
                {
                    sum += h * (f(a + i * h) + 4 * f(a + (i + 1) * h) + f(a + (i + 2) * h)) / 3;
                }
            }
            return sum;
        }
    }

    class Program
    {
        static double df(double x)
        {
            return Math.Cos(x);
        }
        static double f(double x)
        {
            return Math.Sin(x);
        }
        static double ft(double x)
        {

            return Math.Log(x) / (x * Math.Sqrt(1.0 + Math.Log(x)));
        }
        static void Main(string[] args)
        {
            int n = 100;
            double result;
            double a = 0;
            double b = 1;
            double epsilon = 0.00001;

            result = f(b) - f(a);
            Console.WriteLine("Analytic result: " + result.ToString());

            result = Simpson.IntegrateEps(df, a, b, n, epsilon);
            Console.WriteLine("Result using test function: " + result.ToString());
            a = 0.37;
            b = 3.37;
            result = Simpson.IntegrateEps(ft, a, b, n, epsilon);
            Console.WriteLine("Result using function: " + result.ToString());
            Console.ReadLine();
        }
    }
}