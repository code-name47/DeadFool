using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private float _transitionTime;
        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBackButton);
        }
        private void OnClickBackButton() {
            Debug.Log("Resume Button Pressed");
            GameManager.Game.Screen.ClosePopUpScreen(this.transform, GameManager.Game.Screen.GameScreen.transform, ScreenLocation.Pop, _transitionTime, true);
            GameManager.Game.Screen.GameScreen.ResumeGame();
        }
    }
}
