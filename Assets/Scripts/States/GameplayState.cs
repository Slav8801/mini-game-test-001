using Core;
using Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace States
{
	public class GameplayState : MonoBehaviour
	{
		[SerializeField]
		private Button homeButton;
		[SerializeField]
		private Text matchesAmount;
		[SerializeField]
		private Text turnsAmount;
		[SerializeField]
		private GridLayoutGroup flipButtonsLayoutGroup;
		[SerializeField]
		private RectTransform flipButtonsLayoutGroupRect;
		[SerializeField]
		private Transform flipButtonContainer;
		[SerializeField]
		private FlipButtonController flipButtonTemplate;
		[SerializeField]
		private ImagesConfig imagesConfig;

		private List<FlipButtonController> flipButtons;

		private FlipButtonController previousFlippedButton;

		public event Action OnHomeButton;
		public event Action OnStageComplete;

		private int currentTurn = 0;

		private void Start()
		{
			flipButtons = new List<FlipButtonController>();

			homeButton.onClick.AddListener(HandleOnHomeButtonClicked);

			UpdateTurnsLabel();
			UpdateMatchesLabel();

			var difficultyLevel = PersistenceSystem.Instance.CurrentGame.DifficultyIndex;

			var difficultyVector = GetAmountOfTilesBasedOnDifficulty(difficultyLevel);

			CreateFlipButtons(difficultyVector.x * difficultyVector.y);

			StartCoroutine(SetTileSize(difficultyVector));
		}

		private void HandleOnHomeButtonClicked()
		{
			OnHomeButton?.Invoke();
		}

		private void IncreaseTurns()
		{
			currentTurn++;
			UpdateTurnsLabel();
		}

		private void UpdateMatchesLabel() => matchesAmount.text = PersistenceSystem.Instance.CurrentGame.TotalMatches.ToString();
		private void UpdateTurnsLabel() => turnsAmount.text = currentTurn.ToString();

		private void CreateFlipButtons(int amount)
		{
			var imagesPairs = imagesConfig.ImagesPairs;
			//Following line taken from: https://stackoverflow.com/questions/273313/randomize-a-listt
			var shuffledImages = imagesPairs.OrderBy(_ => UnityEngine.Random.value).ToList();
			var halfList = shuffledImages.Take(amount / 2);

			var doubledList = new List<ImageIdPair>();
			doubledList.AddRange(halfList);
			doubledList.AddRange(halfList);

			var doubleShuffledList = doubledList.OrderBy(_ => UnityEngine.Random.value).ToList();

			for (int index = 0; index < amount; index++)
			{
				var index1 = index;
				var flipButton = Instantiate(flipButtonTemplate, flipButtonContainer);
				flipButton.gameObject.SetActive(true);
				flipButton.Setup(doubleShuffledList[index]);
				flipButton.OnFlip += () => HandleOnFlipped(index1);
				flipButtons.Add(flipButton);
			}
		}

		private Vector2Int GetAmountOfTilesBasedOnDifficulty(int difficultyIndex)
		{
			var difficultyVector = Definitions.Difficulty.DIFFICULTY_MAP[difficultyIndex];
			return difficultyVector;
		}

		private IEnumerator SetTileSize(Vector2Int difficultyVector)
		{
			yield return null;
			flipButtonsLayoutGroupRect.ForceUpdateRectTransforms();

			flipButtonsLayoutGroup.constraintCount = difficultyVector.x;

			var width = flipButtonsLayoutGroupRect.rect.width;
			var height = flipButtonsLayoutGroupRect.rect.height;

			var usableWidth = width - ((difficultyVector.x - 1) * flipButtonsLayoutGroup.spacing.x);
			var usableHeight = height - ((difficultyVector.y - 1) * flipButtonsLayoutGroup.spacing.y);

			var tileWidth = usableWidth / difficultyVector.x;
			var tileHeight = usableHeight / difficultyVector.y;

			var minSize = Mathf.Min(tileWidth, tileHeight);

			flipButtonsLayoutGroup.cellSize = new Vector2(minSize, minSize);
		}

		private int flippedTiles = 0;

		private void HandleOnFlipped(int index)
		{
			flippedTiles++;
			var flippedButton = flipButtons[index];

			if (flippedTiles == 2)
			{
				if (previousFlippedButton.GetId() == flippedButton.GetId())
				{
					previousFlippedButton.HideFlipTile();
					flippedButton.HideFlipTile();

					TryToEndStage();
				}

				IncreaseTurns();
				flippedTiles = 0;
				return;
			}

			previousFlippedButton = flippedButton;
		}

		private void TryToEndStage()
		{
			for (var index = 0; index < flipButtons.Count; index++)
			{
				if (!flipButtons[index].GetIsHidden()) return;
			}

			EndStage();
		}

		private void EndStage()
		{
			PersistenceSystem.Instance.CurrentGame.TotalMatches++;
			PersistenceSystem.Instance.SaveGame();
			OnStageComplete?.Invoke();
		}
	}
}