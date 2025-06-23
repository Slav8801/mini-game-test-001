using System;

namespace Persistence
{
	[Serializable]
	public class SaveGame
	{
		public int DifficultyIndex = 0;
		public int TopScore = 0;
		public int CurrentScore = 0;
	}
}