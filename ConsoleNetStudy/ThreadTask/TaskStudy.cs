using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleNetStudy.ThreadTask
{
    /// <summary>
    /// 多线程 Task学习
    /// </summary>
    public class TaskStudy
    {
        #region Task解读
        public void DoTaskStudy()
        {
            Task.Run(() => Transformation("wht", "AspNetCore"));
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => Transformation("XXL", "AspNetCore")));
            tasks.Add(Task.Run(() => Transformation("WJ", "AspNetCore")));
            tasks.Add(Task.Run(() => Transformation("LDP", "AspNetCore")));
            TaskFactory taskFactory = new TaskFactory();

            taskFactory.ContinueWhenAny(tasks.ToArray(), tArray =>
            {
                Console.WriteLine($"其中一个做完,{Thread.CurrentThread.ManagedThreadId}");
            });

            taskFactory.ContinueWhenAll(tasks.ToArray(), tArray =>
            {
                Console.WriteLine($"全部已经做完,{Thread.CurrentThread.ManagedThreadId}");
            });

            //可以保证顺序
            tasks.Add(taskFactory.ContinueWhenAll(tasks.ToArray(), tArray =>
            {
                Console.WriteLine($"全部已经做完,{Thread.CurrentThread.ManagedThreadId}");
            }));
            Task.WaitAny(tasks.ToArray());
            Console.WriteLine($"其中一个做完,{Thread.CurrentThread.ManagedThreadId}");
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"全部已经做完,{Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        ///  提升自己
        /// </summary>
        /// <param name="peopleName">学员名字</param>
        /// <param name="subjectName">学科</param>
        private void Transformation(string peopleName, string subjectName)
        {
            //一年刻苦学习

        }

        #endregion

        #region 多线程安全学习
        //锁的应用和扩展
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void AddData()
        {
            for (int i = 0; i < 6; i++) 
            {
                int k = i;
                Task.Run(() =>
                {
                    Console.WriteLine($"Task start i:{i},k:{k},{Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"Task end i:{i},k:{k},{Thread.CurrentThread.ManagedThreadId}");

                });
            }

        }

        //多线程去访问同一个集合一般没问题，线程问题一般都出现在修改一个对象的过程中
        private void UpdateArray() 
        {
            List<int> vs = new List<int>();
            for (int i = 0; i < 10000; i++) 
            {
                //多线程之后数据小于10000
                //List是数组,在内存上连续摆放，同一时刻去增加一个数组，都是操作内存同一个位置
                //两个cpu 同时发送指令，内存先执行一个，在执行一个，就会出现覆盖
                Task.Run(() =>
                {
                    //Monitor.Enter(locks);
                    lock (locks) 
                    {
                        vs.Add(i);
                    }
                    //Monitor.Exit(locks);
                });
            }

            //线程安全定义：一段代码 单线程执行和多线程执行结果不一致就说明有线程安全问题

            //解决线程安全问题
            //锁 加锁可以解决安全 --单线程化  lock 保证 方法快任意时刻只有一个线程在执行
            //lock 语法糖 等价于Monitor 首先锁定一个（内存）引用地址--不能锁定 值类型 也不能是null
            //null 是一个占据引用，需要一个引用
            //lock 相关


        }
        private static readonly object locks = new object(); 


    }

}
