using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{

    public class RevivePanel : MonoBehaviour
    {
        [SerializeField] private float _giveupButtonDelay;
        [SerializeField] private Button _giveUpButton;
        [SerializeField] private Button _reviveButton;

        private void Awake()
        {
            _giveUpButton.onClick.AddListener(OnClickGiveUp);
            _reviveButton.onClick.AddListener(OnClickRevive);
        }

        private void OnClickRevive() {
            GameManager.Game.Screen.GameScreen.GameOverScreen.OnClickRevive();
        }
        private void OnClickGiveUp()
        {
            GameManager.Game.Screen.GameScreen.GameOverScreen.OnClickGiveup();
        }

        private void OnEnable()
        {
            _giveUpButton.gameObject.SetActive(false);
            StartCoroutine(WaitForGiveUpButton());
        }

        private IEnumerator WaitForGiveUpButton() {
            yield return new WaitForSecondsRealtime(_giveupButtonDelay);
            _giveUpButton.gameObject.SetActive(true);
        }
    }
}
