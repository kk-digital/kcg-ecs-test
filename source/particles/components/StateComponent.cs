using Entitas;
//using KMath;

namespace Particle
{
    [Particle]
    public class StateComponent : IComponent
    {
        public int Index;
        //public ParticleType ParticleType;

        public int ParticleEmitterId;

        public float StartingHealth;
        public float Health;
        public float DecayRate;

        public float SpriteRotationRate;

        //public Vec4f Color;
        //public  Vec2f Size;
    }
}