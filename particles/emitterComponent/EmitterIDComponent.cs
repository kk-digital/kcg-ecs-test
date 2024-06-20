using Entitas;

namespace Particle
{
    [ParticleEmitter]
    public class EmitterIDComponent : IComponent
    {
        // This is not the index of ParticleEmitterList. It should never reuse values.
        public int ID;
        public int Index;
    }
}