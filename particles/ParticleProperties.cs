using KMath;

namespace Particle
{

    public struct ParticleProperties
    {
        public int PropertiesId;
        public string Name;
        
        public float MinDecayRate;
        public float MaxDecayRate;
        public Vec2f Acceleration;
        public float SpriteRotationRate;

        // we can use a mix of sprites for the particles
        public int SpriteId;

        public bool HasAnimation;
        public AnimationType AnimationType;

        // the starting properties of the particles
        public Vec2f MinSize;
        public Vec2f MaxSize;
        public Vec2f StartingVelocity;
        public float StartingRotation;
        public float StartingScale;
        public float EndScale;
        public Vec4f StartingColor;
        public Vec4f EndColor;

        public Vec4f[] ColorArray;
        public int ColorCount;

        public Enums.ParticleColorUpdateMethod ColorUpdateMethod;
        public float AnimationSpeed;


        // Box Debris
        public bool IsCollidable;
        public bool Bounce;
        public Vec2f BounceFactor;

    }
}

