using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomClasses
{
    public interface IRandomGenerator
    {
        public int seed { get; }

        public int getRandom();

        public int getRandom(int inclusiveMinimum, int exclusiveMaximum);
}

    public class RandomGenerator : IRandomGenerator
    {
        private Random random;
        public int seed { get; }

        public RandomGenerator()
        {
            seed = (int)DateTime.Now.Ticks;
            random = new Random(seed);
        }

        public int getRandom()
        {
            return random.Next();
        }
        public int getRandom(int inclusiveMinimum, int exclusiveMaximum)
        {
            return random.Next(inclusiveMinimum, exclusiveMaximum);
        }
    }
}
