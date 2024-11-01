using Particle;

namespace MyProject;

class Program
{
    public static void Main(string[] args)
    {
        var contexts = Contexts.sharedInstance;
        var e = contexts.game.CreateEntity();
        e.AddHealth(100);

        System.Console.WriteLine("e.health.value: " + e.health.Value);
        
        ParticleList particleList = new ParticleList();
        ParticleEntity particleEntity = ParticleSpawnerSystem.Spawn(contexts.particle);
        particleList.Add(particleEntity);
    }
}
