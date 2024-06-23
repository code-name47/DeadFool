using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
namespace LooneyDog
{
    public class SplashScreen : MonoBehaviour
    {
        public Image LooneyDogLogo { get { return _looneyDogLogo; } set { _looneyDogLogo = value; } }
        [SerializeField] private Image _looneyDogLogo,_gameLogo;
        [SerializeField] private TextMeshProUGUI _looneyDogText,_gameText;
        [SerializeField] private float _transitionSpeed;
        [SerializeField] private float _logoAppearDelay,_gameLogoAppearDelay;
        [SerializeField] private float _logoScreenTime, _gameLogoScreenTime;
        [SerializeField] private int _menuSceneIndex;
 
        private void OnEnable()
        {
            StartCoroutine(LogoAppear());
        }


        private IEnumerator LogoAppear()
        {
            yield return new WaitForSeconds(_logoAppearDelay);
            _looneyDogLogo.DOFade(1, _transitionSpeed).OnStart(()=> {
                _looneyDogText.DOFade(1, _transitionSpeed);
            }).OnComplete(() => {
                StartCoroutine(LogoDisappear()); 
            });
        }

        private IEnumerator LogoDisappear()
        {
            //LoadMenuScene(); // The limbo Scene is for loading stuff which you need to load before Home Scene loads like advertisements and all
            yield return new WaitForSeconds(_logoScreenTime);
            _looneyDogLogo.DOFade(0, _transitionSpeed).OnStart(() => {
                _looneyDogText.DOFade(0, _transitionSpeed);
            }).OnComplete(() => {
                StartCoroutine(GameLogoAppear());
            });
            //GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Splsh.gameObject, GameManager.Game.Screen.Home.gameObject);    
        }

        private IEnumerator GameLogoAppear() {
            yield return new WaitForSeconds(_gameLogoAppearDelay);
            _gameLogo.DOFade(1, _transitionSpeed).OnStart(() => {
                _gameText.DOFade(1, _transitionSpeed);
            }).OnComplete(() => {
                StartCoroutine(GameLogoDisappear());
            });
        }

        private IEnumerator GameLogoDisappear() {
            LoadMenuScene(); // The limbo Scene is for loading stuff which you need to load before Home Scene loads like advertisements and all
            yield return new WaitForSeconds(_gameLogoScreenTime);
            GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Splsh.gameObject, GameManager.Game.Screen.Home.gameObject);
        }

        private void LoadMenuScene() {
            GameManager.Game.Screen.Load.LoadLevel(_menuSceneIndex, GameDifficulty.Easy,gameObject);
            //SceneManager.LoadScene(_menuSceneIndex);
        }

    }
}