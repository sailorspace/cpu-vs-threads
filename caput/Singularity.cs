using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caput
{
    internal class Singularity
    {
    }

    class Singleton
    {
        private static readonly Lazy<Singleton> _instance = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => _instance.Value;

        public int InstanceId { get; } = Guid.NewGuid().GetHashCode();

        private Singleton() { }
    }

    
}


