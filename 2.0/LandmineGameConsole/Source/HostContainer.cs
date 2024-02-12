using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using LandmineGameClasses;
using RandomClasses;

namespace ApplicationClasses
{
    public class HostContainer
    {
        public static IHost CreateHostContainer()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            
            builder.Services.AddTransient<ILandmineGame, LandmineGame>();
            builder.Services.AddTransient<IBoard, Board>();
            builder.Services.AddTransient<IPlayer, Player>();
            builder.Services.AddTransient<IRandomGenerator, RandomGenerator>();
            
            IHost host = builder.Build();
            return host;
        }
    }
}
