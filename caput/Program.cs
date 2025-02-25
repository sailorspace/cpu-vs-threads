// See https://aka.ms/new-console-template for more information
using caput;

using static caput.Thrashing;
Console.WriteLine("Hello, World!");
Console.WriteLine("Threading Concepts Demo");

// CountdownEvent
Console.WriteLine("CountdownEvent Demo");
CountdownEventDemo();

// Lazy<T>
Console.WriteLine("Lazy<T> Demo");
LazyDemo();

// Singleton via LazyInitializer
Console.WriteLine("Singleton via LazyInitializer Demo");
SingletonDemo();

// Volatile
Console.WriteLine("Volatile Demo");
VolatileDemo();

// Interlocked.CompareExchange
Console.WriteLine("Interlocked.CompareExchange Demo");
InterlockedCompareExchangeDemo();

