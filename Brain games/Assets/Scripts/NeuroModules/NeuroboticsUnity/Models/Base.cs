using System;
using JetBrains.Annotations;

namespace NeuroboticsUnity.Models
{
    [Serializable]
    public class Base
    {
        #region Public Fields

        public bool result;
        
        [CanBeNull] public string command;
        [CanBeNull] public string error;

        [CanBeNull] public string time;

        #endregion
    }
}