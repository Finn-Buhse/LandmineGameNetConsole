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

        // Tests use value of 5, so an assertion will be triggered if the tests fail to properly set this variable
        int getRandomResult = 0;

        // GIVENS
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

        // WHENS
        public void whenGetNumberWithMaximumEqualToMinimumPlusOneCalled()
        {
            getRandomResult = randomGenerator1!.getRandom(5, 6);
        }

        // THENS

        public void thenTheTwoRandomGeneratorsHaveDifferentSeeds()
        {
            Assert.AreNotEqual(randomGenerator1!.seed, randomGenerator2!.seed);
        }
        public void thenGetNumberResultIs5()
        {
            Assert.AreEqual(getRandomResult, 5);
        }

        [TestMethod]
        public void RandomGeneratorInstancesHaveUniqueSeed()
        {
            givenHostContainerCreated();
            givenTwoRandomGeneratorsCreated();

            thenTheTwoRandomGeneratorsHaveDifferentSeeds();
        }

        [TestMethod]
        public void GetRandomWithMinimumAndMaximumReturnsWithinBounds()
        {
            givenHostContainerCreated();
            givenRandomGeneratorCreated();

            // Try this one hundred times
            // Note: If the bounds are off, the generator could return the correct number every time out of chance instead of being properly constrained.
            // This approach minimises the chances of that happening, however it's still questionable whether to include such a test
            for (int i = 0; i < 100; i++)
            {
                whenGetNumberWithMaximumEqualToMinimumPlusOneCalled();

                thenGetNumberResultIs5();
            }
        }
    }
}
