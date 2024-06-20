using GameManager;
using KMath;
using KMath.Random;
using Planet;

namespace Particle
{
    public class ParticleEmitterUpdateSystem
    {
        // Todo: Move these to gameState
        //Note(): they are just pointers
        ParticleEmitterPropertiesManager ParticleEmitterPropertiesManager;
        ParticlePropertiesManager ParticlePropertiesManager;

        public void InitStage1()
        {
        }

        public void InitStage2(ParticleEmitterPropertiesManager particleEmitterCreationApi, ParticlePropertiesManager particlePropertiesManager)
        {
            ParticleEmitterPropertiesManager = particleEmitterCreationApi;
            ParticlePropertiesManager = particlePropertiesManager;
        }

        public void Update(float deltaTime)
        {
            PlanetState planet = GameState.Planet;

            for (int i = 0; i < planet.ParticleEmitterList.Capacity; i++)
            {
                ParticleEmitterEntity particleEmitterEntity = planet.ParticleEmitterList.Get(i);
                
                // Some entries are null
                // Check if the entry is null
                if (particleEmitterEntity == null)
                {
                    // skip
                    continue;
                }

                // Make sure the entity contains
                // The particle emitter main component
                if (!particleEmitterEntity.hasParticleEmitterState)
                {
                    // skip
                    continue;
                }

                var state = particleEmitterEntity.particleEmitterState;
                var position = particleEmitterEntity.particleEmitter2dPosition;
                state.Duration -= deltaTime;

                
                // skip if its not on screen
                if (!state.IsOffScreen)
                {
                    continue;
                }

                // Get properties.
                ref ParticleEmitterProperties emitterProperties = ref ParticleEmitterPropertiesManager.GetRef((int)state.ParticleEmitterType);
                ref ParticleProperties particleProperties = ref ParticlePropertiesManager.GetRef((int)state.ParticleType);
                

                if (state.CurrentTime <= 0.0f)
                {
                    for (int j = 0; j < (int)(emitterProperties.ParticleCount * state.Intensity); j++)
                    {
                        float x = position.Position.X;
                        float y = position.Position.Y;
                        
                        float rand1 = Mt19937.genrand_realf();
                        float rand2 = Mt19937.genrand_realf();

                        Vec2f velocity = particleProperties.StartingVelocity;
                        
                        velocity.X += rand1 * (emitterProperties.VelocityIntervalEnd.X - 
                                               emitterProperties.VelocityIntervalBegin.X) +
                                      emitterProperties.VelocityIntervalBegin.X;
                        velocity.Y += rand2 * (emitterProperties.VelocityIntervalEnd.Y - 
                                               emitterProperties.VelocityIntervalBegin.Y) +
                                      emitterProperties.VelocityIntervalBegin.Y;
                        
                        if (state.EmissionSpread != 0)
                        {
                            float randomValue = (Mt19937.genrand_realf() - 0.5f);
                            Vec2f direction = Vec2f.Rotate(state.EmissionDirection, randomValue * state.EmissionSpread);
                            velocity = velocity.Magnitude * direction.Normalized;
                        }
                        
                        x += rand1 * emitterProperties.SpawnRadius * 2 - emitterProperties.SpawnRadius;
                        y += rand2 * emitterProperties.SpawnRadius * 2 - emitterProperties.SpawnRadius;

                        velocity *= state.Intensity;

                        GameState.ParticleSpawnerSystem.AddParticle(new Vec2f(x, y), velocity, state.ParticleType, 1, particleEmitterEntity.particleEmitterID.Index);
                    }

                    state.CurrentTime = emitterProperties.TimeBetweenEmissions;
                }
                else
                {
                    state.CurrentTime -= deltaTime;
                }
                
                // Remove the emitter at the end 
                // The emitter will spawn particles atleast once
                // even if the starting duration is 0 
                if (state.Duration < 0)
                {
                    planet.RemoveParticleEmitter(particleEmitterEntity.particleEmitterID.Index);
                }

            }
        }
    }
}