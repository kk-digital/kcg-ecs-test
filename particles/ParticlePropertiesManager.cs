using Enums;
using KMath;

namespace Particle
{
    public class ParticlePropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private ParticleProperties[] PropertiesArray;

        private Dictionary<string, int> NameToId;

        public void InitStage1()
        {
            NameToId = new Dictionary<string, int>();
            PropertiesArray = new ParticleProperties[1024];
            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new ParticleProperties();
            }
            CurrentIndex = -1;
        }

        public ParticleProperties Get(ParticleType id)
        {
            if (id >= 0 && (int)id < PropertiesArray.Length)
            {
                return PropertiesArray[(int)id];
            }

            return new ParticleProperties();
        }

        public ref ParticleProperties GetRef(int id)
        {
            return ref PropertiesArray[id];
        }

        public ParticleProperties Get(string name)
        {
            bool exists = NameToId.TryGetValue(name, out int value);
            if (exists)
            {
                return Get((ParticleType)value);
            }

            return new ParticleProperties();
        }

        public void Create(int id)
        {
            while (id >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = id;
            if (CurrentIndex != -1)
            {
                ref ParticleProperties currentParticle = ref PropertiesArray[CurrentIndex];
                currentParticle.PropertiesId = CurrentIndex;
                currentParticle.StartingScale = 1.0f;
                currentParticle.EndScale = 1.0f;

                currentParticle.ColorArray = new Vec4f[16];
                currentParticle.ColorCount = 0;
            }
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;

            if (!NameToId.ContainsKey(name))
            {
                NameToId.Add(name, CurrentIndex);
            }

            PropertiesArray[CurrentIndex].Name = name;
        }

        public void SetAnimationType(AnimationType animationType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].HasAnimation = true;
                PropertiesArray[CurrentIndex].AnimationType = animationType;
            }
        }
        public void SetDecayRate(float decayRate)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinDecayRate = decayRate;
                PropertiesArray[CurrentIndex].MaxDecayRate = decayRate;
            }
        }

        public void SetColorUpdateMethod(ParticleColorUpdateMethod colorUpdateMethod)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].ColorUpdateMethod = colorUpdateMethod;
            }
        }

        public void AddColor(Vec4f color)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                ref ParticleProperties currentParticle = ref PropertiesArray[CurrentIndex];
                currentParticle.ColorArray[currentParticle.ColorCount] = color;
                currentParticle.ColorCount++;
            }
        }

        public void SetDecayRate(float minDecayRate, float maxDecayRate)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinDecayRate = minDecayRate;
                PropertiesArray[CurrentIndex].MaxDecayRate = maxDecayRate;
            }
        }

        public void SetAcceleration(Vec2f acceleration)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Acceleration = acceleration;
            }
        }

        public void SetSpriteRotationRate(float rate)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteRotationRate = rate;
            }
        }

        public void SetSpriteId(int spriteId)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteId = spriteId;
            }
        }

        public void SetSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinSize = size;
                PropertiesArray[CurrentIndex].MaxSize = size;
            }
        }

        public void SetSize(Vec2f minSize, Vec2f maxSize)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].MinSize = minSize;
                PropertiesArray[CurrentIndex].MaxSize = maxSize;
            }
        }

        public void SetStartingVelocity(Vec2f startingVelocity)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingVelocity = startingVelocity;
            }
        }

        public void SetStartingRotation(float startingRotation)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingRotation = startingRotation;
            }
        }

        public void SetStartingScale(float startingScale)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingScale = startingScale;
            }
        }

        public void SetEndScale(float endScale)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].EndScale = endScale;
            }
        }

        public void SetStartingColor(Vec4f startingColor)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].StartingColor = startingColor;
            }
        }

        public void SetEndColor(Vec4f endColor)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].EndColor = endColor;
            }
        }

        public void SetAnimationSpeed(float animationSpeed)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].AnimationSpeed = animationSpeed;
            }
        }

        public void SetIsCollidable(bool isCollidable)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].IsCollidable = isCollidable;
            }
        }

        public void SetBounce(bool bounce)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Bounce = bounce;
            }
        }

        public void SetBounceFactor(Vec2f bounceFactor)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].BounceFactor = bounceFactor;
            }
        }

        public void End()
        {
            CurrentIndex = -1;
        }
        

        public void InitializeResources()
        {
            Create((int)ParticleType.Ore);
            SetDecayRate(1.0f);
            SetAcceleration(new Vec2f(0.0f, -20.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.5f, 0.5f));
            SetStartingVelocity(new Vec2f(1.0f, 10.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            End();

            Create((int)ParticleType.OreExplosionParticle);
            SetDecayRate(1.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.5f, 0.5f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            End();

            Create((int)ParticleType.DustParticle);
            SetDecayRate(4.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0);
            SetAnimationType(AnimationType.Dust);
            SetSize(new Vec2f(0.35f, 0.35f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            End();
            
            Create((int)ParticleType.Debris);
            SetDecayRate(0.5f);
            SetAcceleration(new Vec2f(0.0f, 10.5f));
            SetStartingVelocity(new Vec2f(0.0f, -0.0f));
            SetSpriteRotationRate(0.0f);
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.7f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.6f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.1f));
            SetIsCollidable(true);
            SetBounce(true);
            SetBounceFactor(new Vec2f(0.3f, 0.3f));
            End();

            Create((int)ParticleType.PistolMagazine);
            SetDecayRate(0.17f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, -0.3f));
            SetStartingScale(1.0f);
            SetEndScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            SetIsCollidable(true);
            End();
            
            Create((int)ParticleType.BulletCasing);
            SetDecayRate(0.17f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, -0.3f));
            SetStartingScale(1.0f);
            SetEndScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            SetIsCollidable(true);
            End();

            Create((int)ParticleType.ThrusterBrust);
            SetDecayRate(0.7f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, -0.3f));
            SetStartingScale(3.0f);
            SetEndScale(0.01f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            SetIsCollidable(true);
            End();
            
            Create((int)ParticleType.ThrusterSmoke);
            SetDecayRate(0.7f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, -0.3f));
            SetStartingScale(3.0f);
            SetEndScale(0.01f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            SetIsCollidable(true);
            End();
            
            Create((int)ParticleType.VehicleExplosion);
            SetDecayRate(0.7f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, -0.3f));
            SetStartingScale(6.0f);
            SetEndScale(0.01f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            SetIsCollidable(true);
            End();
            
            Create((int)ParticleType.VehicleSmoke);
            SetDecayRate(0.5f, 1.0f);
            SetAcceleration(new Vec2f(0.0f, 7.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(2.0f, 2.0f));
            SetStartingScale(0.0f);
            SetEndScale(5.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.6f));
            SetEndColor(new Vec4f(0.0f, 0.0f, 0.0f, 0.0f));
            End();
            
            Create((int)ParticleType.GasParticle);
            SetDecayRate(0.17f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0);
            SetAnimationType(AnimationType.Smoke);
            SetSize(new Vec2f(4.5f, 4.5f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(10.3f);
            SetStartingColor(new Vec4f(255f, 72f, 0f, 255.0f));
            End();

            Create((int)ParticleType.MuzzleFlash);
            SetDecayRate(6.0f);
            SetAcceleration(new Vec2f(0.0f, -10.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            SetStartingVelocity(new Vec2f(0.0f, 2.5f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.7f);
            SetStartingColor(new Vec4f(0.990f, 0.660f, 0.228f, 0.6f));
            SetEndColor(new Vec4f(0.740f, 0.448f, 0.0666f, 0.1f));
            SetIsCollidable(true);
            End();

            Create((int)ParticleType.Blood);
            SetDecayRate(1.5f);
            SetAcceleration(new Vec2f(0.0f, -10.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            SetStartingVelocity(new Vec2f(0.0f, 2.5f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.7f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.6f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.1f));
            SetIsCollidable(true);
            SetBounce(false);
            End();

            Create((int)ParticleType.Blood2);
            SetName("Blood");
            SetDecayRate(0.3f, 1.0f);
            SetAcceleration(new Vec2f(0.0f, -15.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.15f, 0.15f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.7f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.7f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.2f));
            SetIsCollidable(true);
            SetBounce(false);
            SetBounceFactor(new Vec2f(0.3f, 0.3f));
            End();
            
            
            Create((int)ParticleType.BloodSmoke);
            SetDecayRate(4.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.7f, 0.70f));
            SetStartingVelocity(new Vec2f(0.0f, 0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.5f);
            SetEndScale(0.5f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.3f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.1f));
            SetIsCollidable(true);
            End();

            Create((int)ParticleType.BloodFog);
            SetDecayRate(1.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.9f, 0.9f));
            SetStartingVelocity(new Vec2f(0.0f, 0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.5f);
            SetEndScale(0.5f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.1f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.1f));
            End();
            
            
            Create((int)ParticleType.BloodHit);
            SetDecayRate(8f);
            SetAcceleration(new Vec2f(0.0f, -0.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            SetStartingVelocity(new Vec2f(0.0f, 2.5f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.7f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.6f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.1f));
            SetIsCollidable(true);
            SetBounce(false);
            End();

            Create((int)ParticleType.Blood2Hit);
            SetName("Blood");
            SetDecayRate(10.0f);
            SetAcceleration(new Vec2f(0.0f, -0.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.15f, 0.15f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.7f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.7f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.2f));
            SetIsCollidable(true);
            SetBounce(false);
            SetBounceFactor(new Vec2f(0.3f, 0.3f));
            End();



            Create((int)ParticleType.Bleed_Blood);
            SetDecayRate(1.5f);
            SetAcceleration(new Vec2f(0.0f, -7.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.075f, 0.075f), new Vec2f(0.575f, 0.575f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.7f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.6f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.1f));
            SetIsCollidable(true);
            End();

            Create((int)ParticleType.Bleed);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, -5.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.15f, 0.15f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetEndScale(0.5f);
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.2f));
            SetIsCollidable(true);
            SetBounce(true);
            SetBounceFactor(new Vec2f(0.3f, 0.3f));
            End();

            Create((int)ParticleType.Regen);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, 5.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.5f, 0.5f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(0.5f);
            SetEndScale(1.0f);
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.5f));
            End();

            Create((int)ParticleType.Heal);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, 5.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.5f, 0.5f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(0.5f);
            SetEndScale(1.0f);
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.5f));
            End();

            Create((int)ParticleType.Pill);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, 5.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.33f, 0.66f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(0.5f);
            SetEndScale(1.0f);
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.5f));
            End();


            Create((int)ParticleType.Wood);
            SetDecayRate(0.5f);
            SetAcceleration(new Vec2f(0.0f, -10.0f));
            SetSpriteRotationRate(90.0f);
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingVelocity(new Vec2f(1.0f, 5.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            SetIsCollidable(true);
            End();

            Create((int)ParticleType.Explosion);
            SetDecayRate(2.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0);
            SetAnimationType(AnimationType.Explosion);
            SetSize(new Vec2f(3.0f, 3.0f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            End();
            
            Create((int)ParticleType.BuildingDrone);
            SetDecayRate(0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0);
            SetSize(new Vec2f(1.0f, 1.0f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            End();

            Create((int)ParticleType.Shrapnel);
            SetDecayRate(2.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.125f, 0.125f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetStartingColor(new Vec4f(255.0f, 255.0f, 255.0f, 255.0f));
            SetIsCollidable(true);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            End();

            Create((int)ParticleType.MetalMiningImpact);
            SetDecayRate(3.0f);
            SetAcceleration(new Vec2f(0.0f, -20.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            SetStartingColor(new Vec4f(0.4f, 0.4f, 0.4f, 1.0f));
            SetEndColor(new Vec4f(0.4f, 0.4f, 0.4f, 0.0f));
            End();

            Create((int)ParticleType.CopperMiningImpact);
            SetDecayRate(3.0f);
            SetAcceleration(new Vec2f(0.0f, -20.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            SetStartingColor(new Vec4f(0.72f, 0.45f, 0.2f, 1.0f));
            SetEndColor(new Vec4f(0.72f, 0.45f, 0.2f, 0.0f));
            End();

            Create((int)ParticleType.GoldMiningImpact);
            SetDecayRate(3.0f);
            SetAcceleration(new Vec2f(0.0f, -20.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            SetStartingColor(new Vec4f(0.83f, 0.68f, 0.2f, 1.0f));
            SetEndColor(new Vec4f(0.83f, 0.68f, 0.2f, 0.0f));
            End();

            

            Create((int)ParticleType.WaterParticlesImpact);
            SetDecayRate(6.0f);
            SetAcceleration(new Vec2f(0.0f, -10.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.125f, 0.125f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            SetStartingColor(new Vec4f(0.68f, 0.84f, 0.9f, 1.0f));
            SetEndColor(new Vec4f(0.68f, 0.84f, 0.9f, 0.0f));
            End();


            Create((int)ParticleType.MetalBulletImpact);
            SetDecayRate(6.0f);
            SetAcceleration(new Vec2f(0.0f, -10.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            SetStartingColor(new Vec4f(0.4f, 0.4f, 0.4f, 1.0f));
            SetEndColor(new Vec4f(0.8f, 0.8f, 0.8f, 0.0f));
            End();

            Create((int)ParticleType.RockBulletImpact);
            SetDecayRate(6.0f);
            SetAcceleration(new Vec2f(0.0f, -10.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            SetStartingColor(new Vec4f(1.0f, 0.64f, 0.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 0.64f, 0.0f, 0.0f));
            End();

            Create((int)ParticleType.BloodImpact);
            SetDecayRate(6.0f);
            SetAcceleration(new Vec2f(0.0f, -10.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingRotation(0.0f);
            SetStartingScale(1.0f);
            SetBounce(true);
            SetBounceFactor(new Vec2f(1.0f, 0.25f));
            SetStartingColor(new Vec4f(1.0f, 0.0f, 0.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 0.0f, 0.0f, 0.0f));
            End();


            Create((int)ParticleType.BulletTrail);
            SetDecayRate(5.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.075f, 0.075f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 0.83f, 0.43f, 0.2f));
            SetEndColor(new Vec4f(1.0f, 0.83f, 0.43f, 0.0f));
            End();

            Create((int)ParticleType.Explosion_2_Part1);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, 10.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.4f, 0.4f));
            SetStartingScale(1.0f);
            SetEndScale(0.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 0.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 0.0f, 1.0f));
            End();

            Create((int)ParticleType.Explosion_2_Part2);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, 7.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.3f, 0.3f));
            SetStartingScale(1.0f);
            SetEndScale(0.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 0.7f, 0.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 0.7f, 0.0f, 1.0f));
            End();

            Create((int)ParticleType.Explosion_2_Part3);
            SetDecayRate(2.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.25f, 0.25f));
            SetStartingScale(1.0f);
            SetEndScale(0.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(0.5f, 0.5f, 0.5f, 0.5f));
            End();

            Create((int)ParticleType.Explosion_2_Smoke);
            SetDecayRate(0.5f, 1.0f);
            SetAcceleration(new Vec2f(0.0f, 7.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(2.0f, 2.0f));
            SetStartingScale(0.0f);
            SetEndScale(1.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.6f));
            SetEndColor(new Vec4f(0.0f, 0.0f, 0.0f, 0.0f));
            End();

            Create((int)ParticleType.Explosion_2_Shrapnel);
            SetDecayRate(2.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(130.0f);
            SetSize(new Vec2f(0.125f, 0.125f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingScale(1.0f);
            SetEndScale(1.0f);
            SetStartingColor(new Vec4f(1.0f, 0.7f, 0.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 0.7f, 0.0f, 0.0f));
            End();


            Create((int)ParticleType.Explosion_2_Impact);
            SetDecayRate(5.2f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSize(new Vec2f(3.0f, 3.0f));
            SetStartingScale(0.0f);
            SetStartingScale(0.4f);
            SetEndScale(2.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.0f));
            End();
            

            Create((int)ParticleType.Explosion_2_Flash);
            SetDecayRate(6.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(1.6f, 1.6f));
            SetStartingScale(0.4f);
            SetEndScale(2.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.2f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.0f));
            End();


            Create((int)ParticleType.Dust_2);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, -1.0f));
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingScale(0.0f);
            SetEndScale(5.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1f, 1f, 1f, 0.35f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.0f));
            End();



            Create((int)ParticleType.Dust_3);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, -1.0f));
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingScale(0.0f);
            SetEndScale(5.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1f, 1f, 1f, 0.35f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.0f));
            End();


            Create((int)ParticleType.Dust_SwordAttack);
            SetDecayRate(1.0f, 2.0f);
            SetAcceleration(new Vec2f(0.0f, -1.0f));
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingScale(0.0f);
            SetEndScale(10.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(2f, 2f, 2f, 1.0f));
            SetEndColor(new Vec4f(2f, 2f, 2f, 0.0f));
            End();



            Create((int)ParticleType.Smoke_2);
            SetDecayRate(1.5f);
            SetAcceleration(new Vec2f(0.0f, 5.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingScale(1.0f);
            SetEndScale(8.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.5f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.0f));
            End();

            Create((int)ParticleType.Smoke_3);
            SetDecayRate(0.7f);
            SetAcceleration(new Vec2f(0.0f, 1.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingScale(1.0f);
            SetEndScale(8.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.2f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.0f));
            End();

            Create((int)ParticleType.SwordAttack_Impact);
            SetDecayRate(8.0f);
            SetAcceleration(new Vec2f(0.0f, -0.0f));
            SetSize(new Vec2f(3.0f, 3.0f));
            SetStartingScale(0.0f);
            SetEndScale(1.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            End();
            
            
            
            
            Create((int)ParticleType.Jetpack_Smoke);
            SetDecayRate(3f, 8.0f);
            SetAcceleration(new Vec2f(0.0f, 7.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.9f, 0.9f));
            SetStartingScale(0.5f);
            SetEndScale(1.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.6f));
            SetEndColor(new Vec4f(0.0f, 0.0f, 0.0f, 0.0f));
            End();

            Create((int)ParticleType.Jetpack_Particles);
            SetDecayRate(4.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.35f, 0.35f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingScale(1.0f);
            SetEndScale(0.1f);
            SetColorUpdateMethod(ParticleColorUpdateMethod.ManyColorsLinear);
            AddColor(new Vec4f(1.0f, 0.73f, 0.3f, 1.0f));
            AddColor(new Vec4f(1.0f, 0.65f, 0.0f, 1.0f));
            AddColor(new Vec4f(1.0f, 0.65f, 0.0f, 1.0f));
            AddColor(new Vec4f(0.47f, 0.13f, 0.13f, 1.0f));
            AddColor(new Vec4f(0.47f, 0.13f, 0.13f, 1.0f));
            AddColor(new Vec4f(0.0f, 0.0f, 0.0f, 1.0f));
            End();
            
            
            
            Create((int)ParticleType.Thruster_Smoke);
            SetDecayRate(3f, 8.0f);
            SetAcceleration(new Vec2f(0.0f, 7.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(1.6f, 1.6f));
            SetStartingScale(0.5f);
            SetEndScale(1.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.6f));
            SetEndColor(new Vec4f(0.0f, 0.0f, 0.0f, 0.0f));
            End();

            Create((int)ParticleType.Thruster_Particles);
            SetDecayRate(4.0f);
            SetAcceleration(new Vec2f(0.0f, -2.5f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.45f, 0.45f));
            SetStartingVelocity(new Vec2f(0.0f, -2.5f));
            SetStartingScale(1.0f);
            SetEndScale(0.1f);
            SetIsCollidable(true);
            SetBounce(true);
            SetColorUpdateMethod(ParticleColorUpdateMethod.ManyColorsLinear);
            AddColor(new Vec4f(1.0f, 0.73f, 0.3f, 1.0f));
            AddColor(new Vec4f(1.0f, 0.65f, 0.0f, 1.0f));
            AddColor(new Vec4f(1.0f, 0.65f, 0.0f, 1.0f));
            AddColor(new Vec4f(0.47f, 0.13f, 0.13f, 1.0f));
            AddColor(new Vec4f(0.47f, 0.13f, 0.13f, 1.0f));
            AddColor(new Vec4f(0.0f, 0.0f, 0.0f, 1.0f));
            End();
            
            
            Create((int)ParticleType.Rocket_Smoke);
            SetDecayRate(1.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.45f, 0.45f));
            SetStartingScale(0.5f);
            SetEndScale(1.0f);
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 0.6f));
            SetEndColor(new Vec4f(0.0f, 0.0f, 0.0f, 0.0f));
            End();

            Create((int)ParticleType.Rocket_Particles);
            SetDecayRate(8.0f);
            SetAcceleration(new Vec2f(0.0f, 0.0f));
            SetSpriteRotationRate(0.0f);
            SetSize(new Vec2f(0.35f, 0.35f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetStartingScale(1.0f);
            SetEndScale(0.1f);
            SetColorUpdateMethod(ParticleColorUpdateMethod.ManyColorsLinear);
            AddColor(new Vec4f(1.0f, 0.73f, 0.3f, 1.0f));
            AddColor(new Vec4f(1.0f, 0.65f, 0.0f, 1.0f));
            AddColor(new Vec4f(1.0f, 0.65f, 0.0f, 1.0f));
            AddColor(new Vec4f(0.47f, 0.13f, 0.13f, 1.0f));
            AddColor(new Vec4f(0.47f, 0.13f, 0.13f, 1.0f));
            AddColor(new Vec4f(0.0f, 0.0f, 0.0f, 1.0f));
            End();
            
            Create((int)ParticleType.ArmorPiece);
            SetDecayRate(0.2f);
            SetAcceleration(new Vec2f(0.0f, -9.0f));
            SetSpriteRotationRate(360.0f);
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetIsCollidable(true);
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            End();
            
            Create((int)ParticleType.VehiclePieces);
            SetDecayRate(0.8f);
            SetAcceleration(new Vec2f(0.0f, -9.0f));
            SetSpriteRotationRate(360.0f);
            SetSize(new Vec2f(0.1f, 0.1f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetIsCollidable(true);
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetStartingScale(1.0f);
            SetEndScale(3.0f);
            End();
            
            Create((int)ParticleType.HeadPiece);
            SetDecayRate(0.2f);
            SetAcceleration(new Vec2f(0.0f, -9.0f));
            SetSpriteRotationRate(360f);
            SetSize(new Vec2f(0.2f, 0.2f));
            SetStartingVelocity(new Vec2f(0.0f, 0.0f));
            SetIsCollidable(true);
            SetStartingColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            SetEndColor(new Vec4f(1.0f, 1.0f, 1.0f, 1.0f));
            End();
        }
    }

}
