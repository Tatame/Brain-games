namespace NeuroModules
{
    public interface ISenseModel : IBaseModel
    {
        float Attention { get; }
        float Concentration { get; }
        float Meditation { get; }
        float MaxSense { get; }
        public float this[string senseName] { get; }
    }
}