using System;

namespace NeuroboticsUnity.Requests
{
    public class StartDevice : Base
    {
        private string Query;
        public StartDevice(bool showDataPackets, string serialNumber) : base(showDataPackets)
        {
            Path = "/startDevice/";
            Query = $"sn={serialNumber}";
        }

        protected override Uri Url
        {
            get
            {
                var url = new UriBuilder(base.Url)
                {
                    Query = Query
                };
                return url.Uri;
            }
        }
    }
}