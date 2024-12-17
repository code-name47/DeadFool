using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _giveupPanel, _revivePanel;
        [SerializeField] private Button _reviveButton, _giveupButton, _backtoMenuButton;
        [SerializeField] private float _giveupButtonDelay,_transitionTime;

        private void Awake()
        {
            //_reviveButton.onClick.AddListener(OnClickRevive);
            _giveupButton.onClick.AddListener(OnClickGiveup);
            //_backtoMenuButton.onClick.AddListener(OnClickBackToMenu);
        }
        private void OnEnable()
        {
            _revivePanel.SetActive(true);
            _giveupButton.gameObject.SetActive(false);
            _giveupPanel.SetActive(false);

            StartCoroutine(DelayedGiveUpButtonEmergence());
        }

        private IEnumerator DelayedGiveUpButtonEmergence() {
            yield return new WaitForSecondsRealtime(_giveupButtonDelay);
            _giveupButton.gameObject.SetActive(true);
        }

        public void OnClickRevive() {
            GameManager.Game.Screen.ClosePopUpScreen(this.transform, GameManager.Game.Screen.GameScreen.transform, ScreenLocation.Pop, _transitionTime, true);
            GameManager.Game.Screen.GameScreen.ResumeGame();
            GameManager.Game.Level.CurrentPlayerController.PlayerRevived();
        }

        public void OnClickGiveup() {
            GameManager.Game.Screen.GameScreen.ResumeGame();
            GameManager.Game.Screen.ClosePopUpScreen(GameManager.Game.Screen.GameScreen.transform, ScreenLocation.Pop, _transitionTime);
            GameManager.Game.Screen.Load.LoadLevel(1, GameDifficulty.Easy, gameObject);
        }

        public void ActivateGivepUpPanel() {
            GameManager.Game.Screen.ClosePopUpScreen(_revivePanel.transform, ScreenLocation.down, _transitionTime);
            GameManager.Game.Screen.OpenPopUpScreen(_giveupPanel.transform, this.transform, ScreenLocation.Pop, _transitionTime, true);
        }
        private void OnClickBackToMenu() { }
    }
}
