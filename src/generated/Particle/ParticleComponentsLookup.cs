//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class ParticleComponentsLookup {

    public const int ParticleAnimation = 0;
    public const int ParticleBase = 1;
    public const int ParticleBox2DCollider = 2;
    public const int ParticleSprite2D = 3;

    public const int TotalComponents = 4;

    public static readonly string[] componentNames = {
        "ParticleAnimation",
        "ParticleBase",
        "ParticleBox2DCollider",
        "ParticleSprite2D"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Particle.AnimationComponent),
        typeof(Particle.BaseComponent),
        typeof(Particle.Box2DColliderComponent),
        typeof(Particle.Sprite2DComponent)
    };
}