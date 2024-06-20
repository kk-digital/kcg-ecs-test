using GameManager;
using KMath;

namespace Particle
{
    public class ParticleUpdateSystem
    {
        public void Update(float deltaTime)
        {
            var planet = GameState.Planet;

            for (int i = 0; i < GameState.Planet.ParticleList.Length; i++)
            {
                ParticleEntity particleEntity = GameState.Planet.ParticleList.Get(i);

                var particleState = particleEntity.particleState;
                var particleSprite = particleEntity.particleSprite2D;
                ref ParticleProperties properties = ref GameState.ParticlePropertiesManager.GetRef((int)particleState.ParticleType);

                float healthNormalized = (particleState.Health / particleState.StartingHealth);

                float r = 1.0f, g = 1.0f, b = 1.0f, a = 1.0f;
                if (properties.ColorUpdateMethod == Enums.ParticleColorUpdateMethod.Linear)
                {
                    r = properties.StartingColor.R * healthNormalized + properties.EndColor.R * (1.0f - healthNormalized);
                    g = properties.StartingColor.G * healthNormalized + properties.EndColor.G * (1.0f - healthNormalized);
                    b = properties.StartingColor.B * healthNormalized + properties.EndColor.B * (1.0f - healthNormalized);
                    a = properties.StartingColor.A * healthNormalized + properties.EndColor.A * (1.0f - healthNormalized);
                }
                else if (properties.ColorUpdateMethod == Enums.ParticleColorUpdateMethod.ManyColorsLinear)
                {
                    // Linear blending pairs of colors
                    // When health changes we jump into the next colors
                    
                    // One colors 's worth of lifetime 
                    float healthPerColor = 1.0f / properties.ColorCount;
                    // Get the current color 
                    int currentColor = (int)((1.0f - healthNormalized) / healthPerColor);
                    // Get the next color
                    int nextColor = currentColor + 1;
                    // Make sure its not out of bounds
                    if (nextColor >= properties.ColorCount)
                    {
                        nextColor = properties.ColorCount - 1;
                    }
                    
                    // Linear blend (currentColor, nextColor)
                    r = properties.ColorArray[currentColor].R * healthNormalized + properties.ColorArray[nextColor].R * (1.0f - healthNormalized);
                    g = properties.ColorArray[currentColor].G * healthNormalized + properties.ColorArray[nextColor].G * (1.0f - healthNormalized);
                    b = properties.ColorArray[currentColor].B * healthNormalized + properties.ColorArray[nextColor].B * (1.0f - healthNormalized);
                    a = properties.ColorArray[currentColor].A * healthNormalized + properties.ColorArray[nextColor].A * (1.0f - healthNormalized);
                }
                
                particleState.Color = new Vec4f(r, g, b, a);

                Vec2f size = particleState.Size * (properties.StartingScale * healthNormalized + properties.EndScale * (1.0f - healthNormalized));
                particleSprite.Size = size;

                if (particleEntity.hasParticleBox2DCollider)
                {
                    var box2d = particleEntity.particleBox2DCollider;
                    box2d.Size = size;
                }

                float newHealth = particleState.Health - particleState.DecayRate * deltaTime;
                particleState.Health = newHealth;

                var physicsState = particleEntity.particlePhysicsState;
                Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
                Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

                Vec2f newPosition = physicsState.Position + displacement;

                // make sure the object is moving before rotating
                if ((newVelocity - physicsState.Velocity).Magnitude >= 0.01)
                {
                    float newRotation = physicsState.Rotation + particleState.SpriteRotationRate * deltaTime;
                    physicsState.Rotation = newRotation;
                }

                physicsState.Position = newPosition;
                physicsState.Velocity = newVelocity;

                int particleEmitterId = particleState.ParticleEmitterId;
                ParticleEmitterEntity particleEmitterEntity = planet.ParticleEmitterList.Get(particleEmitterId);
                if (particleEmitterEntity != null)
                {
                    var particleEmitterPosition = particleEmitterEntity.particleEmitter2dPosition;
                    physicsState.DrawPosition = physicsState.Position /*+ particleEmitterPosition.Position*/;   
                }
                else
                {
                    physicsState.DrawPosition = physicsState.Position;
                }

                if (newHealth <= 0)
                    planet.RemoveParticle(particleState.Index);
            }
        }
    }
}