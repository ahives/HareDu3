namespace HareDu
{
    using System;

    public interface ShovelConfiguration
    {
        void Configure(Action<ShovelTargetConfiguration> configuration);

        void Targeting(Action<ShovelTarget> target);
    }
}