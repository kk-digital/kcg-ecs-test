using Enums;
using KMath;


namespace Particle
{
    public class ParticleEmitterPropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private ParticleEmitterProperties[] PropertiesArray;

        private Dictionary<string, int> NameToId;

        public void InitStage1()
        {
            NameToId = new Dictionary<string, int>();
            PropertiesArray = new ParticleEmitterProperties[1024];
            for(int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new ParticleEmitterProperties();
            }
            CurrentIndex = -1;
        }

        public void InitializeResources()
        {
            Create((int)ParticleEmitterType.OreFountain);
            SetParticleType(ParticleType.Ore);
            SetDuration(0.5f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(0.05f);
            SetVelocityInterval(new Vec2f(-1.0f, 0.0f), new Vec2f(1.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.OreExplosion);
            SetParticleType(ParticleType.OreExplosionParticle);
            SetDuration(0.15f);
            SetSpawnRadius(0.1f);
            SetParticleCount(15);
            SetTimeBetweenEmissions(1.0f);
            SetVelocityInterval(new Vec2f(-10.0f, -10.0f), new Vec2f(10.0f, 10.0f));
            End();

            Create((int)ParticleEmitterType.DustEmitter);
            SetParticleType(ParticleType.DustParticle);
            SetDuration(0.1f);
            SetSpawnRadius(0.1f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(1.02f);
            SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            End();

            Create((int)ParticleEmitterType.GasEmitter);
            SetParticleType(ParticleType.GasParticle);
            SetDuration(0.5f);
            SetSpawnRadius(0.25f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(1.02f);
            SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            End();

            Create((int)ParticleEmitterType.Blood);
            SetParticleType(ParticleType.Blood);
            SetDuration(2.0f);
            SetSpawnRadius(0.4f);
            SetParticleCount(10);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.6f, -0.5f), new Vec2f(0.6f, 0.5f));
            End();


            Create((int)ParticleEmitterType.Blood2);
            SetParticleType(ParticleType.Blood2);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(10);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-4.0f, -10.0f), new Vec2f(4.0f, 2.0f));
            End();
            
            Create((int)ParticleEmitterType.BloodSmoke);
            SetParticleType(ParticleType.BloodSmoke);
            SetDuration(2.0f);
            SetSpawnRadius(0.3f);
            SetParticleCount(6);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.BloodFog);
            SetParticleType(ParticleType.BloodFog);
            SetDuration(2.0f);
            SetSpawnRadius(0.7f);
            SetParticleCount(15);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.1f, -0.1f), new Vec2f(0.1f, 0.1f));
            End();
            
            Create((int)ParticleEmitterType.BloodHit);
            SetParticleType(ParticleType.BloodHit);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(45);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, 0.0f), new Vec2f(25.0f, 0.0f));
            End();
            
            Create((int)ParticleEmitterType.Blood2Hit);
            SetParticleType(ParticleType.Blood2Hit);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(45);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, 0.0f), new Vec2f(25.0f, 0.0f));
            End();
            
            Create((int)ParticleEmitterType.BloodSmokeHit);
            SetParticleType(ParticleType.BloodSmoke);
            SetDuration(2.0f);
            SetSpawnRadius(0.3f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetEmissionDirection(new Vec2f(-1, 0));
            SetVelocityInterval(new Vec2f(-0.0f, 0.0f), new Vec2f(3.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.BloodFogHit);
            SetParticleType(ParticleType.BloodFog);
            SetDuration(2.0f);
            SetSpawnRadius(0.3f);
            SetParticleCount(3);
            SetTimeBetweenEmissions(3.0f);
            SetEmissionDirection(new Vec2f(-1, 0));
            SetVelocityInterval(new Vec2f(-0.0f, 0.0f), new Vec2f(3.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.Bleed_Blood);
            SetParticleType(ParticleType.Bleed_Blood);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(3);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(0.0f, 0.0f), new Vec2f(10.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.Bleed);
            SetParticleType(ParticleType.Bleed);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(7);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.5f, -1.0f), new Vec2f(0.5f, 1.0f));
            End();

            Create((int)ParticleEmitterType.Bleed_Tick);
            SetParticleType(ParticleType.Blood2);
            SetDuration(1.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10f);
            SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.Bandage_Regen);
            SetParticleType(ParticleType.Regen);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(7);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.5f, -1.0f), new Vec2f(0.5f, 1.0f));
            End();

            Create((int)ParticleEmitterType.Heal_Small);
            SetParticleType(ParticleType.Heal);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(7);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.5f, -1.0f), new Vec2f(0.5f, 1.0f));
            End();

            Create((int)ParticleEmitterType.Pills);
            SetParticleType(ParticleType.Pill);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(7);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.5f, -1.0f), new Vec2f(0.5f, 1.0f));
            End();


            Create((int)ParticleEmitterType.WoodEmitter);
            SetParticleType(ParticleType.Wood);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(10);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-1.0f, -1.0f), new Vec2f(1.0f, 1.0f));
            End();

            Create((int)ParticleEmitterType.ExplosionEmitter);
            SetParticleType(ParticleType.Explosion);
            SetDuration(4.0f);
            SetSpawnRadius(0.7f);
            SetParticleCount(5);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(0.0f, 0.0f), new Vec2f(0.0f, 0.0f));
            End();
            
            Create((int)ParticleEmitterType.BuildingDrone_Emitter);
            SetParticleType(ParticleType.BuildingDrone);
            SetDuration(1.0f);
            SetSpawnRadius(0.7f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(1.5f);
            SetVelocityInterval(new Vec2f(0.0f, 0.0f), new Vec2f(0.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.ShrapnelEmitter);
            SetParticleType(ParticleType.Shrapnel);
            SetDuration(0.15f);
            SetSpawnRadius(0.1f);
            SetParticleCount(30);
            SetTimeBetweenEmissions(1.0f);
            SetVelocityInterval(new Vec2f(-5.0f, -5.0f), new Vec2f(5.0f, 5.0f));
            End();

            Create((int)ParticleEmitterType.MetalMiningImpact);
            SetParticleType(ParticleType.MetalMiningImpact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(12);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-4.0f, -4.0f), new Vec2f(4.0f, 4.0f));
            End();

            Create((int)ParticleEmitterType.CopperMiningImpact);
            SetParticleType(ParticleType.CopperMiningImpact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(12);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-4.0f, -4.0f), new Vec2f(4.0f, 4.0f));
            End();

            Create((int)ParticleEmitterType.GoldMiningImpact);
            SetParticleType(ParticleType.GoldMiningImpact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(12);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-4.0f, -4.0f), new Vec2f(4.0f, 4.0f));
            End();

            Create((int)ParticleEmitterType.WaterParticlesImpact);
            SetParticleType(ParticleType.WaterParticlesImpact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(12);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            End();

            Create((int)ParticleEmitterType.MetalBulletImpact);
            SetParticleType(ParticleType.MetalBulletImpact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(6);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            End();

            Create((int)ParticleEmitterType.RockBulletImpact);
            SetParticleType(ParticleType.RockBulletImpact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(6);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            End();

            Create((int)ParticleEmitterType.BloodImpact);
            SetParticleType(ParticleType.BloodImpact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(6);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -3.0f), new Vec2f(3.0f, 3.0f));
            End();

            Create((int)ParticleEmitterType.MuzzleFlash);
            SetParticleType(ParticleType.MuzzleFlash);
            SetDuration(2.0f);
            SetSpawnRadius(0.2f);
            SetParticleCount(10);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.3f, -0.3f), new Vec2f(0.3f, 0.3f));
            End();
            
            Create((int)ParticleEmitterType.PistolMagazine);
            SetParticleType(ParticleType.PistolMagazine);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -7.0f), new Vec2f(0.0f, -7.0f));
            End();
            
            Create((int)ParticleEmitterType.BulletCasing);
            SetParticleType(ParticleType.BulletCasing);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -7.0f), new Vec2f(3.0f, -7.0f));
            End();
            
            Create((int)ParticleEmitterType.ThrusterBrust);
            SetParticleType(ParticleType.ThrusterBrust);
            SetDuration(1.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -7.0f), new Vec2f(3.0f, -7.0f));
            End();
            
            Create((int)ParticleEmitterType.ThrusterSmoke);
            SetParticleType(ParticleType.ThrusterSmoke);
            SetDuration(1.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -7.0f), new Vec2f(3.0f, -7.0f));
            End();
            
            Create((int)ParticleEmitterType.VehicleExplosion);
            SetParticleType(ParticleType.VehicleExplosion);
            SetDuration(1.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-3.0f, -7.0f), new Vec2f(3.0f, -7.0f));
            End();

            Create((int)ParticleEmitterType.VehicleSmoke);
            SetParticleType(ParticleType.VehicleSmoke);
            SetDuration(2.0f);
            SetSpawnRadius(0.15f);
            SetParticleCount(7);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0f), new Vec2f(-0.0f, 0f));
            End();
            
            Create((int)ParticleEmitterType.VehiclePieces);
            SetParticleType(ParticleType.VehiclePieces);
            SetDuration(5.0f);
            SetSpawnRadius(1.15f);
            SetParticleCount(15);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0f), new Vec2f(-0.0f, 0f));
            End();
            
            Create((int)ParticleEmitterType.Explosion_2_Part1);
            SetParticleType(ParticleType.Explosion_2_Part1);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(15);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-2.5f, -2.5f), new Vec2f(2.5f, 2.5f));
            End();

            Create((int)ParticleEmitterType.Explosion_2_Part2);
            SetParticleType(ParticleType.Explosion_2_Part2);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(10);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            End();

            Create((int)ParticleEmitterType.Explosion_2_Part3);
            SetParticleType(ParticleType.Explosion_2_Part3);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(20);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            End();

            Create((int)ParticleEmitterType.Explosion_2_Shrapnel);
            SetParticleType(ParticleType.Explosion_2_Shrapnel);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(60);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-4.5f, -4.5f), new Vec2f(4.5f, 4.5f));
            End();

            Create((int)ParticleEmitterType.Explosion_2_Smoke);
            SetParticleType(ParticleType.Explosion_2_Smoke);
            SetDuration(2.0f);
            SetSpawnRadius(0.2f);
            SetParticleCount(20);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.75f, -0.75f), new Vec2f(0.75f, 0.75f));
            End();

            Create((int)ParticleEmitterType.Explosion_2_Flash);
            SetParticleType(ParticleType.Explosion_2_Flash);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(10);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            End();

            Create((int)ParticleEmitterType.Explosion_2_Impact);
            SetParticleType(ParticleType.Explosion_2_Impact);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-1.5f, -1.5f), new Vec2f(1.5f, 1.5f));
            End();


            Create((int)ParticleEmitterType.Dust_2);
            SetParticleType(ParticleType.Dust_2);
            SetDuration(2.0f);
            SetSpawnRadius(0.15f);
            SetParticleCount(60 * 2);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            End();


            Create((int)ParticleEmitterType.Dust_SwordAttack);
            SetParticleType(ParticleType.Dust_SwordAttack);
            SetDuration(2.0f);
            SetSpawnRadius(0.25f);
            SetParticleCount(60 * 2);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            End();

            Create((int)ParticleEmitterType.Dust_Jumping);
            SetParticleType(ParticleType.Dust_2);
            SetDuration(2.0f);
            SetSpawnRadius(0.15f);
            SetParticleCount(7 * 2);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            End();

            Create((int)ParticleEmitterType.Dust_Landing);
            SetParticleType(ParticleType.Dust_3);
            SetDuration(2.0f);
            SetSpawnRadius(0.1f);
            SetParticleCount(10 * 2);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0.0f), new Vec2f(0.0f, 0f));
            End();



            Create((int)ParticleEmitterType.Smoke_2);
            SetParticleType(ParticleType.Smoke_2);
            SetDuration(2.0f);
            SetSpawnRadius(0.15f);
            SetParticleCount(7);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(-0.0f, -0f), new Vec2f(-0.0f, 0f));
            End();


            Create((int)ParticleEmitterType.Smoke_3);
            SetParticleType(ParticleType.Smoke_3);
            SetDuration(2.0f);
            SetSpawnRadius(0.10f);
            SetParticleCount(7);
            SetTimeBetweenEmissions(0.01f);
            SetVelocityInterval(new Vec2f(-1.5f, -0f), new Vec2f(-1.0f, 0.3f));
            End();



            Create((int)ParticleEmitterType.SwordSlash_1_Right);
            SetParticleType(ParticleType.SwordSlash_1_Right);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            End();


            Create((int)ParticleEmitterType.SwordSlash_1_Left);
            SetParticleType(ParticleType.SwordSlash_1_Left);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            End();


            Create((int)ParticleEmitterType.SwordSlash_2_Right);
            SetParticleType(ParticleType.SwordSlash_2_Right);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            End();


            Create((int)ParticleEmitterType.SwordSlash_2_Left);
            SetParticleType(ParticleType.SwordSlash_2_Left);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.SwordSlash_3_Right);
            SetParticleType(ParticleType.SwordSlash_3_Right);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            End();

            Create((int)ParticleEmitterType.SwordSlash_3_Left);
            SetParticleType(ParticleType.SwordSlash_3_Left);
            SetDuration(2.0f);
            SetSpawnRadius(0.0f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            End();


            Create((int)ParticleEmitterType.SwordAttack_Impact);
            SetParticleType(ParticleType.SwordAttack_Impact);
            SetDuration(2.0f);
            SetSpawnRadius(0.3f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(10.0f);
            SetVelocityInterval(new Vec2f(-0f, -0f), new Vec2f(-0.0f, 0.0f));
            End();
            
            
            Create((int)ParticleEmitterType.Jetpack_Particles_Emitter);
            SetParticleType(ParticleType.Jetpack_Particles);
            SetDuration(0.1f);
            SetSpawnRadius(0.2f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(1.02f);
            SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            End();
            
            Create((int)ParticleEmitterType.Jetpack_Smoke_Emitter);
            SetParticleType(ParticleType.Jetpack_Smoke);
            SetDuration(0.1f);
            SetSpawnRadius(0.2f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(0, 0), new Vec2f(0.0f, 0.0f));
            End();
            
            
            Create((int)ParticleEmitterType.Thruster_Particles_Emitter);
            SetParticleType(ParticleType.Thruster_Particles);
            SetDuration(0.1f);
            SetSpawnRadius(0.2f);
            SetParticleCount(10);
            SetSpread(0.01f);
            SetTimeBetweenEmissions(1.02f);
            SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
            End();
            
            Create((int)ParticleEmitterType.Thruster_Smoke_Emitter);
            SetParticleType(ParticleType.Thruster_Smoke);
            SetDuration(0.1f);
            SetSpawnRadius(0.2f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(0, 0), new Vec2f(0.0f, 0.0f));
            End();
            
            Create((int)ParticleEmitterType.Rocket_Particles_Emitter);
            SetParticleType(ParticleType.Rocket_Particles);
            SetDuration(0.1f);
            SetSpawnRadius(0.01f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(1.02f);
            SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(3.0f, 0));
            End();
            
            Create((int)ParticleEmitterType.Rocket_Smoke_Emitter);
            SetParticleType(ParticleType.Rocket_Smoke);
            SetDuration(0.1f);
            SetSpawnRadius(0.05f);
            SetParticleCount(1);
            SetTimeBetweenEmissions(3.0f);
            SetVelocityInterval(new Vec2f(0, 0), new Vec2f(0.0f, 0.0f));
            End();
            
        }

        public ParticleEmitterProperties Get(int id)
        {
            if (id >= 0 && id < PropertiesArray.Length) return PropertiesArray[id];

            return new ParticleEmitterProperties();
        }

        public ref ParticleEmitterProperties GetRef(int id)
        {      
            return ref PropertiesArray[id];
        }

        public ParticleEmitterProperties Get(string name)
        {
            int value;
            bool exists = NameToId.TryGetValue(name, out value);
            if (exists) return Get(value);

            return new ParticleEmitterProperties();
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
                PropertiesArray[CurrentIndex].PropertiesId = CurrentIndex;
                PropertiesArray[CurrentIndex].VelocityFactor = 1.0f;
                PropertiesArray[CurrentIndex].EmissionDirection = new Vec2f(1.0f, 0.0f);
            }
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;
            
            if (!NameToId.ContainsKey(name)) NameToId.Add(name, CurrentIndex);

            PropertiesArray[CurrentIndex].Name = name;
        }

        public void SetParticleType(ParticleType particleType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length) 
                PropertiesArray[CurrentIndex].ParticleType = particleType;
        }
        
        public void SetEmissionDirection(Vec2f emissionDirection)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length) 
                PropertiesArray[CurrentIndex].EmissionDirection = emissionDirection;
        }

        public void SetVelocityInterval(Vec2f begin, Vec2f end)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].VelocityIntervalBegin = begin;
                PropertiesArray[CurrentIndex].VelocityIntervalEnd = end;
            }
        }

        public void SetSpawnRadius(float spawnRadius)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
                PropertiesArray[CurrentIndex].SpawnRadius = spawnRadius;
        }

        public void SetDuration(float duration)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length) 
                PropertiesArray[CurrentIndex].Duration = duration;
        }

        public void SetLoop(bool loop)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length) 
                PropertiesArray[CurrentIndex].Loop = loop;
        }

        public void SetParticleCount(int particleCount)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length) 
                PropertiesArray[CurrentIndex].ParticleCount = particleCount;
        }

        public void SetTimeBetweenEmissions(float timeBetweenEmissions)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length) 
                PropertiesArray[CurrentIndex].TimeBetweenEmissions = timeBetweenEmissions;
        }

        public void SetSpread(float spread)
        {
            
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length) 
                PropertiesArray[CurrentIndex].EmissionSpread = spread;
        }

        public void End()
        {
            CurrentIndex = -1;
        }
    }

}
