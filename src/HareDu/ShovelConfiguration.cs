namespace HareDu
{
    using System;

    public interface ShovelConfiguration
    {
        void Shovel(string name);

        void Configure(Action<ShovelTargetConfigurator> configurator);

        void Targeting(Action<ShovelTarget> target);
    }
}