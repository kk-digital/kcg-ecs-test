using Collisions;
using Enums;
using GameManager;
using KMath;
using PlanetTileMap;
using Line2D = KMath.Line2D;

namespace Particle;

public class ParticleCollisionSystem
{

    public void InitStage1()
    {
        
    }

    public void InitStage2()
    {
        
    }
    
    // Maybe HandleCollision() instead of updateCollision
    public void Update()
    {
        ParticleList particleList = GameState.Planet.ParticleList;
        PlanetTileMap.TileMap tileMap = GameState.Planet.TileMap;
        for (int i = 0; i < particleList.Length; i++)
        {
            ParticleEntity entity = particleList.Get(i);

            // If the entity is null, ignore
            if (entity == null)
                continue;

            // Make sure the particle has a box2DCollider component
            // Not all particle can collide with terrain/objects
            if (!entity.hasParticleBox2DCollider)
                continue;

            var spriteComponent = entity.particleSprite2D;
            var physicsState = entity.particlePhysicsState;
            var box2DCollider = entity.particleBox2DCollider;

            Vec2f position = physicsState.PreviousPosition + box2DCollider.Offset - spriteComponent.Size * 0.5f;
            
            float minTime = 1.0f;
            Vec2f minNormal = new Vec2f(0, 1);
            int collidedWithArrayCount = 0;
            int mechArrayCount = 0;
            
            float minSize = MathF.Min(box2DCollider.Size.X, box2DCollider.Size.Y);

            Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
            var tilesInProximity = TileCollisions.GetTilesInProximity(position, new Vec2f(minSize, minSize), delta);

            MaterialType minMaterial = MaterialType.Error;

            for (int y = tilesInProximity.MinY; y <= tilesInProximity.MaxY; y++)
            {
                for (int x = tilesInProximity.MinX; x <= tilesInProximity.MaxX; x++)
                {
                    if (x >= 0 && x < tileMap.MapSize.X && y >= 0 && y < tileMap.MapSize.Y)
                    {
                        Tile tile = tileMap.GetTile(x, y);
                        var tileProperties = GameState.TilesetPropertiesManager.GetTileProperties(tile.FrontTileID);
                        TileGeometryAndRotation shape = tileProperties.BlockShapeType;
                        MaterialType material = tileProperties.MaterialType;

                        if (tile.Adjacency == TileGeometryAndRotationAndAdjacency.Error)
                        {
                            continue;
                        }

                        var adjacencyProperties =
                            GameState.AdjacencyPropertiesManager.GetProperties(tile.Adjacency);
                        for (int j = adjacencyProperties.Offset;
                             j < adjacencyProperties.Offset + adjacencyProperties.Size;
                             j++)
                        {
                            var lineEnum = GameState.AdjacencyPropertiesManager.GetLine(j);

                            Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);
                            GameState.LinePropertiesManager.GetNormal(lineEnum);

                            if (!(shape == TileGeometryAndRotation.QP_R0 ||
                                  shape == TileGeometryAndRotation.QP_R1 ||
                                  shape == TileGeometryAndRotation.QP_R2 || shape == TileGeometryAndRotation.QP_R3))
                            {
                                // circle line sweep test
                                var collisionResult =
                                    CircleLineSweepTest.TestCollision(
                                        position + minSize * 0.5f, minSize * 0.5f, delta, line.A, line.B);

                                if (collisionResult.Time < minTime)
                                {
                                    minTime = collisionResult.Time;
                                    minNormal = collisionResult.Normal;
                                    minMaterial = material;
                                }
                            }
                        }
                    }
                }
            }

            if (minTime < 1.0f)
            {
                if(!entity.hasParticleBase)
                    return;
                
                ref var properties = ref GameState.ParticlePropertiesManager.GetRef((int)entity.particleBase.ParticleType);
                // we collided with terrain
                if (properties.Bounce)
                {
                    Bounce(entity, minNormal);
                }
                else
                {
                    physicsState.Velocity = new Vec2f();
                    physicsState.Acceleration = new Vec2f();
                }
                
            }
        }

    }
    private void Bounce(ParticleEntity particleEntity, Vec2f normal)
    {
        if(particleEntity == null)
            return;

        var bounceVelocity = particleEntity.particlePhysicsState.Velocity.Bounce(normal);

        particleEntity.particlePhysicsState.Velocity = bounceVelocity * 0.4f;
    }
}