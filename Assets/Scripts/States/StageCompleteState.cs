using Core;
using Persistence;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace States
{
	public class StageCompleteState : MonoBehaviour
	{
		[SerializeField]
		private Button continueButton;
		[SerializeField]
		private AudioSource gameOverAS;

		public event Action OnContinueButton;

		private void Start()
		{
			IncreaseDifficulty();
			continueButton.onClick.AddListener(HandleOnContinueButtonClicked);
			gameOverAS.Play();
		}

		private void HandleOnContinueButtonClicked()
		{
			OnContinueButton?.Invoke();
		}

		private void IncreaseDifficulty()
		{
			var currentDifficulty = PersistenceSystem.Instance.CurrentGame.DifficultyIndex;
			if (currentDifficulty >= Definitions.Difficulty.MAXIMUM_DIFFICULTY)
			{
				currentDifficulty = Definitions.Difficulty.MAXIMUM_DIFFICULTY;
			}
			else
			{
				currentDifficulty++;
			}
			PersistenceSystem.Instance.CurrentGame.DifficultyIndex = currentDifficulty;
		}
	}
}