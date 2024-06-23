using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class HomeScreen : MonoBehaviour
    {
        public Button StartButton { get => _startButton; set => _startButton = value; }
        public Button SettingButton { get => _settingButton; set => _settingButton = value; }
        public Button ExitButton { get => _exitButton; set => _exitButton = value; }
        public Button ShopButton { get => _ShopButton; set => _ShopButton = value; }

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _ShopButton;

        [Header("Properties")]
        [SerializeField] private float _transitionSpeed;

        [Header("Test")]
        [SerializeField] private Button _resetButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnCLickStartButton);
            _settingButton.onClick.AddListener(OnClickSettingButton);
            _ShopButton.onClick.AddListener(OnClickShopButton);
            _exitButton.onClick.AddListener(OnClickExitButton);
            _resetButton.onClick.AddListener(OnClickRestButton);
        }
        
        public void OnCLickStartButton() {
            GameManager.Game.Screen.Load.LoadLevel(2,GameDifficulty.Easy,gameObject);

            /*GameManager.Game.Screen.LoadFadeScreen(this.gameObject, GameManager.Game.Screen.Load.gameObject);*/
        }

        public void OnClickSettingButton() {
            GameManager.Game.Screen.OpenPopUpScreen(GameManager.Game.Screen.Setting.transform,transform, ScreenLocation.right, _transitionSpeed,true);
            GameManager.Game.Screen.ClosePopUpScreen(this.transform, ScreenLocation.right, _transitionSpeed);
        }

        public void OnClickExitButton() {
            Application.Quit();
        }

        public void OnClickShopButton() {
            GameManager.Game.Screen.OpenPopUpScreen(GameManager.Game.Screen.Shop.transform, transform, ScreenLocation.down, _transitionSpeed, true);
            GameManager.Game.Screen.ClosePopUpScreen(this.transform, ScreenLocation.right, _transitionSpeed);
        }

        public void OnClickRestButton() {
            GameManager.Game.Skin.SetShipDataSpeedToZeroTest();
        }
 
    }
}
