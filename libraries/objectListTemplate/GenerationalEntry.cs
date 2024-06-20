namespace GenerationalIndices
{
    public struct GenerationalEntry<T>
    {
        public T Value;
        public bool IsFree;
        public int Generation;
    }
}
