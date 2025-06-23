using System;
using UnityEngine;
using UnityEngine.UI;

namespace States
{
	public class SettingsState : MonoBehaviour
	{
		[SerializeField]
		private Button backButton;

		public event Action OnBackButton;

		private void Start()
		{
			backButton.onClick.AddListener(HandleOnBackButtonClicked);
		}

		private void HandleOnBackButtonClicked()
		{
			OnBackButton?.Invoke();
		}
	}
}