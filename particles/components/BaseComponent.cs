using Entitas;

namespace Particle
{
    [Particle]
    public class BaseComponent : IComponent
    {
        public int Index;
        public ParticleType ParticleType;
    }
}