using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static Dictionary<string, List<double>> traty = new Dictionary<string, List<double>>();
        static void Main(string[] args)
        {
            List<double> list = new List<double>() { 0, 0, 0, 0, 0, 0 };
            Console.WriteLine("Добро пожаловать в систему управления финансами!");
            bool konec = true;
            while (konec)
            {
                Console.WriteLine("1. Добавить доход/расход  \r\n2. Показать отчет  \r\n3. Рассчитать баланс  \r\n4. Прогноз на следующий месяц  \r\n5. Статистика\r\n6. Выход  \r\n");
                Console.Write("Выберите действие: ");
                string vibor = Console.ReadLine();
                switch (vibor)
                {
                    case "1":
                        AddTransaction();
                        break;

                    case "2":
                        PrintFinanceReport();
                        break;

                    case "3":
                        CalculateBalance();
                        break;

                    case "4": // не пишет
                        GetAverageExpense();
                        break;

                    case "5": // Общая сумма расходов: 25000 руб.  Самая затратная категория: Продукты(Продукты руб.) Самая частая категория: Продукты(Продукты операций)
                        PrintStatistics();
                        break;

                    case "6":
                        Console.WriteLine("Выход из программы.");
                        konec = false;
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
            Console.ReadKey();
        }
        public static void AddTransaction()
        {
            Console.WriteLine("Введите категорию(Доход, Продукты, Транспорт, Развлечения, Прочие расходы):");
            string category = Console.ReadLine();
            Console.Write("Введите сумму: ");
            double sum = Convert.ToInt32(Console.ReadLine());
            if (category != "Доход")
                sum *= -1; 
            traty[category] = new List<double>();
            traty[category].Add(sum);
            Console.WriteLine("Запись добавлена.\r\n");
        }
        public static void PrintFinanceReport()
        {
            Console.WriteLine("Финансовый отчет:");
            foreach (var category in traty)
                Console.WriteLine($"{category.Key}: {category.Value.Sum()} руб. - {category.Value.Count} операций\r\n");
        }
        public static void CalculateBalance()
        {
            double balense = 0;
            foreach (var opracia in traty)
                balense = balense + opracia.Value.Sum();
            Console.WriteLine($"Текущий баланс: {balense}\r\n");
        }
        public static void GetAverageExpense()
        {
            Console.WriteLine("Введите категорию(Доход, Продукты, Транспорт, Развлечения, Прочие расходы):");
            string category = Console.ReadLine();
            double sred = 0;
            foreach (var categor in traty)
            {
                if (categor.Key == category)
                    sred = categor.Value.Sum() / categor.Value.Count;
                else
                    Console.WriteLine($"\nНет данных для категории \"{category}\".\r\n");
            }
            PredictNextMonthExpenses();
        }
        public static void PredictNextMonthExpenses()
        {
            Console.WriteLine("\nПрогноз расходов на следующий месяц:");

            foreach (var entry in traty)
            {
                if (entry.Key != "Доход" && entry.Value.Count > 0)
                {
                    double sum = 0;
                    for (int i = 0; i < entry.Value.Count; i++)
                        sum += entry.Value[i];
                    double average = sum / entry.Value.Count;
                    Console.WriteLine($"{entry.Key}: {average * 4} (на 4 недели(примерно))\r\n");
                }
            }
        }
        public static void PrintStatistics()
        {
            double totalExpenses = 0;
            string mostExpensive = "";
            double maxExpenseAmount = 0;
            string mostFrequent = "";
            int maxFrequency = 0;

            foreach (var entry in traty)
            {
                if (!entry.Key.Equals("Доход"))
                {
                    double categoryTotal = 0;
                    for (int i = 0; i < entry.Value.Count; i++)
                    {
                        categoryTotal += entry.Value[i];
                    }
                    totalExpenses += categoryTotal;
                    if (categoryTotal > maxExpenseAmount)
                    {
                        maxExpenseAmount = categoryTotal;
                        mostExpensive = entry.Key;
                    }
                    if (entry.Value.Count > maxFrequency)
                    {
                        maxFrequency = entry.Value.Count;
                        mostFrequent = entry.Key;
                    }
                }
            }

            Console.WriteLine($"Общая сумма расходов: {totalExpenses} руб.");
            Console.WriteLine($"Самая затратная категория: {mostExpensive} ({mostExpensive} руб.)");
            Console.WriteLine($"Самая частая категория: {mostFrequent} ({mostFrequent} операций)\r\n");
        }
    }
}
