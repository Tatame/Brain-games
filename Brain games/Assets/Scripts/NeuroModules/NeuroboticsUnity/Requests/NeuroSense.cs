namespace NeuroboticsUnity.Requests
{
    public class NeuroSense : Base
    {
        public NeuroSense(bool showDataPackets) : base(showDataPackets)
        {
            Path = "/bci/";
        }
    }
}