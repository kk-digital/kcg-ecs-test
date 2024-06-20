using Entitas;
using Enums;
using KMath;

namespace Particle
{
    [ParticleEmitter]
    public class EmitterStateComponent : IComponent
    {
        public ParticleType ParticleType;
        public ParticleEmitterType ParticleEmitterType;
        
        public float EmissionSpread; // Angle of the emission spread
        public float Intensity; // This value is multiplied by the final velocity, and the particle count
        public Vec2f EmissionDirection; // Direction of the spread

        public int ParticleCount;

        public float Duration;
        public float CurrentTime;

        public bool IsOffScreen;
    }
}