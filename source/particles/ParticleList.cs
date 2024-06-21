using Xunit;

namespace Particle
{
    // Don't use entitas internal lists because they don't have deterministic order.
    public class ParticleList
    {
        public ParticleEntity[] List;

        public int Length;

        // the capacity is just the Length of the list
        public int Capacity;

        public ParticleList()
        {
            List = new ParticleEntity[4096];
            Capacity = List.Length;
        }


        public ParticleEntity Add(ParticleEntity entity)
        {
            // if we don't have enough space we expand the capacity.
            ExpandArray();


            int LastIndex = Length;
            List[LastIndex] = entity;
            entity.particleState.Index = Length;
            Length++;

            return List[LastIndex];
        }


        public ParticleEntity Get(int Index)
        {
            return List[Index];
        }


        public void Remove(int particleIndex)
        {
            ParticleEntity entity = List[particleIndex];

            List[particleIndex] = List[Length - 1];
            List[particleIndex].particleState.Index = particleIndex;

            // Node(Joao): Destroy can't be above component access. If it's, there is going to be a bug when particleIndex == Length - 1
            entity.Destroy();

            List[Length - 1] = null;
            Length--;
        }

        // used to grow the list
        private void ExpandArray()
        {
            if (Length >= Capacity)
            {
                int NewCapacity = Capacity + 4096;

                // make sure the new capacity 
                // is bigget than the old one
                Assert.True(NewCapacity > Capacity);
                Capacity = NewCapacity;
                System.Array.Resize(ref List, Capacity);
            }
        }

    }
}