#region Headers

using System;
using System.Collections;
using System.IO;
using CatastropheAPI.Storage;
using NeuroModules;
using NeuroModules.InfoModels;
using UnityEngine;

#endregion


namespace NeuroboticsUnity.Core
{
    public class NeuroboticsController : MonoBehaviour, IController
    {
        #region Attributes

        // Triggered when the connection to Neurobotics is established.
        public event Action OnConnect;

        // Triggered when the connection to Neurobotics is lost.
        public event Action OnDisconnect;

        // Triggered when a connection try timeouts.
        public event Action OnConnectionTimeout;

        // Triggered when data are get from Neurobotics.
        public event Action<ISenseModel> OnUpdateData;

        // Settings
        [SerializeField, Range(0.0f, 1.0f), Tooltip("Defines the interval between each Mindwave call.")]
        private float updateStreamRate = 0.02f;

        [Header("Debug settings")] [SerializeField]
        private bool showDataPackets = false;
        
        // Flow
        private Coroutine _loadSensesRoutine = null;

        private bool _pendingConnection = false;
        private bool _connected = false;

        #endregion

        #region Properties

        public bool IsConnecting => _pendingConnection;
        public bool IsConnected => _connected;
        
        public string deviceName { get; set; }
        
        #endregion

        #region Fields For EEG records

        public string eegPath;

        public string eegName;

        public DateTime start;

        public DateTime end;

        #endregion

        #region Engine Methods

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        #endregion

        #region Public Methods

        public void Connect()
        {
            if (_loadSensesRoutine is null)
            {
                _pendingConnection = true;
                var request = new Requests.StartDevice(showDataPackets, deviceName);
                StartCoroutine(request.MakeRequest(data =>
                {
                    Models.Base res = JsonUtility.FromJson<Models.Base>(data);
                    if (!res.result) Debug.LogError(res.error);
                    else
                    {
                        _loadSensesRoutine = StartCoroutine(LoadSenses());
                        _connected = true;
                        OnConnect?.Invoke();
                    }
                }));
            }
        }
        public void StartRecord(string recordsPath)
        {
            recordsPath ??= Path.Combine(Application.dataPath, "Buckets", "EEGs");;
            eegName = DateTime.Now.ToString("yyyyMMddHHmmss");
            eegPath = Path.Combine(recordsPath, eegName);
            var request = new Requests.StartRecord(showDataPackets, eegPath);
            StartCoroutine(request.MakeRequest(data =>
            {
                Models.Base res = JsonUtility.FromJson<Models.Base>(data);
                if (!res.result) Debug.LogError(res.error);
            }));
            start = DateTime.Now;
            
        }

        public void AddAnnotation(string text)
        {
            var request = new Requests.AddEDFAnnotation(showDataPackets, text);
            StartCoroutine(request.MakeRequest(data =>
            {
                Models.Base res = JsonUtility.FromJson<Models.Base>(data);
                if (!res.result) Debug.LogError(res.error);
            }));
        }

        public Record StopRecord()
        {
            var request = new Requests.StopRecord(showDataPackets);

            StartCoroutine(request.MakeRequest(async data =>
            {
                Models.Base res = JsonUtility.FromJson<Models.Base>(data);
                if (!res.result) Debug.LogError(res.error);
                await EEGs.Upload($"{eegPath}.edf", $"{eegName}.edf");
            }));
            
            end = DateTime.Now;
            return new Record { Start = start, End = end, PathOnServer = $"{EEGs.BucketName}/{eegName}.edf"};
        }

        #endregion


        #region Private Methods

        private void OnDataReceived(string data)
        {
            Models.NeuroSense model = JsonUtility.FromJson<Models.NeuroSense>(data);
            if (model.result)
            {
                if (_pendingConnection) _pendingConnection = false;
                OnUpdateData?.Invoke(model);
            }
        }

        private IEnumerator LoadSenses()
        {
            var request = new Requests.NeuroSense(showDataPackets);

            _loadSensesRoutine = StartCoroutine(request.MakeRequest(OnDataReceived));

            yield return new WaitForSeconds(updateStreamRate);
            yield return LoadSenses();
        }

        #endregion
    }
}