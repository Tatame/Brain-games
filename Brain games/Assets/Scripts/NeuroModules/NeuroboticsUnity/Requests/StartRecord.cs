using System;

namespace NeuroboticsUnity.Requests
{
    public class StartRecord: Base
    {
        private string Query;
        public StartRecord(bool showDataPackets, string filePath = null) : base(showDataPackets)
        {
            Path = "/StartRecord/";
            Query = $"path={filePath}";
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