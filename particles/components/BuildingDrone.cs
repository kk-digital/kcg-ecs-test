using Agent;
using Entitas;
using KMath;

namespace Particle;

[Particle]
public class BuildingDrone : IComponent
{
    public int DroneNumber;                             // Id used to create movement pattern with multiple drones
    public bool Available;                              // If the drone exists and there's tiles to build, it's not available
    public Vec2i CurrentTileCoord;                      // Coordinates of the tile the drone is currently building
    public BuildingComponent.BuildTask TargetBuildTask; // Target Construction Task/Blueprint the drone is working on building tiles right now
    public float MaximumDistanceToBuildTask;            // If drone is more distant than this, will move towards blueprint

    public float NormalSpeed;                           // Speed that used for default movement
    public float SpeedWhileBuilding;                    // Speed used for movement while building, for movement patterns and etc

    public float OrbitAngle;                            // Used by the orbit movement pattern, current angle the drone is orbiting
    public bool HasTargetPos;                           // If the drone already has a target position to position itself for building
    public DroneState CurrentState;
    public Vec2f RandomTargetPosition;
    public bool NeedsNewRandomPosition;
    
    // Laser stuff
    public int LaserObjIndex;                           // Index of godot object for laser
    public Vec2f LaserStartPosition;                    // Starting position of the laser beam
    public Vec2f LaserEndPosition;                      // End position of the laser beam
    public bool UsesBuildingQueue;

    // Drone mapState machine
    public enum DroneState
    {
        Spawning,
        FindingBuildTask,
        MovingTowardsBuildTask,
        FindingTile,
        BuildingTile,
        ReturningToOwner,
        BuildingMech
    }

}
