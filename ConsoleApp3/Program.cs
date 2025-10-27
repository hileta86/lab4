using System;

namespace ConsoleApplication1
{
    class Program
    {
        public static double f(double x)
        {
            return x * x - 4;
        }

        public static double fp(double x, double D)
        {
            return (f(x + D) - f(x)) / D;
        }

        public static double f2p(double x, double D)
        {
            return (f(x + D) + f(x - D) - 2 * f(x)) / (D * D);
        }

        public static (double? Root, int Iterations) BisectionMethod(double a, double b, double Eps)
        {
            int Lich = 0;
            double c;

            if (f(a) * f(b) > 0)
            {
                Console.WriteLine("На вказаному інтервалі корінь відсутній");
                return (null, 0);
            }

            if (Math.Abs(f(a)) < Eps) return (a, 0);
            if (Math.Abs(f(b)) < Eps) return (b, 0);

            while (Math.Abs(b - a) > Eps)
            {
                c = (a + b) / 2.0;
                Lich++;

                if (Math.Abs(f(c)) < Eps)
                {
                    return (c, Lich);
                }
                else if (f(a) * f(c) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
            }

            return ((a + b) / 2.0, Lich);
        }

        public static (double? Root, int Iterations) NewtonMethod(double a, double b, double Eps, int Kmax)
        {
            
            double D = Eps / 100.0;
            double x, Dx;

            x = b;
            if (f(x) * f2p(x, D) < 0)
            {
                x = a;
            }

            if (f(x) * f2p(x, D) <= 0)
            {
                Console.WriteLine("Для заданого рівняння збіжність методу Ньютона не гарантується");
                return (null, 0);
            }

            for (int i = 1; i <= Kmax; i++)
            {
                Dx = f(x) / fp(x, D);
                x = x - Dx;

                if (Math.Abs(Dx) <= Eps)
                {
                    return (x, i);
                }
            }

            Console.WriteLine($"За {Kmax} ітерацій корінь з точністю eps не знайдено.");
            return (null, Kmax);
        }

        static void Main(string[] args)
        {
            double a, b, Eps;
            int Kmax;

         
            Console.Write("лiва межа локалiзацiї a): ");
            a = Convert.ToDouble(Console.ReadLine());

            Console.Write("права межа локалiзацiї b: ");
            b = Convert.ToDouble(Console.ReadLine());

            Console.Write("точнiсть eps ");
            Eps = Convert.ToDouble(Console.ReadLine());

            Console.Write("макс. кількiсть iтерацiй Kmax (для МН): ");
            Kmax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nвиберiть: 1 або 2");
            Console.WriteLine("1. Метод Ділення Навпіл (МДН)");
            Console.WriteLine("2. Метод Ньютона (МН)");
            string choice = Console.ReadLine();

            (double? Root, int Iterations) result = (null, 0);
            bool methodChosen = false;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nобрано: Метод Ділення Навпіл");
                    result = BisectionMethod(a, b, Eps);
                    methodChosen = true;
                    break;

                case "2":
                    Console.WriteLine("\nобрано: Метод Ньютона");
                    result = NewtonMethod(a, b, Eps, Kmax);
                    methodChosen = true;
                    break;

                default:
                    Console.WriteLine("Невірний вибір. Завершення програми.");
                    break;
            }

            if (methodChosen)
            {
                Console.WriteLine("\nспільний результат");
                if (result.Root.HasValue)
                {
                    Console.WriteLine($"знайдено корінь: {result.Root.Value}");
                    Console.WriteLine($"виконано ітерцій: {result.Iterations}");
                }
                else
                {
                    Console.WriteLine("корінь не було знайдено.");
                }
            }

            Console.WriteLine("\nEnter для завершення...");
            Console.ReadLine();
        }
    }
}