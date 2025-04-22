using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caput
{

    //checking the code scan for this new change in the object creation
    //start here
    public class Singularity
    {
        public Singleton GetInstance = Singleton.Instance;
    }
    //ends here
    public class Singleton
    {
        private static readonly Lazy<Singleton> _instance = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => _instance.Value;

        public int InstanceId { get; } = Guid.NewGuid().GetHashCode();

        private Singleton() { }
    }

    
}


