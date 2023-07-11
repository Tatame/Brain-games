using System;
using NeuroModules;


namespace NeuroboticsUnity.Models
{
    [Serializable]
    public class NeuroSense : Base, ISenseModel
    {
        #region Public Fields

        public float attention;
        public int concentration;
        public int meditation;

        public int mental_state;
        public float smr;

        #endregion

        #region Properties

        public float Attention => attention;
        public float Concentration => concentration;
        public float Meditation => meditation;
        public float MaxSense => 100f;

        public bool NoSignal => !result;

        #endregion

        #region Indexers

        public float this[string senseName]
        {
            get
            {
                switch (senseName)
                {
                    case "attention": return attention;
                    case "concentration": return (float)concentration;
                    case "meditation": return (float)meditation;
                    default: throw new Exception("Unknown sense name");
                }
            }
        }

        #endregion
    }
}