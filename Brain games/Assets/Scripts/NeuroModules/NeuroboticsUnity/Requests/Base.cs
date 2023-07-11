using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace NeuroboticsUnity.Requests
{
    public class Base
    {
        private const string Scheme = "http";
        private const string Host = "127.0.0.1";
        private const int Port = 2336;
        protected string Path { get; set; } = "/";
        private readonly bool _showDataPackets = false;

        protected Base(bool showDataPackets)
        {
            _showDataPackets = showDataPackets;
        }

        protected virtual Uri Url
        {
            get
            {
                var builder = new UriBuilder(Scheme, Host, Port, Path);
                return builder.Uri;
            }
        }

        public IEnumerator MakeRequest(Action<string> onDataReceived)
        {
            var request = UnityWebRequest.Get(Url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogErrorFormat("error request [{0}, {1}]", Url, request.error);
            }
            else
            {
                var responseText = request.downloadHandler.text;
                if (_showDataPackets)
                {
                    Debug.Log(responseText);
                }

                onDataReceived(responseText);
            }
        }
    }
}