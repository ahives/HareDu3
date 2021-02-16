namespace HareDu
{
    using System;

    public interface ShovelDeleteConfiguration
    {
        void Shovel(string name);
        
        void Targeting(Action<ShovelTarget> target);
    }
}