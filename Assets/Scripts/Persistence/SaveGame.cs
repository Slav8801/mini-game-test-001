using System;

namespace Persistence
{
	[Serializable]
	public class SaveGame
	{
		public int DifficultyIndex = 0;
		public int TotalMatches = 0;
		public int BestScoreSoFar = 0;
	}
}