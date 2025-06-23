using UnityEngine;

namespace Core
{
	public static class Definitions
	{
		public static class SaveGame
		{
			public const string DEFAULT = "SaveGame";
		}

		public static class Score
		{
			public const int POINTS_PER_MATCH = 10;
		}

		public static class Difficulty
		{
			public const int MAXIMUM_DIFFICULTY = 4;

			public static readonly Vector2Int[] DIFFICULTY_MAP = new Vector2Int[]
			{
				new Vector2Int(2,2),
				new Vector2Int(2,3),
				new Vector2Int(3,4),
				new Vector2Int(4,4),
				new Vector2Int(5,4),
			};
		}

		public static class StateNames
		{
			public const string MAIN_STATE = "Main";
			public const string MAIN_MENU_STATE = "MainMenu";
			public const string GAMEPLAY_STATE = "Gameplay";
			public const string STAGE_COMPLETE_STATE = "StageComplete";
			public const string SETTINGS_STATE = "Settings";
		}
	}
}