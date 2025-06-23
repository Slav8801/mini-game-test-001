using Core;
using UnityEngine;

namespace Persistence
{
	public class PersistenceSystem
	{
		private static PersistenceSystem instance;
		public static PersistenceSystem Instance => instance ?? (instance = new PersistenceSystem());

		public SaveGame CurrentGame = null;

		public void CreateSaveGame()
		{
			LoadGame();
			if (CurrentGame == null)
			{
				CurrentGame = new SaveGame();
				SaveGame();
			}
		}

		public void SaveGame()
		{
			var savedGame = JsonUtility.ToJson(CurrentGame);
			PlayerPrefs.SetString(Definitions.SaveGame.DEFAULT, savedGame);
		}

		public void LoadGame()
		{
			var jsonString = PlayerPrefs.GetString(Definitions.SaveGame.DEFAULT);

			if (string.IsNullOrEmpty(jsonString)) return;

			var loadedGame = JsonUtility.FromJson<SaveGame>(jsonString);
			CurrentGame = loadedGame;
		}
	}
}