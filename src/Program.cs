using System;
using Particle;
using Vehicle;
using Contexts = Vehicle.Contexts;

namespace MyProject;

class Program
{
    public static void Main(string[] args)
    {
        Contexts            contexts = Vehicle.Contexts.sharedInstance;
        VehicleEntity e        = contexts.vehicle.CreateEntity();
        e.AddVehicleBase(Random.Shared.Next(), 0.0f, true, true, true, true, true);

        System.Console.WriteLine("vehicleId " + e.vehicleBase.Id);
        
        ParticleList particleList = new ParticleList();
        ParticleEntity particleEntity = ParticleSpawnerSystem.Spawn(Particle.Contexts.sharedInstance.particle);
        particleList.Add(particleEntity);
    }
}
