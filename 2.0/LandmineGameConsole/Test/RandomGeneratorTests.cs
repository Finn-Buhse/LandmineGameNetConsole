using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ApplicationClasses;
using RandomClasses;

namespace Testing
{
    [TestClass]
    public class RandomGeneratorTests
    {
        IHost? host;
        IRandomGenerator? randomGenerator1;
        IRandomGenerator? randomGenerator2;

        public void givenHostContainerCreated()
        {
            host = HostContainer.CreateHostContainer();
        }

        public void givenRandomGeneratorCreated()
        {
            randomGenerator1 = host!.Services.GetRequiredService<IRandomGenerator>();
        }

        public void givenTwoRandomGeneratorsCreated()
        {
            randomGenerator1 = host!.Services.GetRequiredService<IRandomGenerator>();
            randomGenerator2 = host!.Services.GetRequiredService<IRandomGenerator>();
        }

        public void thenTheTwoRandomGeneratorsHaveDifferentSeeds()
        {
            Assert.AreNotEqual(randomGenerator1!.seed, randomGenerator2!.seed);
        }
        public void thenGetNumberWithMaximumEqualToMinimumPlusOneAlwaysReturnsMinimum()
        {
            // Try this one hundred times
            // If the bounds really are off, the generator could return the correct number every time out of luck instead of being properly contrained
            // This minimises the chances of that happening, however it's still questionable whether to include such a test
            for (int i = 0; i < 100; i++) 
            {
                Assert.AreEqual(randomGenerator1!.getRandom(5, 6), 5);
            }
        }

        [TestMethod]
        public void RandomGeneratorInstancesHaveUniqueSeed()
        {
            givenHostContainerCreated();
            givenTwoRandomGeneratorsCreated();

            thenTheTwoRandomGeneratorsHaveDifferentSeeds();
        }

        [TestMethod]
        public void RandomlyGeneratedNumbersUsingMinimumAndMaximumAreWithinBounds()
        {
            givenHostContainerCreated();
            givenRandomGeneratorCreated();

            thenGetNumberWithMaximumEqualToMinimumPlusOneAlwaysReturnsMinimum();
        }
    }
}
