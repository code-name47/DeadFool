using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LooneyDog
{
    public class GameScreen : MonoBehaviour
    {
        //public JoypadController JoypadController { get => _joypadController; set => _joypadController = value; }
        //public DefaultInputActions InputActions { get => _inputActions; set => _inputActions = value; }

        //[SerializeField] private JoypadController _joypadController;
        [SerializeField] public DefaultInputActions _inputActions;
        [SerializeField] private PausePanel _pauseMenu;
        [SerializeField] private float _transitionTime,_waveTextTime;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private float _duckPoints=0;
        [SerializeField] private DuckPointUiContoller _duckPointDisplayer;
        [SerializeField] Image _healthMeter;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private bool _gamePaused=false;
        [SerializeField] private GameObject _waveText;

        public bool GamePaused { get => _gamePaused; set => _gamePaused = value; }
        public GameOverScreen GameOverScreen { get => _gameOverScreen; set => _gameOverScreen = value; }

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
            _inputActions.Enable();

            _pauseButton.onClick.AddListener(OnClickPausePanel);
        }

        public void PauseGame() {
            _gamePaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame() {
            _gamePaused = false;
            Time.timeScale = 1;
        }

        private void OnClickPausePanel() {
            PauseGame();
            GameManager.Game.Screen.OpenPopUpScreen(_pauseMenu.transform, this.transform, ScreenLocation.Pop, _transitionTime, true);
            
        }

        public void AddDuckPoint() {
            _duckPoints++;
            _duckPointDisplayer.DisplayDuckPoint(_duckPoints);
        }

        public void SetHealth(float PlayerHeath) {
            _healthMeter.fillAmount = PlayerHeath;
        }

        public void CallGameOverScreen() {
            PauseGame();
            GameManager.Game.Screen.OpenPopUpScreen(GameOverScreen.transform, this.transform, ScreenLocation.down, _transitionTime, true);
        }

        public void EnableWaveText() {
            _waveText.SetActive(true);
            StartCoroutine(DisableWaveText());
        }

        IEnumerator DisableWaveText() {
            yield return new WaitForSeconds(_waveTextTime);
            _waveText.SetActive(false);
        }
    }
}
