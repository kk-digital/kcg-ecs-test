//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ParticleEntity {

    public Particle.StateComponent particleState { get { return (Particle.StateComponent)GetComponent(ParticleComponentsLookup.ParticleState); } }
    public bool hasParticleState { get { return HasComponent(ParticleComponentsLookup.ParticleState); } }

    public void AddParticleState(int newIndex, Particle.ParticleType newParticleType, int newParticleEmitterId, float newStartingHealth, float newHealth, float newDecayRate, float newSpriteRotationRate, KMath.Vec4f newColor, KMath.Vec2f newSize) {
        var index = ParticleComponentsLookup.ParticleState;
        var component = (Particle.StateComponent)CreateComponent(index, typeof(Particle.StateComponent));
        component.Index = newIndex;
        component.ParticleType = newParticleType;
        component.ParticleEmitterId = newParticleEmitterId;
        component.StartingHealth = newStartingHealth;
        component.Health = newHealth;
        component.DecayRate = newDecayRate;
        component.SpriteRotationRate = newSpriteRotationRate;
        component.Color = newColor;
        component.Size = newSize;
        AddComponent(index, component);
    }

    public void ReplaceParticleState(int newIndex, Particle.ParticleType newParticleType, int newParticleEmitterId, float newStartingHealth, float newHealth, float newDecayRate, float newSpriteRotationRate, KMath.Vec4f newColor, KMath.Vec2f newSize) {
        var index = ParticleComponentsLookup.ParticleState;
        var component = (Particle.StateComponent)CreateComponent(index, typeof(Particle.StateComponent));
        component.Index = newIndex;
        component.ParticleType = newParticleType;
        component.ParticleEmitterId = newParticleEmitterId;
        component.StartingHealth = newStartingHealth;
        component.Health = newHealth;
        component.DecayRate = newDecayRate;
        component.SpriteRotationRate = newSpriteRotationRate;
        component.Color = newColor;
        component.Size = newSize;
        ReplaceComponent(index, component);
    }

    public void RemoveParticleState() {
        RemoveComponent(ParticleComponentsLookup.ParticleState);
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

    static Entitas.IMatcher<ParticleEntity> _matcherParticleState;

    public static Entitas.IMatcher<ParticleEntity> ParticleState {
        get {
            if (_matcherParticleState == null) {
                var matcher = (Entitas.Matcher<ParticleEntity>)Entitas.Matcher<ParticleEntity>.AllOf(ParticleComponentsLookup.ParticleState);
                matcher.componentNames = ParticleComponentsLookup.componentNames;
                _matcherParticleState = matcher;
            }

            return _matcherParticleState;
        }
    }
}