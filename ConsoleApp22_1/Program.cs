using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp3_4
{
    public class Program
    {        
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива");
            int lenght = 0;
            try
            {
                lenght = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Не число");
            }
            object[] array = new object[lenght];
            Task taskarray = new Task(() =>
            {
                Console.WriteLine("Начало потока 1");
                for (int i=0; i<lenght;i++)
                {
                    array[i] = new Random().Next(0,100);
                    Console.WriteLine(array[i]);
                    Thread.Sleep(200);
                }
                Console.WriteLine("Конец потока 1");
            });
            
            Action<Task, object> action = new Action<Task, object>(Thread2);
            Task task2 = taskarray.ContinueWith(action, array);
            taskarray.Start();
            task2.Wait();
        }

        static void Thread2(Task task, object objarray)
        {
            Console.WriteLine("Начало потока 2");
            Thread.Sleep(1000);
            int max = 0;
            int sum = 0;
            var list = (object[])objarray;
           foreach (int e in list)
            {
                sum+= e;
                if (e>max)
                {
                    max = e;
                }                
            }

            Console.WriteLine($"Сумма элементов: {sum}");
            Console.WriteLine($"Максимальное число: {max}");
            Console.WriteLine("Конец потока 2");
            Console.ReadKey();
        }
    }
}



