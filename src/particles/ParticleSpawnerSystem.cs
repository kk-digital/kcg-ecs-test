
namespace Particle
{
    public class ParticleSpawnerSystem
    { 
        public static ParticleEntity Spawn(ParticleContext entitasContext)
        {
            ParticleEntity entity = entitasContext.CreateEntity();
            entity.AddParticleBase(-1);
            
            entity.AddParticleSprite2D(0, null, 0, 1, 1);
            entity.AddParticleAnimation(
                    newAnimationSpeed: 1.0f, 
                    newCurrentTime: 0f,
                    newCurrentFrame: 1,
                    newIsFinished: false,
                    newCurrentSpriteId: -1);

            entity.AddParticleBox2DCollider(0,0, 0, 1 , 1);

            return entity;
        }
    }
}
