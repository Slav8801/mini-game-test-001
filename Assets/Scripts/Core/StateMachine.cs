using States;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
	public class StateMachine : MonoBehaviour
	{
		private Scene currentState;

		private void Awake()
		{
			//initialize utilities
		}

		private void Start()
		{
			EnterMainMenuState();
		}

		private void EnterMainMenuState()
		{
			var sceneToLoad = Definitions.StateNames.MAIN_MENU_STATE;
			StartCoroutine(LoadSceneAsync(sceneToLoad, () =>
			{
				var sceneRoot = currentState.GetRootGameObjects()[0];
				var controller = sceneRoot.GetComponent<MainMenuState>();
			}));
		}

		private void EnterGameplayState()
		{
			var sceneToLoad = Definitions.StateNames.GAMEPLAY_STATE;
			StartCoroutine(LoadSceneAsync(sceneToLoad, () =>
			{
				var sceneRoot = currentState.GetRootGameObjects()[0];
				var controller = sceneRoot.GetComponent<GameplayState>();
			}));
		}

		private void EnterStageCompleteState()
		{
			var sceneToLoad = Definitions.StateNames.STAGE_COMPLETE_STATE;
			StartCoroutine(LoadSceneAsync(sceneToLoad, () =>
			{
				var sceneRoot = currentState.GetRootGameObjects()[0];
				var controller = sceneRoot.GetComponent<StageCompleteState>();
			}));
		}

		private IEnumerator LoadSceneAsync(string sceneToLoad, Action onComplete = null)
		{
			if (currentState != null)
			{
				SceneManager.SetActiveScene(SceneManager.GetSceneByName(Definitions.StateNames.MAIN_STATE));
				SceneManager.UnloadSceneAsync(currentState);
			}

			var asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

			while (!asyncLoad.isDone)
			{
				yield return null;
			}
			
			var scene = SceneManager.GetSceneByName(sceneToLoad);
			
			currentState = scene;

			SceneManager.SetActiveScene(scene);

			onComplete?.Invoke();
		}
	}
}