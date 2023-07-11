using System;

namespace NeuroboticsUnity.Requests
{
    public class StopRecord : Base
    {
        public StopRecord(bool showDataPackets) : base(showDataPackets)
        {
            Path = "/StopRecord/";
        }
    }
}