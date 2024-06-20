using Entitas;

namespace Particle
{
    [Particle]
    public class AnimationComponent : IComponent
    {
        // Todo: Add all this info into a struct and allow other types of entities to have same data.
        public float AnimationSpeed;
        //public AnimationType Type; 
        public float CurrentTime;
        public int CurrentFrame;
        public bool IsFinished;
        public int CurrentSpriteId;
    }
}
