using System.Collections.Generic;
using System.Linq;

namespace GenerationalIndices
{
    public class ObjectList<T> where T: struct
    {
        private List<GenerationalEntry<T>> _entries = new List<GenerationalEntry<T>>();

        public GenerationalIndexKey Add(T val)
        {
            var freeSlotIndex = _entries.FindIndex(x => x.IsFree);
            if (freeSlotIndex >= 0)
            {
                var freeSlotEntry = _entries[freeSlotIndex];
                freeSlotEntry.Value = val;
                freeSlotEntry.IsFree = false;
                _entries[freeSlotIndex] = freeSlotEntry;

                return new GenerationalIndexKey { Index = freeSlotIndex, Generation = freeSlotEntry.Generation };
            }
            else
            {
                var newEntry = new GenerationalEntry<T> { Generation = 0, Value = val };
                _entries.Add(newEntry);

                return new GenerationalIndexKey { Index = _entries.Count - 1, Generation = newEntry.Generation };
            }
        }

        public T? Get(GenerationalIndexKey indexKey)
        {
            if (indexKey.Index < 0 || indexKey.Index >= _entries.Count) return null;

            var entry = _entries.ElementAtOrDefault(indexKey.Index);
            if (entry.Generation != indexKey.Generation) return null;

            return entry.Value;
        }

        public void Remove(GenerationalIndexKey indexKey)
        {
            if (indexKey.Index < 0 || indexKey.Index >= _entries.Count) return;

            var entry = _entries.ElementAtOrDefault(indexKey.Index);
            if (entry.Generation != indexKey.Generation) return; // Trying to remove an older generation

            entry.Generation++;
            entry.IsFree = true;
            _entries[indexKey.Index] = entry;
        }

    }
}
