using Enums;
using KMath;


namespace Particle
{
    public class ParticleEffectPropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex; // current index into the Offset Array

        private ParticleEffectProperties[] PropertiesArray; // an array of offsets into the LineSegment array 

        private int CurrentOffset;
        private ParticleEffectElement[] ElementArray;

        public void InitStage1()
        {
            PropertiesArray = new ParticleEffectProperties[256];
            ElementArray = new ParticleEffectElement[1024];


            CurrentIndex = 0;
            CurrentOffset = 0;
        }

        public void InitStage2()
        {
            InitializeResources();
        }

        public ParticleEffectProperties GetProperties(ParticleEffect id)
        {
            Utility.Utils.Assert((int)id >= 0 && (int)id < PropertiesArray.Length);

            return PropertiesArray[(int)id];
        }

        public ParticleEffectElement GetElement(int index)
        {
             Utility.Utils.Assert(index >= 0 && index < ElementArray.Length);

             return ElementArray[index];
        }

        public void Create(ParticleEffect id)
        {
            if ((int)id + 1 >= PropertiesArray.Length)
                Array.Resize(ref PropertiesArray, PropertiesArray.Length + 1024);

            CurrentIndex = (int)id;
            PropertiesArray[CurrentIndex] = new ParticleEffectProperties{Offset=CurrentOffset};
        }
        

        
        public void AddEmitter(ParticleEmitterType type, Vec2f elementOffset, float delay)
        {
            if ((int)type >= ElementArray.Length)
                Array.Resize(ref ElementArray, ElementArray.Length + 1024);

            PropertiesArray[CurrentIndex].Size++;
            ElementArray[CurrentOffset].Offset = elementOffset;
            ElementArray[CurrentOffset].Delay = delay;
            ElementArray[CurrentOffset++].Emitter = type;
        }

        // Todo(Refactoring): Refactor this to godot this should only be called from rendering being completely decoupled from the game logic.
        /*
        public void SpawnMuzzleFlash(Vec2f position, AgentMovementDirection direction)
        {
            if (direction == AgentMovementDirection.Right)
            {
                var Prefab = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(GameState.MuzzleFlash, new UnityEngine.Vector3(position.X, position.Y, 0.0f), UnityEngine.Quaternion.identity);
                var renderer = Prefab.GetComponent<UnityEngine.ParticleSystemRenderer>();
                renderer.flip = new UnityEngine.Vector3(0, renderer.flip.y, renderer.flip.z);
                Prefab.GetComponent<UnityEngine.ParticleSystem>().Play();
            }
            else if (direction == AgentMovementDirection.Left)
            {
                var Prefab = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(GameState.MuzzleFlash, new UnityEngine.Vector3(position.X, position.Y, 0.0f), UnityEngine.Quaternion.identity);
                var renderer = Prefab.GetComponent<UnityEngine.ParticleSystemRenderer>();
                renderer.flip = new UnityEngine.Vector3(-1, renderer.flip.y, renderer.flip.z);
                Prefab.GetComponent<UnityEngine.ParticleSystem>().Play();
            }
        }

        public void SpawnImpactEffect(Vec2f position)
        {
            var Prefab = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(GameState.ImpactEffect, new UnityEngine.Vector3 (position.X, position.Y, 0.0f), UnityEngine.Quaternion.identity);
            Prefab.GetComponent<UnityEngine.ParticleSystem>().Play();
            
        }
        */

        public void End()
        {

        }

        public void InitializeResources()
        {
            Create(ParticleEffect.Blood_Small);
            AddEmitter(ParticleEmitterType.BloodSmoke, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Blood2, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Blood_Medium);
            AddEmitter(ParticleEmitterType.Blood, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Blood2, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.BloodSmoke, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.BloodFog, Vec2f.Zero, 0.0f);
            End();
            
            Create(ParticleEffect.Blood_Hit);
            AddEmitter(ParticleEmitterType.BloodHit, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Blood2Hit, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Blood2, Vec2f.Zero, 0.0f);
          //  AddEmitter(ParticleEmitterType.BloodSmokeHit, Vec2f.Zero, 0.0f);
         //   AddEmitter(ParticleEmitterType.BloodFogHit, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Bleed_Small);
            AddEmitter(ParticleEmitterType.Bleed_Blood, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Bleed, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Bandage_Regen);
            AddEmitter(ParticleEmitterType.Bandage_Regen, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Heal_Small);
            AddEmitter(ParticleEmitterType.Heal_Small, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Pills);
            AddEmitter(ParticleEmitterType.Pills, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Bleed_Small_Tick);
            AddEmitter(ParticleEmitterType.Bleed_Tick, Vec2f.Zero, 0.0f);
            End();
            
            Create(ParticleEffect.PistolMagazine);
            AddEmitter(ParticleEmitterType.PistolMagazine, new Vec2f(0.1f, -0.1f), 0.0f);
            End();
            
            Create(ParticleEffect.BulletCasing);
            AddEmitter(ParticleEmitterType.BulletCasing, new Vec2f(0.1f, -0.1f), 0.0f);
            End();
            
            Create(ParticleEffect.ThrusterBrust);
            AddEmitter(ParticleEmitterType.ThrusterBrust, new Vec2f(0.0f, 0.0f), 0.0f);
            End();
            
            Create(ParticleEffect.ThrusterSmoke);
            AddEmitter(ParticleEmitterType.ThrusterSmoke, new Vec2f(0.0f, 0.0f), 0.0f);
            End();
            
            Create(ParticleEffect.VehicleExplosion);
            AddEmitter(ParticleEmitterType.Explosion_2_Flash, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Shrapnel, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Smoke, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Impact, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Part3, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Part2, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Part1, Vec2f.Zero, 0.0f);
            End();
            
            Create(ParticleEffect.VehicleSmoke);
            AddEmitter(ParticleEmitterType.VehicleSmoke, new Vec2f(0.0f, 0.0f), 0.0f);
            End();
            
            Create(ParticleEffect.VehiclePieces);
            AddEmitter(ParticleEmitterType.VehiclePieces, new Vec2f(0.0f, 0.0f), 0.0f);
            End();

            Create(ParticleEffect.Explosion_2);
            AddEmitter(ParticleEmitterType.Explosion_2_Flash, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Shrapnel, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Smoke, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Impact, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Part3, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Part2, Vec2f.Zero, 0.0f);
            AddEmitter(ParticleEmitterType.Explosion_2_Part1, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Smoke_2);
            AddEmitter(ParticleEmitterType.Smoke_2, Vec2f.Zero, 0.0f);
            End();
            
            Create(ParticleEffect.BuildingDrone);
            AddEmitter(ParticleEmitterType.BuildingDrone_Emitter, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Smoke_3);
            AddEmitter(ParticleEmitterType.Smoke_3, Vec2f.Zero, 0.0f);
            End();

            Create(ParticleEffect.Dust_Jumping);
            for(int i = 0; i < 8; i++)
            {
                AddEmitter(ParticleEmitterType.Dust_Jumping, new Vec2f(0.0f, 0.15f * i), 0.015f * i);
            }
            End();
        }


    }

}
