using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
	public class FlipButtonController : MonoBehaviour
	{
		[SerializeField]
		private Button flipButton;
		[SerializeField]
		private GameObject imagesRoot;
		[SerializeField]
		private Image upSide;
		[SerializeField]
		private Image downSide;
		[SerializeField]
		private int secondsToStayOpen = 2;

		public event Action OnFlip;

		private bool isHidden;
		private bool isShowingDownSide;
		private Coroutine flipSequenceCoroutine = null;
		private ImageIdPair imageIdPair = null;

		public void Setup(ImageIdPair imageIdPair)
		{
			this.imageIdPair = imageIdPair;
			downSide.sprite = imageIdPair.Sprite;
			flipButton.onClick.AddListener(HandleOnFlipButtonClicked);
			imagesRoot.SetActive(true);
			ShowUpSide();
		}

		public int GetId() => imageIdPair?.ID ?? 0;
		public bool GetIsHidden() => isHidden;
		public bool GetIsShowingDownSide() => isShowingDownSide;

		public void HideFlipTile()
		{
			if (flipSequenceCoroutine != null) StopCoroutine(flipSequenceCoroutine);

			isShowingDownSide = false;
			isHidden = true;
			imagesRoot.SetActive(false);

			flipButton.onClick.RemoveAllListeners();
			flipButton.interactable = false;
		}

		private void HandleOnFlipButtonClicked()
		{
			OnFlip?.Invoke();
			flipSequenceCoroutine = StartCoroutine(FlipSequence());
		}

		private void ShowUpSide()
		{
			upSide.gameObject.SetActive(true);
			downSide.gameObject.SetActive(false);
			flipSequenceCoroutine = null;
		}

		private void ShowDownSide()
		{
			upSide.gameObject.SetActive(false);
			downSide.gameObject.SetActive(true);
		}

		private IEnumerator FlipSequence()
		{
			flipButton.interactable = false;
			isShowingDownSide = true;
			ShowDownSide();
			yield return new WaitForSeconds(secondsToStayOpen);
			ShowUpSide();
			yield return null;
			isShowingDownSide = false;
			flipButton.interactable = true;
		}
	}
}