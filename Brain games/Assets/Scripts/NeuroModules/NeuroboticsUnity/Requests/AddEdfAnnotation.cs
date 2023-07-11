using System;

namespace NeuroboticsUnity.Requests
{
    public class AddEDFAnnotation : Base
    {
        private string Query;
        public AddEDFAnnotation(bool showDataPackets, string text) : base(showDataPackets)
        {
            Path = "/addEDFAnnotation/";
            Query = $"text={text}";
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