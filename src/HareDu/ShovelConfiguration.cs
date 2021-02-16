namespace HareDu
{
    using System;

    public interface ShovelConfiguration
    {
        void Shovel(string name);

        void Configure(Action<ShovelTargetConfiguration> configuration);

        void Targeting(Action<ShovelTarget> target);
    }
}