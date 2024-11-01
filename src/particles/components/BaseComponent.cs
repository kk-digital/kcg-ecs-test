using Entitas;
using Vehicle;

namespace Particle
{
    [Particle]
    public class BaseComponent : IComponent
    {
        public int Index;

        public VehicleEntity vehicle;
    }
}