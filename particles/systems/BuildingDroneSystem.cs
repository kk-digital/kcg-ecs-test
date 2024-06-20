using Agent;
using GameManager;
using Godot;
using KMath;
using KMath.Random;
using PlanetTileMap;
using Constants = KMath.Constants;

namespace Particle;

public class BuildingDroneSystem
{
    public void InitStage1() { }

    public void InitStage2() { }
    
    public void Update()
    {
        ParticleList particleList = GameState.Planet.ParticleList;
        // PlanetTileMap.TileMap tileMap = GameState.Planet.TileMap;
        for (int i = 0; i < particleList.Length; i++)
        {
            ParticleEntity entity = particleList.Get(i);
            // If the entity is null, ignore
            if (entity == null)
                continue;
            
            if (!entity.hasParticleBuildingDrone)
                continue;

            // Handles all drone movement: move towards blueprint and movement pattern while building
            HandleDroneState(entity);
        }
    }
    
    private void HandleDroneState(ParticleEntity entity)
    {
        // Get player
        if(!GameState.Planet.CanGetPlayer())
            return;
        var player = GameState.Planet.GetPlayer();
        
        var physicsState = entity.particlePhysicsState;
        var drone = entity.particleBuildingDrone;
        var buildingComponent = player.agentBuilding;
        // KLog.LogDebug($"[Drone Debug] State: {drone.CurrentState}");
        var buildTask = drone.TargetBuildTask;

        switch (drone.CurrentState)
        {
            case BuildingDrone.DroneState.Spawning:
                //TODO: implement cool spawning movement animation, like ejecting with an ease movement
                drone.CurrentState = BuildingDrone.DroneState.FindingBuildTask;
                break;
            
            case BuildingDrone.DroneState.FindingBuildTask:
                // drone.TargetBlueprint = GetClosestBlueprint(physicsState.Position, playerBuildingComp);
                var nextBuildingTask = GetNextBuildingTask(physicsState.Position, buildingComponent, drone);
                if (nextBuildingTask == null)
                {
                    drone.CurrentState = BuildingDrone.DroneState.ReturningToOwner;
                    break;
                }

                drone.TargetBuildTask = nextBuildingTask;
                buildTask = nextBuildingTask;

                bool removedAny = RemoveFinishedTasks(buildTask, buildingComponent, drone);
                if (removedAny)
                {
                    drone.CurrentState = BuildingDrone.DroneState.FindingBuildTask;
                    break;
                }

                drone.CurrentState = BuildingDrone.DroneState.MovingTowardsBuildTask;
                break;
            case BuildingDrone.DroneState.MovingTowardsBuildTask:
                MoveTowardsTask(drone, buildingComponent, physicsState);
                break;
            
            case BuildingDrone.DroneState.FindingTile:
                // If no target tile to work on, get one randomly
                // Drone only works on one tile at a time
                var targetBlueprint = buildTask.TargetBlueprint;
                if (targetBlueprint.NonBuiltTilesCoordinates.Count >= 1)
                {
                    Vec2i offset = new Vec2i(targetBlueprint.PositionX, targetBlueprint.PositionY);
                    int randomTileIndex = GetRandomTileIndex(targetBlueprint);
                    Vec2i randomTile = targetBlueprint.NonBuiltTilesCoordinates[randomTileIndex];
                    drone.CurrentTileCoord = offset + randomTile;
                    
                    if(buildingComponent.MovementPattern == BuildingComponent.DroneMovementPattern.RandomPosition)
                        GetRandomPosition(drone, drone.CurrentTileCoord);
                    
                    drone.CurrentState = BuildingDrone.DroneState.BuildingTile;
                }
                else
                {
                    drone.TargetBuildTask.HasFinished = true;
                    GameState.BuildingManager.PopQueue(buildingComponent);
                    GameState.BuildingManager.RemoveBlueprintFromLists(buildingComponent, drone.TargetBuildTask.TargetBlueprint);
                    drone.TargetBuildTask = null;
                    drone.CurrentState = BuildingDrone.DroneState.FindingBuildTask;
                }
                break;

            case BuildingDrone.DroneState.BuildingTile:
                // TODO: do fixed movement if building single tiles instead of a complex blueprint
                BuildTile(entity, drone);
                MoveDroneWhileBuilding(entity, buildingComponent, GetBuildTaskCenter(drone));
                // OrbitMovementPattern(entity, playerBuildingComp, GetBuildTaskCenter(drone));
                break;
            
            case BuildingDrone.DroneState.BuildingMech:
                BuildMech(entity, drone, buildingComponent);
                MoveDroneWhileBuilding(entity, buildingComponent, GetBuildTaskCenter(drone));
                // OrbitMovementPattern(entity, playerBuildingComp, GetBuildTaskCenter(drone));
                break;
            
            case BuildingDrone.DroneState.ReturningToOwner:
                if (HasQueuedBuildTasks(buildingComponent))
                {
                    drone.CurrentState = BuildingDrone.DroneState.FindingBuildTask;
                    break;
                }
                
                ReturnDroneToPlayer(entity);
                break;
        }
    }

    private bool RemoveFinishedTasks(BuildingComponent.BuildTask task, BuildingComponent buildingComponent, BuildingDrone drone)
    {
        if (task.TargetMech != null)
            if (!task.TargetMech.mechCraftable.IsFullyBuilt)
                return false;

        if (task.TargetBlueprint != null)
            if (task.TargetBlueprint.NonBuiltTilesCoordinates.Count >= 1)
                return false;
        
        task.HasFinished = true;
        GameState.BuildingManager.PopQueue(buildingComponent);
        drone.TargetBuildTask = null;
        drone.NeedsNewRandomPosition = true;
        drone.CurrentState = BuildingDrone.DroneState.FindingBuildTask;
        return true;
    }

    private void MoveTowardsTask(BuildingDrone drone, BuildingComponent buildingComponent, PhysicsStateComponent physicsState)
    {
        // Note: if more drones are created while drones are working, this angle will not be readjusted until finished
        // potentially causing drone overlap
        // should be fine as long as all drones are sent at same time
        Vec2f targetPosition;
        Vec2f taskCenter = GetBuildTaskCenter(drone);
        switch (buildingComponent.MovementPattern)
        {
            default:
            case BuildingComponent.DroneMovementPattern.OrbitAround:
                drone.OrbitAngle = 360 / buildingComponent.DronesList.Length * drone.DroneNumber;

                float positionX = taskCenter.X + drone.MaximumDistanceToBuildTask *
                    Mathf.Sin(drone.OrbitAngle * Constants.Deg2Rad);
                float positionY = taskCenter.Y + drone.MaximumDistanceToBuildTask *
                    Mathf.Cos(drone.OrbitAngle * Constants.Deg2Rad);
                targetPosition = new Vec2f(positionX, positionY);
                break;
            
            case BuildingComponent.DroneMovementPattern.RandomPosition:
                targetPosition = GetRandomPosition(drone, taskCenter);
                break;
        }

        if (IsPositionOutsideRange(targetPosition, physicsState.Position, 0.5f))
        {
            var dir = targetPosition - physicsState.Position;
            physicsState.Velocity = dir.Normalized * drone.NormalSpeed;
            return;
        }

        switch (drone.TargetBuildTask.BuildTaskType)
        {
            case BuildTaskType.Blueprint:
                drone.CurrentState = BuildingDrone.DroneState.FindingTile;
                break;
            case BuildTaskType.Mech:
                drone.CurrentState = BuildingDrone.DroneState.BuildingMech;
                break;
        }
    }

    private static Vec2f GetBuildTaskCenter(BuildingDrone drone)
    {
        var buildTask = drone.TargetBuildTask;
        if (buildTask.BuildTaskType == BuildTaskType.Blueprint)
        {
            var targetBlueprint = buildTask.TargetBlueprint;
            Vec2f bpPos = new Vec2f(targetBlueprint.PositionX, targetBlueprint.PositionY);
            ref BlueprintDefinition bp = ref GameState.BlueprintCreationApi.Get((int)targetBlueprint.BlueprintType);
            bpPos += new Vec2f(bp.NumTilesX / 2, bp.NumTilesY / 2);
            return bpPos;
        }

        var mechCornerPos = buildTask.TargetMech.mechBase.Position;
        var mechPosition = mechCornerPos + buildTask.TargetMech.GetSize() / 2;

        return mechPosition;
    }

    private static void BuildTile(ParticleEntity entity, BuildingDrone drone)
    {
        drone.LaserStartPosition = entity.particlePhysicsState.Position;
        Vec2i tileCoord = drone.CurrentTileCoord;
        drone.LaserEndPosition = new Vec2f(tileCoord.X + 0.5f, tileCoord.Y + 0.5f);

        // Convert tile coordinate to coordinate in the blueprint
        Vec2i tilePos = new Vec2i(tileCoord.X, tileCoord.Y);

        ref var constructionTile = ref GameState.TileMapConstructionOverlay.GetTile(tilePos.X, tilePos.Y);
        // If tile is either built or about to be built, get a new target tile
        if (constructionTile.IsBuilt || constructionTile.IsFinishingBuild)
        {
            drone.NeedsNewRandomPosition = true;
            drone.CurrentState = BuildingDrone.DroneState.FindingTile;
            return;
        }

        var targetBlueprint = drone.TargetBuildTask.TargetBlueprint;
        GameState.BuildingManager.BuildBlueprintTile(ref constructionTile, targetBlueprint, tilePos, GameState.PhysicsDeltaTime);
        
        if (constructionTile.BuildingTimer <= 0 && !constructionTile.IsFinishingBuild)
        {
            // Try to get new tile to build if tile is built
            drone.CurrentState = BuildingDrone.DroneState.FindingTile;
            drone.NeedsNewRandomPosition = true;
        }
    }
    
    private static void BuildMech(ParticleEntity entity, BuildingDrone drone, BuildingComponent playerBuildingComp)
    {
        drone.LaserStartPosition = entity.particlePhysicsState.Position;
        var targetMech = drone.TargetBuildTask.TargetMech;
        Vec2f mechPosition = targetMech.mechBase.Position;
        Vec2i mechSize = targetMech.GetSize();
        drone.LaserEndPosition = new Vec2f(mechPosition.X, mechPosition.Y) + mechSize / 2;
        
        var mechCraftingComponent = targetMech.mechCraftable;
        
        if (mechCraftingComponent.IsFullyBuilt)
        {
            drone.NeedsNewRandomPosition = true;
            drone.CurrentState = BuildingDrone.DroneState.FindingBuildTask;
            return;
        }
        
        GameState.BuildingManager.BuildMech(targetMech, playerBuildingComp, GameState.PhysicsDeltaTime);
    }

    // Old code, without build task queue
    private ConstructionTask GetClosestBlueprint(Vec2f dronePos, BuildingComponent buildingComponent)
    {
        ConstructionTask closestBlueprint = null;
        float lowestDistance = Mathf.Inf;

        if (buildingComponent.BuildTasksQueue.Count == 0)
            return null;
        
        for (var ctI = 0; ctI < GameState.TileMapConstructionOverlay.ConstructionTaskList.Length; ctI++)
        {
            var constructionTask = GameState.TileMapConstructionOverlay.ConstructionTaskList[ctI];
            if (constructionTask == null)
            {
                continue;
            }

            if (closestBlueprint == null)
            {
                closestBlueprint = constructionTask;
                continue;
            }

            // TODO: fix sometimes doesn't get the closest blueprint
            Vec2f blueprintPosition = new Vec2f(constructionTask.PositionX, constructionTask.PositionY);
            float distance = Vec2f.Distance(dronePos, blueprintPosition);
            if (distance < lowestDistance)
            {
                closestBlueprint = constructionTask;
                lowestDistance = distance;
            }
        }

        return closestBlueprint;
    }

    private BuildingComponent.BuildTask? GetNextBuildingTask(Vec2f dronePos, BuildingComponent buildingComp, BuildingDrone drone)
    {
        BuildingComponent.BuildTask buildTask = null;

        if (drone.UsesBuildingQueue && HasQueuedBuildTasks(buildingComp))
        {
            buildTask = buildingComp.BuildTasksQueue[0];
        }
        else if (buildingComp.UnorderedBuildTasks.Count > 0)
        {
            buildTask = buildingComp.UnorderedBuildTasks.First();
        }

        return buildTask;
    }

    private bool HasQueuedBuildTasks(BuildingComponent buildingComponent)
    {
        return buildingComponent.BuildTasksQueue.Count > 0;
    }
    
    // Move drone towards player and makes it disappear when close enough
    private void ReturnDroneToPlayer(ParticleEntity entity)
    {
        // TODO: Make all drones return to player at the same time? to avoid returning some drones instead of all
        // Make their speed increase/decrease based on distance
        // Add a little delay before returning to player
        
        // Get player
        if(!GameState.Planet.CanGetPlayer())
            return;
        var player = GameState.Planet.GetPlayer();
        
        var drone = entity.particleBuildingDrone;
        var dronePhysicsState = entity.particlePhysicsState;
        drone.HasTargetPos = false;
        var playerPhysicsState = player.agentPhysicsState;

        Vec2f selfPosition = entity.particlePhysicsState.Position;
        Vec2f targetPosition = playerPhysicsState.FrameStateStart.Position;
        if (IsPositionOutsideRange(targetPosition, selfPosition, 0.5f))
        {
            var dir = targetPosition - dronePhysicsState.Position;
            dronePhysicsState.Velocity = dir.Normalized * drone.NormalSpeed;
            return;
        }

        // TODO: improve how drones expire/return to player, it's not good to be relying on a visual
        // TODO: in case drone fails to return to player, set particle health to 10~15 and decay rate to 1 s
        // if particle dies, remove from list? some way to tell that drones expired so player can send them again
        if (drone.Available)
        {
            drone.Available = false;
            entity.particleState.DecayRate = 100;
            
            // Make drone available again in player component
            player.agentBuilding.DronesList[drone.DroneNumber].IsAvailable = true;
        }
    }

    private static void MoveDroneWhileBuilding(ParticleEntity entity,  BuildingComponent buildingComponent, Vec2f buildTaskCenter)
    {
        switch (buildingComponent.MovementPattern)
        {
            default:
            case BuildingComponent.DroneMovementPattern.OrbitAround:
                OrbitMovementPattern(entity, buildTaskCenter);
                break;
            case BuildingComponent.DroneMovementPattern.RandomPosition:
                entity.particlePhysicsState.Position = entity.particleBuildingDrone.RandomTargetPosition;
                
                break;
        }
    }

    private static Vec2f GetRandomPosition(BuildingDrone drone, Vec2f buildTaskCenter)
    {
        if (!drone.NeedsNewRandomPosition)
            return drone.RandomTargetPosition;
        
        float maxX = buildTaskCenter.X + drone.MaximumDistanceToBuildTask * 0.7f;
        float minX = buildTaskCenter.X - drone.MaximumDistanceToBuildTask * 0.7f;
        float positionX = XorShift32.NextStaticFloat(minX, maxX);

        float maxY = buildTaskCenter.Y + drone.MaximumDistanceToBuildTask * 0.7f;
        float minY = buildTaskCenter.Y - drone.MaximumDistanceToBuildTask * 0.7f;
        float positionY = XorShift32.NextStaticFloat(minY, maxY);

        drone.RandomTargetPosition = new Vec2f(positionX, positionY);
        drone.NeedsNewRandomPosition = false;
        
        return drone.RandomTargetPosition;
    }

    // Makes drone orbit around blueprint while building, with an angle based on the drone number and quantity
    private static void OrbitMovementPattern(ParticleEntity entity, Vec2f orbitCenter)
    {
        PhysicsStateComponent physicsState = entity.particlePhysicsState;
        BuildingDrone drone = entity.particleBuildingDrone;

        //orbit around movement pattern
        //https://code.tutsplus.com/circular-motion-in-as3-make-one-moving-object-orbit-another--active-10833t
        drone.OrbitAngle += drone.SpeedWhileBuilding * GameState.PhysicsDeltaTime;
        //TODO: adjust orbit radius/distance based on blueprint size instead of fixed value
        float positionX = orbitCenter.X + drone.MaximumDistanceToBuildTask *
            Mathf.Sin(drone.OrbitAngle * Constants.Deg2Rad);
        float positionY = orbitCenter.Y + drone.MaximumDistanceToBuildTask *
            Mathf.Cos(drone.OrbitAngle * Constants.Deg2Rad);
        var targetPosition = new Vec2f(positionX, positionY);

        physicsState.Position = targetPosition;
    }

    private static bool IsPositionOutsideRange(Vec2f targetPosition, Vec2f selfPosition, float maxDistance)
    {
        return Vec2f.Distance(targetPosition, selfPosition) > maxDistance;
    }

    private static int GetRandomTileIndex(ConstructionTask closestBlueprintTask)
    {
        if (closestBlueprintTask.NonBuiltTilesCoordinates.Count == 1)
            return 0;
        
        return (int)XorShift32.NextStatic(0, (uint)(closestBlueprintTask.NonBuiltTilesCoordinates.Count));
    }

    private static bool IsAnyBlueprintsAvailable()
    {
        for (var ctI = 0; ctI < GameState.TileMapConstructionOverlay.ConstructionTaskList.Length; ctI++)
        {
            var constructionTask = GameState.TileMapConstructionOverlay.ConstructionTaskList[ctI];
            if (constructionTask != null)
                return true;
        }

        return false;
    }
}
