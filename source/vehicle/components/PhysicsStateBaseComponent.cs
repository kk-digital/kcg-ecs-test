using Entitas;
//using Enums;
//using Globals;
//using KMath;

namespace Vehicle
{
    [Vehicle]
    public class PhysicsStateComponent : IComponent
    {
        //public Vec2f SpawnPosition;

        public float AccelerationXAxis;
        public float AccelerationSpeedYAxis;
        
        //public Vec2f Position;
        //public Vec2f PreviousPosition;
        
        public float RotationZ;
        public float PreviousRotationZ;

        //public Vec2f Velocity;
        //public Vec2f Acceleration;  // Instantaneous, reset to zero at the end of the frame.

        public bool OnGrounded;

        //public VehicleMovementDirection MovingDirection;

        public bool IsAffectedByGravity;
        
        public bool IsMoving;
        public bool IsMovingPrevious; // Previous frame
        
        public bool IsCollided; // Used for determine if object collided ever for circle collision

        //public VehicleFrameInput FrameInput;
        
        //public List<Thruster> Thrusters;
    }
}

