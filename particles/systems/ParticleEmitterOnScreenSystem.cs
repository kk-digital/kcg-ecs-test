using GameManager;

namespace Particle
{
    public class ParticleEmitterOnScreenSystem
    {
        // Todo: Move this variable out of the system.
        public int TicksCount; // Number of ticks since last update.
        
        public void Update()
        {
            const int TicksToUpdate = 5;

            if (TicksCount < TicksToUpdate)
            {
                TicksCount++;
                return;
            }

            // Reset ticksCount
            TicksCount = 0;

            for (int i = 0; i < GameState.Planet.ParticleEmitterList.Capacity; i++)
            {
                ParticleEmitterEntity entity = GameState.Planet.ParticleEmitterList.Get(i);
                if (entity == null)
                    continue;
                
                // Todo: We should pass game boundaries as an input to the game mapState.
                //if (!RendererLoop.CameraFrustum.IsOnScreen(entity.particleEmitter2dPosition.Position.X, entity.particleEmitter2dPosition.Position.Y))
                    entity.particleEmitterState.IsOffScreen = true;
                //else
                //    entity.particleEmitterState.IsOffScreen = false;
            }
        }
    }
}
