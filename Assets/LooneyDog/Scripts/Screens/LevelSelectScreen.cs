using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
namespace LooneyDog
{
    public class LevelSelectScreen : MonoBehaviour
    {
        [Header("LevelData")]
        [SerializeField] private int _levelNumber=1;
        [SerializeField] private int _maxLevels;
        [SerializeField] private float _groundRotationtime;
        [SerializeField] private Ease ease;

        [Header("Buttons")]
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _backButton;

        

        
        [Header("LevelTexts")]
        [SerializeField] private TextMeshProUGUI _titleBox;
        [SerializeField] private TextMeshProUGUI _storyBox;

        public Button NextButton { get => _nextButton; set => _nextButton = value; }
        public Button PreviousButton { get => _previousButton; set => _previousButton = value; }
        public Button StartGameButton { get => _startGameButton; set => _startGameButton = value; }
        public Button BackButton { get => _backButton; set => _backButton = value; }

        public void Awake()
        {
            _previousButton.onClick.AddListener(OnClickPreviousButton);
            _nextButton.onClick.AddListener(OnClickNextButton);
            _startGameButton.onClick.AddListener(OnClickStartButton);
            _backButton.onClick.AddListener(OnClickBackButton);

        }

        public void OnEnable()
        {
            FillLevelDetails();
        }



        private void OnClickPreviousButton() {
            Transform _levelGround = GameManager.Game.Level.LevelGrounds;
            _levelGround.DOMoveZ(_levelGround.position.z + 30, _groundRotationtime, true).SetEase(ease).OnStart(()=> {
                GameManager.Game.Screen.DeactivateAllButtons(gameObject, _groundRotationtime);
            });
            _levelNumber--;
            FillLevelDetails();
        }

        private void OnClickNextButton() {
            Transform _levelGround = GameManager.Game.Level.LevelGrounds;
            _levelGround.DOMoveZ(_levelGround.position.z - 30, _groundRotationtime, true).SetEase(ease).SetEase(ease).OnStart(() => {
                GameManager.Game.Screen.DeactivateAllButtons(gameObject, _groundRotationtime);
            }); ;
            _levelNumber++;
            FillLevelDetails();
        }

        private void CheckButtonEnabled() {
            if (_levelNumber >= _maxLevels)
            {
                _nextButton.gameObject.SetActive(false);
            }
            else {
                _nextButton.gameObject.SetActive(true);
            }
            if (_levelNumber <= 1)
            {
                _previousButton.gameObject.SetActive(false);
            }
            else {
                _previousButton.gameObject.SetActive (true);
            }
        }

        private void FillLevelDetails() {
            string title, story;
            _maxLevels = GameManager.Game.Level.GetLevelTitleAndStory(_levelNumber-1, out title, out story);
            _titleBox.text = title;
            _storyBox.text = story;
            CheckButtonEnabled();
        }

        private void OnClickStartButton() {
            GameManager.Game.Screen.Load.LoadLevel(2+_levelNumber, GameDifficulty.Easy, gameObject);
            GameManager.Game.Sound.PlayUisound(UiClipId.Click);
        }

        private void OnClickBackButton() {
            GameManager.Game.Screen.Load.LoadLevel(1, GameDifficulty.Easy, gameObject);
        
        }

    }

}
