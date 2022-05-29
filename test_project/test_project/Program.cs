using System.Diagnostics;
using System.Reflection;

namespace test_project
{
    abstract class Inicialisation
    {
        public string One;
        public int Age;
        public double Money;
        public Inicialisation(string one, int age, double money)
        {
            One = one;
            Age = age;
            Money = money;
        }
        public abstract void Abstract();
    }
    class Program
    {
        public static void Analises()
        {
            string path = @"E:\Anna.txt";
            string TextString = ".,?!-/*”“'\"";
            string FileContents = File.ReadAllText(path);
            
            foreach (char c in TextString)
            {
                FileContents = FileContents.Replace(c, ' ');
            }
            FileContents = FileContents.Replace(Environment.NewLine, " ");
            string[] Words = FileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> map = new Dictionary<string, int>();
            foreach (string word in Words.Select(x => x.ToLower()))
            {
                if (map.TryGetValue(word, out int c))
                {
                    map[word] = c + 1;
                }
                else
                {
                    map.Add(word, 1);
                }
                Words = new string[1];
            }
            Console.WriteLine(map);
            List<Tuple<int, string>> Stats = map.Select(x => new Tuple<int, string>(x.Value, x.Key)).ToList();
            Stats.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            foreach (Tuple<int, string> pair in Stats)
            {
                File.AppendAllText(@"E:\stats.txt", pair.Item2 + " " + pair.Item1 + Environment.NewLine);
            }
        }
        static public void Main(string[] args)
        {
            //Console.WriteLine("Основной поток запущен");
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //Console.WriteLine("Анализ текста запущен паралельно");
            //Task task = new Task(Analises);
            //task.Start();
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //for (int i = 0; i < 60; i++)
            //{
            //    Console.Write(".");
            //    Thread.Sleep(100);
            //}
            //Console.WriteLine("\r\nНажмите любую клавишу для продолжения");
            //GC.Collect();
            //Console.Read();


            //Console.WriteLine("Анализ текста запущен однопоточно");
            //stopwatch.Start();
            //Program program = new Program();
            //Program.Analises();
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //for (int i = 0; i < 60; i++)
            //{
            //    Console.Write(".");
            //    Thread.Sleep(100);
            //}
            //Console.WriteLine("\r\nРабота программы завершена");
            //Console.ReadLine();

            Type type = typeof(Inicialisation);
            ConstructorInfo? info = type.GetConstructor(new Type[] { typeof(string), typeof(int), typeof(double) });
            object inicialisation = info.Invoke(new object[] { "Test", 20, 25.5  });
        }
    }
}