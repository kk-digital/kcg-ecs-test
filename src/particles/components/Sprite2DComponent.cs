using Entitas;

namespace Particle
{
    [Particle]
    public class Sprite2DComponent : IComponent
    {
        public int     SpriteId;
        public byte[]  Vertices;
        public int     TextureLayer;
        public float   Width;
        public float   Height;
    }
}
