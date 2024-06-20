using GameManager;
using KMath;
using KMath.Random;
using Planet;
using Sprites;
using Utility;

namespace Particle
{
    public class ParticleSpawnerSystem
    { 
        ParticlePropertiesManager _particlePropertiesManager;

        public void InitStage1()
        {

        }

        public void InitStage2(ParticlePropertiesManager particlePropertiesManager)
        {
            _particlePropertiesManager = particlePropertiesManager;
        }

        public ParticleEntity Spawn(ParticleType particleType, Vec2f position, Vec2f velocity, float health = 1.0f, int particleEmitterId = -1)
        {
            ParticleProperties particleProperties = 
                        _particlePropertiesManager.Get(particleType);

            var entity = GameState.Planet.EntitasContext.particle.CreateEntity();
            Vec2f size = particleProperties.MinSize + (particleProperties.MaxSize - particleProperties.MinSize)  *  Mt19937.genrand_realf();
            float random = Mt19937.genrand_realf();
            entity.AddParticleBase(-1, particleType);
            entity.AddParticleState(-1, particleType, particleEmitterId, health, health, particleProperties.MinDecayRate * random + particleProperties.MaxDecayRate * (1.0f - random), KMath.KMath.ToRadians(particleProperties.SpriteRotationRate), Vec4f.One, size);
            entity.AddParticlePhysicsState(new Vec2f(position.X, position.Y), new Vec2f(position.X, position.Y), new Vec2f(position.X, position.Y), particleProperties.Acceleration,
                             new Vec2f(velocity.X, velocity.Y), 0, false, particleProperties.Bounce, particleProperties.BounceFactor,
                             new Vec2f());
            
            entity.AddParticleSprite2D(particleProperties.SpriteId, null, null, -1, size);

            if (particleProperties.HasAnimation)
                entity.AddParticleAnimation(
                    newType: particleProperties.AnimationType,
                    newAnimationSpeed: 1.0f, 
                    newCurrentTime: 0f,
                    newCurrentFrame: 1,
                    newIsFinished: false,
                    newCurrentSpriteId: -1);

            if (particleProperties.IsCollidable) 
                entity.AddParticleBox2DCollider(size, new Vec2f());

            return entity;
        }

        public ParticleEntity SpawnDebrisParticle(Vec2f position, Vec2f[] triangles, Vec2f[] textureCoords , int layer, Vec2f velocity, 
            Vec2f offset, int particleEmitterId = -1)
        {
            ParticleProperties particleProperties = 
                _particlePropertiesManager.Get(ParticleType.Debris);

            var entity = GameState.Planet.EntitasContext.particle.CreateEntity();   
            Vec2f size = particleProperties.MinSize + (particleProperties.MaxSize - particleProperties.MinSize);
            float random = Mt19937.genrand_realf();
            entity.AddParticleState(-1, ParticleType.Debris, particleEmitterId, 1.0f, 1.0f, particleProperties.MinDecayRate * random + particleProperties.MaxDecayRate * (1.0f - random), KMath.KMath.ToRadians(particleProperties.SpriteRotationRate), Vec4f.One, size);
            entity.AddParticlePhysicsState(new Vec2f(position.X, position.Y), new Vec2f(position.X, position.Y),
                new Vec2f(position.X, position.Y), particleProperties.Acceleration,
                new Vec2f(velocity.X, velocity.Y), 0, false, particleProperties.Bounce, particleProperties.BounceFactor,
                new Vec2f());
            
            entity.AddParticleSprite2D(-1, triangles, textureCoords, layer, size);

            if (particleProperties.HasAnimation)
                entity.AddParticleAnimation(
                    newType: particleProperties.AnimationType,
                    newAnimationSpeed: 1.0f, 
                    newCurrentTime: 0f,
                    newCurrentFrame: 1,
                    newIsFinished: false,
                    newCurrentSpriteId: -1);

            if (particleProperties.IsCollidable)
                entity.AddParticleBox2DCollider(size, offset);

            return entity;
        }

        // Todo: we don't need texcoords and offset here, we only need a relative textureCoords position...
        public void SpawnSpriteDebris(Vec2f position, Vec2f offset, TexCoords spriteCoords, float spriteWidth, float spriteHeight)
        {
            float x = spriteCoords.Coords.X;
            float y = spriteCoords.Coords.Y;
            float width = spriteCoords.Coords.Z;
            float height = spriteCoords.Coords.W;
            

            float velocityValueX = 2.0f;
            float velocityValueY = 2.0f;

            // the box debris is composed of 
            // 5 parts, each part will have some vertices and 
            // texture coordinates

            // part 1
            Vec2f[] part1Vertices = new Vec2f[6];

            part1Vertices[0] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part1Vertices[1] = new Vec2f(0.0f * spriteWidth, 0.26f * spriteHeight);
            part1Vertices[2] = new Vec2f(0.73f * spriteWidth, 0.0f * spriteHeight);

            part1Vertices[3] = new Vec2f(0.73f * spriteWidth, 0.0f * spriteHeight);
            part1Vertices[4] = new Vec2f(0.0f * spriteWidth, 0.26f * spriteHeight);
            part1Vertices[5] = new Vec2f(0.66f * spriteWidth, 0.26f * spriteHeight);
            


            Vec2f[] part1Coords = new Vec2f[6];

            part1Coords[0] = new Vec2f(x + 0.0f * width, ((y + height) - 1.0f * height));
            part1Coords[1] = new Vec2f(x + 0.0f * width, ((y + height) - 0.74f * height));
            part1Coords[2] = new Vec2f(x + 0.73f * width, ((y + height) - 1.0f * height));
            part1Coords[3] = new Vec2f(x + 0.73f * width, ((y + height) - 1.0f * height));
            part1Coords[4] = new Vec2f(x + 0.0f * width, ((y + height) - 0.74f * height));
            part1Coords[5] = new Vec2f(x + 0.66f * width, ((y + height) - 0.74f * height));

            // part 2
            Vec2f[] part2Vertices = new Vec2f[12];

            part2Vertices[0] = new Vec2f(1.0f * spriteWidth, 1.0f * spriteHeight);
            part2Vertices[1] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight);
            part2Vertices[2] = new Vec2f(0.83f * spriteWidth, 0.66f * spriteHeight);

            part2Vertices[3] = new Vec2f(0.83f * spriteWidth, 0.66f * spriteHeight);
            part2Vertices[4] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight); 
            part2Vertices[5] = new Vec2f(0.83f * spriteWidth, 0.33f * spriteHeight);

            part2Vertices[6] = new Vec2f(0.83f * spriteWidth, 0.33f * spriteHeight);
            part2Vertices[7] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight);
            part2Vertices[8] = new Vec2f(0.66f * spriteWidth, 0.26f * spriteHeight);

            part2Vertices[9] = new Vec2f(0.66f * spriteWidth, 0.26f * spriteHeight);
            part2Vertices[10] = new Vec2f(1.0f * spriteWidth, 0.0f * spriteHeight);
            part2Vertices[11] = new Vec2f(0.73f * spriteWidth, 0.0f * spriteHeight);


            Vec2f[] part2Coords = new Vec2f[12];

            part2Coords[0] = new Vec2f(x + 1.0f * width, ((y + height) - 0.0f * height));
            part2Coords[1] = new Vec2f(x + 1.0f * width, ((y + height) - 1.0f * height));
            part2Coords[2] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));

            part2Coords[3] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));
            part2Coords[4] = new Vec2f(x + 1.0f * width, ((y + height) - 1.0f * height));
            part2Coords[5] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));

            part2Coords[6] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));
            part2Coords[7] = new Vec2f(x + 1.0f * width, ((y + height) - 1.0f * height));
            part2Coords[8] = new Vec2f(x + 0.66f * width, ((y + height) - 0.74f * height));

            part2Coords[9] = new Vec2f(x + 0.66f * width, ((y + height) - 0.74f * height));
            part2Coords[10] = new Vec2f(x + 1.0f * width, ((y + height) - 1.0f * height));
            part2Coords[11] = new Vec2f(x + 0.73f * width, ((y + height) - 1.0f * height));




            // part 3
            Vec2f[] part3Vertices = new Vec2f[9];

            part3Vertices[0] = new Vec2f(0.83f * spriteWidth, 0.07f * spriteHeight);
            part3Vertices[1] = new Vec2f(0.66f * spriteWidth, 0.0f * spriteHeight);    
            part3Vertices[2] = new Vec2f(0.83f * spriteWidth, 0.4f * spriteHeight);

            part3Vertices[3] = new Vec2f(0.83f * spriteWidth, 0.4f * spriteHeight);
            part3Vertices[4] = new Vec2f(0.66f * spriteWidth, 0.0f * spriteHeight);
            part3Vertices[5] = new Vec2f(0.5f * spriteWidth, 0.4f * spriteHeight);

            part3Vertices[6] = new Vec2f(0.5f * spriteWidth, 0.4f * spriteHeight);
            part3Vertices[7] = new Vec2f(0.66f * spriteWidth, 0.0f * spriteHeight);   
            part3Vertices[8] = new Vec2f(0.5f * spriteWidth, 0.0f * spriteHeight);


            Vec2f[] part3Coords = new Vec2f[9];

            part3Coords[0] = new Vec2f(x + 0.83f * width, ((y + height) - 0.66f * height));
            part3Coords[1] = new Vec2f(x + 0.66f * width, ((y + height) - 0.74f * height));
            part3Coords[2] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));

            part3Coords[3] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));
            part3Coords[4] = new Vec2f(x + 0.66f * width, ((y + height) - 0.74f * height)); 
            part3Coords[5] = new Vec2f(x + 0.5f * width, ((y + height) - 0.33f * height));

            part3Coords[6] = new Vec2f(x + 0.5f * width, ((y + height) - 0.33f * height));
            part3Coords[7] = new Vec2f(x + 0.66f * width, ((y + height) - 0.74f * height));   
            part3Coords[8] = new Vec2f(x + 0.5f * width, ((y + height) - 0.74f * height));



            // part 4
            Vec2f[] part4Vertices = new Vec2f[6];
            part4Vertices[0] = new Vec2f(0.5f * spriteWidth, 0.0f * spriteHeight);
            part4Vertices[1] = new Vec2f(0.0f * spriteWidth, 0.33f * spriteHeight);
            part4Vertices[2] = new Vec2f(0.83f * spriteWidth, 0.0f * spriteHeight);
            part4Vertices[3] = new Vec2f(0.83f * spriteWidth, 0.0f * spriteHeight);
            part4Vertices[4] = new Vec2f(0.0f * spriteWidth, 0.33f * spriteHeight);
            part4Vertices[5] = new Vec2f(1.0f * spriteWidth, 0.33f * spriteHeight);
            


            Vec2f[] part4Coords = new Vec2f[6];
            part4Coords[0] = new Vec2f(x + 0.5f * width, ((y + height) - 0.33f * height));
            part4Coords[1] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part4Coords[2] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));
            part4Coords[3] = new Vec2f(x + 0.83f * width, ((y + height) - 0.33f * height));
            part4Coords[4] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part4Coords[5] = new Vec2f(x + 1.0f * width, ((y + height) - 0.0f * height));
        


            // part 5
            Vec2f[] part5Vertices = new Vec2f[6];

            part5Vertices[0] = new Vec2f(0.0f * spriteWidth, 0.0f * spriteHeight);
            part5Vertices[1] = new Vec2f(0.0f * spriteWidth, 0.74f * spriteHeight);
            part5Vertices[2] = new Vec2f(0.5f * spriteWidth, 0.0f * spriteHeight);
            part5Vertices[3] = new Vec2f(0.5f * spriteWidth, 0.0f * spriteHeight);
            part5Vertices[4] = new Vec2f(0.0f * spriteWidth, 0.74f * spriteHeight);
            part5Vertices[5] = new Vec2f(0.5f * spriteWidth, 0.4f * spriteHeight);


            Vec2f[] part5Coords = new Vec2f[6];

            part5Coords[0] = new Vec2f(x + 0.0f * width, ((y + height) - 0.74f * height));
            part5Coords[1] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part5Coords[2] = new Vec2f(x + 0.5f * width, ((y + height) - 0.74f * height));
            part5Coords[3] = new Vec2f(x + 0.5f * width, ((y + height) - 0.74f * height));
            part5Coords[4] = new Vec2f(x + 0.0f * width, ((y + height) - 0.0f * height));
            part5Coords[5] = new Vec2f(x + 0.5f * width, ((y + height) - 0.33f * height));


            // random velocity for each part
            float rand1 = Mt19937.genrand_realf() * 1.5f - 0.5f;
            float rand2 = Mt19937.genrand_realf() * 1.5f - 0.5f;

            Vec2f velocity;
            velocity.X = rand1 * -1 * velocityValueX;
            velocity.Y = rand2 * -1 * velocityValueY;

            var planet = GameState.Planet;
            planet.ParticleList.Add(SpawnDebrisParticle(position, part1Vertices, part1Coords, spriteCoords.Layer, velocity, offset));
            
            rand1 = Mt19937.genrand_realf();
            rand2 = Mt19937.genrand_realf();

            velocity.X = rand1 * 1 * velocityValueX;
            velocity.Y = rand2 * 1 * velocityValueY * 3;

            planet.ParticleList.Add(SpawnDebrisParticle(position, part2Vertices, part2Coords, spriteCoords.Layer, velocity, offset));

            rand1 = Mt19937.genrand_realf();
            rand2 = Mt19937.genrand_realf();

            velocity.X = rand1 * velocityValueX;
            velocity.Y = rand2 * velocityValueY;

            planet.ParticleList.Add(SpawnDebrisParticle(position + new Vec2f(0.0f, 0.26f * spriteHeight), part3Vertices, 
                part3Coords, spriteCoords.Layer, velocity, offset));

            rand1 = Mt19937.genrand_realf();
            rand2 = Mt19937.genrand_realf();

            velocity.X = rand1 * velocityValueX;
            velocity.Y = rand2 * 1 * velocityValueY;

            planet.ParticleList.Add(SpawnDebrisParticle(position + new Vec2f(0.0f, 0.66f * spriteHeight), part4Vertices, 
                part4Coords, spriteCoords.Layer, velocity, offset));


            rand1 = Mt19937.genrand_realf();
            rand2 = Mt19937.genrand_realf();

            velocity.X = rand1 * -1 * velocityValueX;
            velocity.Y = rand2 * velocityValueY;

            planet.ParticleList.Add(SpawnDebrisParticle(position + new Vec2f(0.0f, 0.26f * spriteHeight), part5Vertices,
                part5Coords,  spriteCoords.Layer, velocity, offset));
        }
        
        public ParticleEntity AddParticle(Vec2f position, Vec2f velocity, ParticleType type, int health = 1, int particleEmitterId = -1)
        {
            PlanetState planet = GameState.Planet;
            Utils.Assert(planet.ParticleList.Capacity < PlanetEntityLimits.ParticleLimit);

            ParticleEntity newEntity = planet.ParticleList.Add(Spawn(type, position, velocity, health, particleEmitterId));
            return newEntity;
        }

        public void AddDebris(Vec2f position, Vec2f offset, TexCoords spriteCoords, float spriteWidth, float spriteHeight)
        {
            PlanetState planet = GameState.Planet;
            Utils.Assert(planet.ParticleList.Capacity + 5 < PlanetEntityLimits.ParticleLimit);

            var newPosition = new Vec2f((position.X + offset.X), (position.Y + offset.Y) * -0.36f);
            SpawnSpriteDebris(newPosition , offset, spriteCoords, spriteWidth, spriteHeight);
        }

    }
}
