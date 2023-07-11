using System;
using NeuroboticsUnity.Core;
using NeuroModules.Neurosky.Core;
using UnityEditor;
using UnityEngine;

namespace NeuroModules
{
    public enum Brands
    {
        Neurosky,
        Neurobotics
    }

    public class NeuroBrandsManager : MuffinTools.MonoSingleton<NeuroBrandsManager>
    {
        public Brands selected;
        public string deviceName;

        #region Protected Methods

        protected override void OnInstanceInit()
        {
            base.OnInstanceInit();

            DontDestroyOnLoad(gameObject);
            Debug.Log(selected);
            switch (selected)
             {
                 case Brands.Neurobotics:
                     gameObject.AddComponent<NeuroboticsManager>();
                     Controller = GetComponent<NeuroboticsController>();
                     break;
                 case Brands.Neurosky:
                     gameObject.AddComponent<MindwaveManager>();
                     Controller = GetComponent<MindwaveController>();
                     break;
                 default:
                     Debug.LogError("Unrecognized Option");
                     break;
             }

            Controller.deviceName = deviceName;
        }

        #endregion

        public IController Controller { get; private set; } = null;
    }
}
