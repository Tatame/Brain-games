using System;
using System.Threading.Tasks;
using NeuroModules.InfoModels;

namespace NeuroModules
{
    public interface IController
    {
        // Triggered when the connection to Mindwave is established.
        public event Action OnConnect;
        // Triggered when the connection to Mindwave is lost (or Disconnect() has been called).
        public event Action OnDisconnect;
        // Triggered when a connection try timeouts.
        public event Action OnConnectionTimeout;
        
        // Triggered when receiving data from Mindwave.
        public event Action<ISenseModel> OnUpdateData;

        public void Connect();

        public void StartRecord(string recordsPath = null);
        public Record StopRecord();

        public void AddAnnotation(string text);
        
        public bool IsConnecting { get; }
        public bool IsConnected { get; }

        public string deviceName { get; set; }

    }
}