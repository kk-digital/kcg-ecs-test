using Entitas;
using GenerationTests;
using Vehicle;

namespace Particle
{
    [Particle]
    public class BaseComponent : IComponent
    {
        public int                       Index;
        public VehicleEntity             vehicle;
        public SameNameClass             ClassTest;
        public Vehicle.Sprite2DComponent CompoentVariable;
    }
}