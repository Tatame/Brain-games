#region Headers

using UnityEngine;

#endregion

namespace NeuroModules.Neurosky.Core
{
    ///<summary>
    /// 
    ///		This component is just a shortcut for working with the Mindwave.
    ///		It's a Singleton, so you can access to it from anywhere, and it ensures that only one
    ///	instance is running during the app lifetime.
    ///	
    ///		From this component, you can access to the MindwaveController, and the MindwaveCalibrator.
    ///	The controller connects to the Mindwave and generate event when it send datas. The calibrator
    ///	can help you to work with brainwaves values by calculating ratios.
    ///
    ///</summary>
    [AddComponentMenu("Scripts/MindwaveUnity/Mindwave Manager")]
    [RequireComponent(typeof(MindwaveController))]
    public class MindwaveManager : MuffinTools.MonoSingleton<MindwaveManager>, IManager
    {
        #region Protected Methods

        protected override void OnInstanceInit()
        {
            base.OnInstanceInit();

            DontDestroyOnLoad(gameObject);

            Controller = GetComponent<MindwaveController>();
        }

        #endregion


        #region Accessors

        public IController Controller { get; private set; } = null;

        #endregion
    }
}