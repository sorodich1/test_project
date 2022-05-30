using System.Diagnostics;

namespace test_project
{
    class Program
    {
        public static void Analises()
        {
            string path = @"E:\Anna.txt";
            string TextString = ".,?!-/*”“'\"";
            string FileContents = File.ReadAllText(path);
            //Подсчёт колличества всевозможных знаков в тексте
            foreach (char c in TextString)
            {
                FileContents = FileContents.Replace(c, ' ');
            }
            FileContents = FileContents.Replace(Environment.NewLine, " ");
            string[] Words = FileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> map = new Dictionary<string, int>();
            //Подсчёт колличества слов
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
            //Сбор статистики
            foreach (Tuple<int, string> pair in Stats)
            {
                File.AppendAllText(@"E:\stats.txt", pair.Item2 + " " + pair.Item1 + Environment.NewLine);
                return;
            }
        }
        static public void Main(string[] args)
        {
            Console.WriteLine("Основной поток запущен");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Анализ текста запущен паралельно");
            Task task = new Task(Analises);
            task.Start();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            for (int i = 0; i < 60; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine("\r\nНажмите любую клавишу для продолжения");
            //Очистка мусора и пауза для разграничения двух методов
            GC.Collect();
            Console.Read();


            Console.WriteLine("Анализ текста запущен однопоточно");
            stopwatch.Start();
            Program program = new Program();
            Program.Analises();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            for (int i = 0; i < 60; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine("\r\nРабота программы завершена");
            Console.ReadLine();
        }
    }
}