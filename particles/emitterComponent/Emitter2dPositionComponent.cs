using Entitas;
using KMath;

namespace Particle
{
    [ParticleEmitter]
    public class Emitter2dPositionComponent : IComponent
    {
        public Vec2f Position;
        public Vec2f Acceleration;
        public Vec2f Velocity;
    }
}