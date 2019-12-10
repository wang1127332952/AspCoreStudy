using ConsoleNetStudy.ThreadTask;
using System;

namespace ConsoleNetStudy
{
    class Program
    {
        static void Main(string[] args)
        {

            ///Thread学习
            //TreadStudy treadStudy = new TreadStudy();
           // treadStudy.ThreadFunc();

            //ThreadPool学习
           // TreadPoolStudy treadPoolStudy = new TreadPoolStudy();
           // treadPoolStudy.TreadPool();

            TaskStudy taskStudy = new TaskStudy();
            taskStudy.DoTaskStudy();
            // taskStudy.UpdateArray();
            Console.ReadLine();

        }
    }
}
