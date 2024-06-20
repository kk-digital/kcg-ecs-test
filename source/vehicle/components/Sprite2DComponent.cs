using Entitas;
// KMath;
//using Sprites;

namespace Vehicle
{
    [Vehicle]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;
        public int ThrusterSpriteId;
        public int Width;
        public int Height;
        //public TexCoords Coords;

        //public Vec2f Size => new Vec2f(Width, Height);
    }
}