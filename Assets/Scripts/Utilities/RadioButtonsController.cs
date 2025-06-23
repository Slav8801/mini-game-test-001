using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
	public class RadioButtonsController : MonoBehaviour
	{
		[SerializeField]
		private Button[] toggleButtons;
		[SerializeField]
		private GameObject[] onVisuals;

		public event Action<int> OnIndexChanged;

		private int currentIndex;

		private void Start()
		{
			for (var index = 0; index < toggleButtons.Length; index++)
			{
				var index1 = index;
				toggleButtons[index].onClick.AddListener(() => HandleOnToggleButtonClicked(index1));
			}
		}

		public void SetCurrentIndex(int newIndex)
		{
			Toggle(newIndex);
		}

		public int GetCurrentIndex() => currentIndex;

		private void Toggle(int index)
		{
			currentIndex = index;

			OnIndexChanged?.Invoke(currentIndex);

			UpdateVisuals();
		}

		private void UpdateVisuals()
		{
			for (var index = 0; index < onVisuals.Length; index++)
			{
				onVisuals[index].SetActive(index == currentIndex);
			}
		}

		private void HandleOnToggleButtonClicked(int index)
		{
			Toggle(index);
		}

		private void OnDestroy()
		{
			OnIndexChanged = null;
		}
	}
}