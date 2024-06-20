//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ParticleEntity {

    public Particle.BuildingDrone particleBuildingDrone { get { return (Particle.BuildingDrone)GetComponent(ParticleComponentsLookup.ParticleBuildingDrone); } }
    public bool hasParticleBuildingDrone { get { return HasComponent(ParticleComponentsLookup.ParticleBuildingDrone); } }

    public void AddParticleBuildingDrone(int newDroneNumber, bool newAvailable, KMath.Vec2i newCurrentTileCoord, Agent.BuildingComponent.BuildTask newTargetBuildTask, float newMaximumDistanceToBuildTask, float newNormalSpeed, float newSpeedWhileBuilding, float newOrbitAngle, bool newHasTargetPos, Particle.BuildingDrone.DroneState newCurrentState, KMath.Vec2f newRandomTargetPosition, bool newNeedsNewRandomPosition, int newLaserObjIndex, KMath.Vec2f newLaserStartPosition, KMath.Vec2f newLaserEndPosition, bool newUsesBuildingQueue) {
        var index = ParticleComponentsLookup.ParticleBuildingDrone;
        var component = (Particle.BuildingDrone)CreateComponent(index, typeof(Particle.BuildingDrone));
        component.DroneNumber = newDroneNumber;
        component.Available = newAvailable;
        component.CurrentTileCoord = newCurrentTileCoord;
        component.TargetBuildTask = newTargetBuildTask;
        component.MaximumDistanceToBuildTask = newMaximumDistanceToBuildTask;
        component.NormalSpeed = newNormalSpeed;
        component.SpeedWhileBuilding = newSpeedWhileBuilding;
        component.OrbitAngle = newOrbitAngle;
        component.HasTargetPos = newHasTargetPos;
        component.CurrentState = newCurrentState;
        component.RandomTargetPosition = newRandomTargetPosition;
        component.NeedsNewRandomPosition = newNeedsNewRandomPosition;
        component.LaserObjIndex = newLaserObjIndex;
        component.LaserStartPosition = newLaserStartPosition;
        component.LaserEndPosition = newLaserEndPosition;
        component.UsesBuildingQueue = newUsesBuildingQueue;
        AddComponent(index, component);
    }

    public void ReplaceParticleBuildingDrone(int newDroneNumber, bool newAvailable, KMath.Vec2i newCurrentTileCoord, Agent.BuildingComponent.BuildTask newTargetBuildTask, float newMaximumDistanceToBuildTask, float newNormalSpeed, float newSpeedWhileBuilding, float newOrbitAngle, bool newHasTargetPos, Particle.BuildingDrone.DroneState newCurrentState, KMath.Vec2f newRandomTargetPosition, bool newNeedsNewRandomPosition, int newLaserObjIndex, KMath.Vec2f newLaserStartPosition, KMath.Vec2f newLaserEndPosition, bool newUsesBuildingQueue) {
        var index = ParticleComponentsLookup.ParticleBuildingDrone;
        var component = (Particle.BuildingDrone)CreateComponent(index, typeof(Particle.BuildingDrone));
        component.DroneNumber = newDroneNumber;
        component.Available = newAvailable;
        component.CurrentTileCoord = newCurrentTileCoord;
        component.TargetBuildTask = newTargetBuildTask;
        component.MaximumDistanceToBuildTask = newMaximumDistanceToBuildTask;
        component.NormalSpeed = newNormalSpeed;
        component.SpeedWhileBuilding = newSpeedWhileBuilding;
        component.OrbitAngle = newOrbitAngle;
        component.HasTargetPos = newHasTargetPos;
        component.CurrentState = newCurrentState;
        component.RandomTargetPosition = newRandomTargetPosition;
        component.NeedsNewRandomPosition = newNeedsNewRandomPosition;
        component.LaserObjIndex = newLaserObjIndex;
        component.LaserStartPosition = newLaserStartPosition;
        component.LaserEndPosition = newLaserEndPosition;
        component.UsesBuildingQueue = newUsesBuildingQueue;
        ReplaceComponent(index, component);
    }

    public void RemoveParticleBuildingDrone() {
        RemoveComponent(ParticleComponentsLookup.ParticleBuildingDrone);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class ParticleMatcher {

    static Entitas.IMatcher<ParticleEntity> _matcherParticleBuildingDrone;

    public static Entitas.IMatcher<ParticleEntity> ParticleBuildingDrone {
        get {
            if (_matcherParticleBuildingDrone == null) {
                var matcher = (Entitas.Matcher<ParticleEntity>)Entitas.Matcher<ParticleEntity>.AllOf(ParticleComponentsLookup.ParticleBuildingDrone);
                matcher.componentNames = ParticleComponentsLookup.componentNames;
                _matcherParticleBuildingDrone = matcher;
            }

            return _matcherParticleBuildingDrone;
        }
    }
}