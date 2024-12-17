using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LooneyDog
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private int _sceneIndexToBeLoaded;
        [SerializeField] GameDifficulty _difficultySet;
        [SerializeField] private float _waitTimeBeforeLoad;

        public int SceneIndexToBeLoaded { get => _sceneIndexToBeLoaded; set => _sceneIndexToBeLoaded = value; }

        public void LoadLevel(int LevelNumber, GameDifficulty gameDifficulty, GameObject FromScreen) {
            SetSceneIndexAndDifficulty(LevelNumber, gameDifficulty);
            //_sceneIndexToBeLoaded = LevelNumber;
            GameManager.Game.Screen.LoadFadeScreen(FromScreen, GameManager.Game.Screen.Load.gameObject);
        }

        private void OnEnable()
        {
            StartCoroutine(WaitTimeBeforeLoad());
        }

        IEnumerator WaitTimeBeforeLoad()
        {
            yield return new WaitForSecondsRealtime(_waitTimeBeforeLoad);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndexToBeLoaded, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            yield return new WaitForSecondsRealtime(2f);
            LoadLevel();
        }

        private void LoadLevel()
        {
            if (_sceneIndexToBeLoaded > 1)
            {
                GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Load.gameObject, GameManager.Game.Screen.GameScreen.gameObject);
                GameManager.Game.Level.SetCurrentLevelDetails(SceneIndexToBeLoaded, _difficultySet);
                GameManager.Game.Level.GetLevelData(SceneIndexToBeLoaded, _difficultySet);
            }
            else
            {
                GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Load.gameObject, GameManager.Game.Screen.Home.gameObject);
            }
        }
        public void SetSceneIndexAndDifficulty(int sceneindex, GameDifficulty gameDifficulty)
        {
            _sceneIndexToBeLoaded = sceneindex;
            _difficultySet = gameDifficulty;
        }
    }
}
