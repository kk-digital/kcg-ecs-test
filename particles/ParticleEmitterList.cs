using Utility;

namespace Particle
{
    // Don't use entitas internal lists because they don't have deterministic order.
    public class ParticleEmitterList
    {
        
        // Array for storing entities
        public ParticleEmitterEntity[] List;

        // the capacity is just the Length of the list
        public int EntityCount;

        // Capacity is the lenth of the list.
        // We need to assign capacity instead of Capacity => List.Length.List.Length performs a cast to ICollection<T>
        // to retrieve the array's Length, which made getCapacity considerably expensive in the profiler.
        // For More information: https://stackoverflow.com/questions/16208951/array-count-much-slower-than-list-count
        public int Capacity;


        KMath.Random.XorShift32 fastRand;

        public ParticleEmitterList()
        {
            List = new ParticleEmitterEntity[1024];
            fastRand = new KMath.Random.XorShift32(1u);
            Capacity = List.Length;
        }

        public void Clear()
        {
            for (int i = 0; i < Capacity; i++)
            {
                List[i] = null;
            }
            EntityCount = 0;
        }


        public ParticleEmitterEntity Add(ParticleEmitterEntity entity)
        {
            // if 50% of the capacity is filled expand list.
            if (EntityCount > Capacity / 2)
                ExpandArray();

            int index = Math.Abs((int)fastRand.Next() % Capacity);

            for (int i = 0; i < Capacity; i++)
            {
                if (List[index] == null) // If position is free add entity to position.
                {
                    List[index] = entity;
                    entity.particleEmitterID.Index = index;
                    EntityCount++;
                    break;
                }

                index++;
                if (index == Capacity) // if index is equal last position of the list go to the beginning.
                    index = 0;
                continue;
            }
            return entity;
        }

        public ParticleEmitterEntity Get(int Index)
        {
            if (Index < 0 || Index >= List.Length)
            {
                return null;
            }
            
            return List[Index];
        }

        public void Remove(int index)
        {
            Utils.Assert(index >= 0 && index < Capacity);

            if (List[index] == null)
                return;

            List[index].Destroy();
            List[index] = null;
            EntityCount--;
        }

        // used to grow the list
        private void ExpandArray()
        {
            Capacity += 4096;
            Array.Resize(ref List, Capacity);
        }

    }
}