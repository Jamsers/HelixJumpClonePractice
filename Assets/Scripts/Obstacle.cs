public interface Obstacle {
    public enum Type {
        ChunkOut
    }

    public Type type { get; }
    public void Reset();
    public void Spawn();
}