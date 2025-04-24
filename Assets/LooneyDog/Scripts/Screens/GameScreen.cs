using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
        [SerializeField] private bool _gamePaused=false,_startTimer = false;
        [SerializeField] private GameObject _waveText;
        [SerializeField] private TextMeshProUGUI _textOfWave;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _timerCount;
        [Header("SlowMotionPanel")]
        [SerializeField] private Image _slowMotionPanel;
        [SerializeField] private float _rotSlowPanel,_timeSlowValue,_timeSlowSpeed,_originalFixedDeltaTime;
        [SerializeField] private Color _fullTransparentColor,_semiTransparentColor;
        [SerializeField] private Button _kickingActioButton;
        public bool GamePaused { get => _gamePaused; set => _gamePaused = value; }
        public GameOverScreen GameOverScreen { get => _gameOverScreen; set => _gameOverScreen = value; }
        public TextMeshProUGUI TextOfWave { get => _textOfWave; set => _textOfWave = value; }

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
            _inputActions.Enable();
            _originalFixedDeltaTime = Time.fixedDeltaTime;
            _pauseButton.onClick.AddListener(OnClickPausePanel);
            _kickingActioButton.onClick.AddListener(PerformAction);
        }

        private void OnClickJoystick() { }

        private void OnEnable()
        {
            
        }

        public void Update()
        {
            /*if (_timerCount > 0 && _startTimer == true)
            {
                _timerCount -= Time.deltaTime;
                _timerText.text = Mathf.Ceil(_timerCount).ToString();
            }
            else
            {
                if (_startTimer == true)
                {
                    _startTimer = false;
                    GameManager.Game.Level.FinishWave();
                }   
            }*/

            /*if (!JoyStickDown) {
                SlowPanelUnFade();
            }*/
        }

        public void EnableActionButton(ActionID actionId) {
            switch (actionId)
            {
                case ActionID.Normal:
                    break;
                case ActionID.Kicking:
                    _kickingActioButton.gameObject.SetActive(true);
                    _kickingActioButton.enabled = true;
                    break;
                case ActionID.JumpingAcross:
                    break;
                default:
                    break;
            }
        }

        public void DisableActionButton(ActionID actionId) {
            switch (actionId)
            {
                case ActionID.Normal:
                    break;
                case ActionID.Kicking:
                    _kickingActioButton.gameObject.SetActive(false);
                    break;
                case ActionID.JumpingAcross:
                    break;
                default:
                    break;
            }
        }

        public void PerformAction() {
            GameManager.Game.Level.CurrentPlayerController.PerformAction();
            _kickingActioButton.enabled = false;
        }
        public void SetTimer(float timeCount, bool startTimer) {
            _timerCount = timeCount;
            _startTimer = startTimer;
        }

        public void SetTimerText(string timertext) {
            _timerText.text = timertext;
        }

        public void PauseGame() {
            _gamePaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame() {
            _gamePaused = false;
            Time.timeScale = 1;
        }

        public void SlowPanelFade() {
         
            _slowMotionPanel.color = Color.Lerp(_slowMotionPanel.color, _semiTransparentColor, _rotSlowPanel);
            Time.timeScale = Mathf.Lerp(Time.deltaTime, _timeSlowValue, _timeSlowSpeed);
            Time.fixedDeltaTime = Time.deltaTime * _originalFixedDeltaTime;
            GameManager.Game.Level.CameraController.ActivateSpeedLines();
            var TransparentColor = GameManager.Game.Level.CameraController.SpeedLines.startColor;
            TransparentColor.a = Mathf.Lerp(GameManager.Game.Level.CameraController.SpeedLines.startColor.a, _semiTransparentColor.a, _rotSlowPanel);
            GameManager.Game.Level.CameraController.SpeedLines.startColor = TransparentColor;
        }

        public void SlowPanelUnFade() {
            
            _slowMotionPanel.color = Color.Lerp(_slowMotionPanel.color, _fullTransparentColor, _rotSlowPanel);
            //Time.timeScale = 1;
            Time.timeScale = Mathf.Lerp(Time.deltaTime, 1, _timeSlowSpeed);
            Time.fixedDeltaTime = (1/Time.deltaTime) * _originalFixedDeltaTime;
            
            var TransparentColor = GameManager.Game.Level.CameraController.SpeedLines.startColor;
            TransparentColor.a = 0f;
            GameManager.Game.Level.CameraController.SpeedLines.startColor = TransparentColor;
            /*var TransparentColor = GameManager.Game.Level.CameraController.SpeedLines.startColor;
            TransparentColor.a = Mathf.Lerp(GameManager.Game.Level.CameraController.SpeedLines.startColor.a, _fullTransparentColor.a, _rotSlowPanel/2f);
            GameManager.Game.Level.CameraController.SpeedLines.startColor = TransparentColor;*/
            GameManager.Game.Level.CameraController.DeActivateSpeedLinnes();

            
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

        public void EnableWaveText(string text) {
            _textOfWave.text = text;
            _waveText.SetActive(true);
            StartCoroutine(DisableWaveText());
        }

        IEnumerator DisableWaveText() {
            yield return new WaitForSeconds(_waveTextTime);
            _waveText.SetActive(false);
        }
    }
}
