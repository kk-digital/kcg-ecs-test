using KMath;
using Enums;
using GameManager;

namespace Particle
{
    public class ParticleEmitterSpawnerSystem
    {

        ParticleEmitterPropertiesManager ParticleEmitterPropertiesManager;
        ParticlePropertiesManager ParticlePropertiesManager;
        int uniqueID = 0;

        public void InitStage1()
        {

        }

        public void InitStage2(ParticleEmitterPropertiesManager particleEmitterCreationApi,
                                            ParticlePropertiesManager particlePropertiesManager)
        {
            ParticleEmitterPropertiesManager = particleEmitterCreationApi;
            ParticlePropertiesManager = particlePropertiesManager;
        }

        public ParticleEmitterEntity Spawn(ParticleEmitterType type, 
                                        Vec2f position, Vec2f velocity)
        {
            ParticleEmitterEntity entity = CreateParticleEmitterEntity(type, position, velocity);

            return entity;
        }

        private ParticleEmitterEntity CreateParticleEmitterEntity(ParticleEmitterType type, Vec2f position, Vec2f velocity)
        {
            ParticleEmitterProperties emitterProperties = 
                        ParticleEmitterPropertiesManager.Get((int)type);
            ParticleProperties particleProperties = 
                        ParticlePropertiesManager.Get(emitterProperties.ParticleType);
            
                var e = GameState.Planet.EntitasContext.particleEmitter.CreateEntity();
            e.AddParticleEmitterID(uniqueID++, -1);
            e.AddParticleEmitter2dPosition(new Vec2f(position.X, position.Y), Vec2f.Zero, velocity);
                e.AddParticleEmitterState(emitterProperties.ParticleType, type, 
                emitterProperties.EmissionSpread,  emitterProperties.VelocityFactor, emitterProperties.EmissionDirection, 
                emitterProperties.ParticleCount,
                emitterProperties.Duration, 0.0f, true);

            return e;
        }
    }
}
