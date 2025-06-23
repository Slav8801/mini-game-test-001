using Persistence;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace States
{
	public class MainMenuState : MonoBehaviour
	{
		[SerializeField]
		private Button playButton;
		[SerializeField]
		private Button settingsButton;
		[SerializeField]
		private RadioButtonsController radioButtons;

		public event Action OnPlayButton;
		public event Action OnSettingsButton;

		private void Start()
		{
			CheckForHighScore();

			radioButtons.OnIndexChanged += HandleOnDifficultyChanged;
			var index = PersistenceSystem.Instance.CurrentGame.DifficultyIndex;
			radioButtons.SetCurrentIndex(index);

			playButton.onClick.AddListener(HandleOnPlayButtonClicked);
			settingsButton.onClick.AddListener(HandleOnSettingsButtonClicked);
		}

		private void HandleOnPlayButtonClicked()
		{
			OnPlayButton?.Invoke();
		}

		private void HandleOnSettingsButtonClicked()
		{
			OnSettingsButton?.Invoke();
		}

		private void HandleOnDifficultyChanged(int index)
		{
			var currentIndex = radioButtons.GetCurrentIndex();
			PersistenceSystem.Instance.CurrentGame.DifficultyIndex = currentIndex;
			PersistenceSystem.Instance.SaveGame();
		}

		private void CheckForHighScore()
		{
			var currentScore = PersistenceSystem.Instance.CurrentGame.CurrentScore;
			var currentHighScore = PersistenceSystem.Instance.CurrentGame.TopScore;
			if (currentHighScore < currentScore)
			{
				PersistenceSystem.Instance.CurrentGame.TopScore = currentScore;
			}
			PersistenceSystem.Instance.CurrentGame.CurrentScore = 0;
		}
	}
}