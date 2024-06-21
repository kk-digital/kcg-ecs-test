using Entitas;

namespace Vehicle
{
    [Vehicle]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;
        public int ThrusterSpriteId;
        public int Width;
        public int Height;
    }
}