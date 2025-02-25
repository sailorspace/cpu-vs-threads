using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caput
{
    public  class Thrashing
    {
        public static void CountdownEventDemo()
        {
            var countdownEvent = new CountdownEvent(3);

            for (int i = 0; i < 3; i++)
            {
                int index = i;
                Task.Run(() =>
                {
                    Console.WriteLine($"Task {index} is completing.");
                    //Thread.Sleep(20);
                    countdownEvent.Signal();
                });
            }

            Console.WriteLine("Waiting for tasks to complete...");
            countdownEvent.Wait();
            Console.WriteLine("All tasks have completed.");
        }

        public static void LazyDemo()
        {
            var lazyInt = new Lazy<int>(() =>
            {
                Console.WriteLine("Initializing lazy int...");
                return 42;
            });

            Console.WriteLine("Lazy int is not initialized yet.");
            Console.WriteLine("Value: " + lazyInt.Value);
            Console.WriteLine("Lazy int is now initialized.");
        }

        public static void SingletonDemo()
        {
            var singleton = Singleton.Instance;

            Task.Run(() =>
            {
                Console.WriteLine("Thread 1: " + singleton.InstanceId);
            });

            Task.Run(() =>
            {
                Console.WriteLine("Thread 2: " + singleton.InstanceId);
            });
        }

        public static void VolatileDemo()
        {
            var stopThread = false;
            var thread = new Thread(() =>
            {
                while (!stopThread)
                {
                    Console.WriteLine("Thread is running...");
                    Thread.Sleep(1000);
                }
            });

            thread.Start();

            Console.WriteLine("Press Enter to stop the thread...");
            Console.ReadLine();

            stopThread = true; // This change will be visible to the thread due to the volatile keyword
            thread.Join();
        }

        static volatile bool _stopThread = false;

        public static void VolatileDemoWithVolatileKeyword()
        {
            var thread = new Thread(() =>
            {
                while (!_stopThread)
                {
                    Console.WriteLine("Thread is running...");
                    Thread.Sleep(1000);
                }
            });

            thread.Start();

            Console.WriteLine("Press Enter to stop the thread...");
            Console.ReadLine();

            _stopThread = true; // This change will be visible to the thread due to the volatile keyword
            thread.Join();
        }

        public static void InterlockedCompareExchangeDemo()
        {
            int value = 0;

            Task.Run(() =>
            {
                if (Interlocked.CompareExchange(ref value, 1, 0) == 0)
                {
                    Console.WriteLine("Thread 1 updated the value to 1.");
                }
                else
                {
                    Console.WriteLine("Thread 1 failed to update the value.");
                }
            });

            Task.Run(() =>
            {
                if (Interlocked.CompareExchange(ref value, 2, 0) == 0)
                {
                    Console.WriteLine("Thread 2 updated the value to 2.");
                }
                else
                {
                    Console.WriteLine("Thread 2 failed to update the value.");
                }
            });
        }

    }
}
