namespace HareDu
{
    using System;

    public interface ShovelDeleteConfiguration
    {
        void Name(string name);
        
        void Targeting(Action<ShovelTarget> target);
    }
}