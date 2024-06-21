using Entitas;

namespace Vehicle
{
    [Vehicle]
    public class BaseComponent : IComponent
    {
        public Int64 Id;
        public float Durability;
        public bool EngineRunning; // If vehicle's thrusters running mapState
        public bool StartingProcedure; // This is for the upward movement after player gets in vehicle
        public bool EndingProcedure; // This is for the radial force when vehicle destroy
        public bool CanPilot; // State determines if player can pilot vehicle
        public bool IsDestroyed;

    }
}
