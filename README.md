# cpu-vs-threads
#C-A-P-U-T => CPU Address Process Utilization with Threads
trying how multi cores and threads fight to process data between cpu + L1 + L2 + L3 + RAM + SSD

//to use
+ CountDownEvent
+ Barrier 
+ Memory.Barrier for full fencing 
+ ReaderWriterLockSlim  
+ Multi/Single THread timers and elasped time and tick overlap issues
+ Volatile Half Fencing vs VolatileRead and VolatileWrite Full Fencing

Next check Commit
If the Singleton class is marked as internal but its methods and properties are marked as public, the Singularity class (which is public) can still access the Singleton methods and properties as long as they are within the same assembly. Here's why:

Class Accessibility:

The internal modifier restricts the visibility of the Singleton class to the same assembly.
This means that the Singleton class cannot be accessed from outside the assembly, regardless of the accessibility of its members.
Public Members within the Same Assembly:

Within the same assembly, the public members of the internal Singleton class are treated as public for any code that has access to the Singleton class. This includes the Singularity class.
Singularity Class Exposure:

Since the Singularity class is public, it can be accessed by any external code.
The Singularity class can act as a bridge, exposing the functionality of the Singleton class (via its public methods or properties) to external code.
Example:
C#
namespace caput
{
    public class Singularity
    {
        public Singleton GetInstance => Singleton.Instance; // Accesses the public method of the internal class
    }

    internal class Singleton
    {
        private static readonly Lazy<Singleton> _instance = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => _instance.Value; // Public property accessible within the assembly

        public int InstanceId { get; } = Guid.NewGuid().GetHashCode();

        private Singleton() { }
    }
}
