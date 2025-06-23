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

		public event Action OnContinueButton;

		private void Start()
		{
			IncreaseDifficulty();
			continueButton.onClick.AddListener(HandleOnContinueButtonClicked);
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