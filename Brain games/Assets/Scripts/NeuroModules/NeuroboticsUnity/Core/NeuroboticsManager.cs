#region Headers

using UnityEngine;
using NeuroModules;

#endregion


namespace NeuroboticsUnity.Core
{
    [AddComponentMenu("Scripts/NeuroboticsUnity/Neurobotics Manager")]
    [RequireComponent(typeof(NeuroboticsController))]
    public class NeuroboticsManager : MuffinTools.MonoSingleton<NeuroboticsManager>, IManager
    {
        #region Protected Methods

        protected override void OnInstanceInit()
        {
            base.OnInstanceInit();

            DontDestroyOnLoad(gameObject);

            Controller = GetComponent<NeuroboticsController>();
        }

        #endregion


        #region Accessors

        public IController Controller { get; private set; } = null;

        #endregion
    }
}