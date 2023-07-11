using System;

namespace NeuroModules.Neurosky.Data
{
	///<summary>
	///
	///		Represents datas extracted from Mindwave stream.
	///
	///</summary>
	[System.Serializable]
	public class MindwaveDataModel : ISenseModel
	{

		#region Attributes

		// Attention & meditation metrics
		public MindwaveDataESenseModel eSense;

		// Brain waves metrics
		public MindwaveDataEegPowerModel eegPower;

		// Mindwave connection status
		public int poorSignalLevel;
		public string status;

		#endregion

		#region Properties

		public float Attention => eSense.attention;
		public float Concentration => eSense.attention;
		public float Meditation => eSense.meditation;
		public float MaxSense => Core.MindwaveHelper.SENSE_MAX;

		#endregion

		#region Accessors

		/// <summary>
		/// Checks if this data is relative to a "no signal" value (poorSignalLevel too high).
		/// </summary>
		public bool NoSignal
		{
			get { return (poorSignalLevel >= Core.MindwaveHelper.NO_SIGNAL_LEVEL); }
		}
		
		public float this[string senseName]
		{
			get
			{
				switch (senseName)
				{
					case "attention": return Attention;
					case "concentration": return Attention;
					case "meditation": return Meditation;
					default: throw new Exception("Unknown sense name");
				}
			}
		}

		#endregion

	}
}
