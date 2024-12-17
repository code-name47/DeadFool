using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LooneyDog
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 _playerDirection;
        [SerializeField] private float _playerSpeed,_playerRotationSpeed,_playerHealth,_playerTotalHealth;
        [SerializeField] private float _playerRecoverRate;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private float _hitDelay, _smallHitDelay, _hitMoveDistance,_deadScreenDelay,_reviveDelay;
        [SerializeField] private BoxCollider _playerCollider;
        [SerializeField] private DialogueController _dialogueController;
        private bool _isGettingHit = false, _isPlayerDead = false;
        [SerializeField] private bool _iswieldedKatana=false,_isdeflecting=false,_isWeildingGun;
        [SerializeField] private KatanaController _katana;
        [SerializeField] private GunController _gunLeft,_gunRight;

        public bool IswieldedKatana { get => _iswieldedKatana; set => _iswieldedKatana = value; }
        public GunController GunLeft { get => _gunLeft; set => _gunLeft = value; }
        public KatanaController Katana { get => _katana; set => _katana = value; }
        public Animator PlayerAnimator { get => _playerAnimator; set => _playerAnimator = value; }

        private void OnEnable()
        {
            GameManager.Game.Level.CurrentPlayerController = this;

            GetPlayerData();
        }

        public void GetPlayerData() {
            _isPlayerDead = false;
            CharacterData ActiveCharacter = GameManager.Game.Skin.CharacterObject[(int)GameManager.Game.Skin.CurrentActiveCharacter];
            _playerTotalHealth = ActiveCharacter.Health;
            _playerRecoverRate = ActiveCharacter.HeathRecoverRate;
            _playerHealth = _playerTotalHealth;
        }

        public void RecoverHealth() {
            if (_playerHealth< _playerTotalHealth) {
                _playerHealth += (_playerRecoverRate / 100f) * Time.deltaTime;
                GameManager.Game.Screen.GameScreen.SetHealth(_playerHealth/_playerTotalHealth);
            }
        }
        private void FixedUpdate()
        {
            RecoverHealth();
            if (!_isGettingHit)
            {
                if (!_isPlayerDead)
                {
                    MovePlayer();
                }
            }
           /* else {
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward*_hitMoveDistance, _hitDelay*Time.deltaTime); 
            }*/
        }

        public void OnMove(InputValue input)
        {
            _playerDirection = input.Get<Vector2>();
            PlayerAnimator.SetFloat("Moving", _playerDirection.magnitude);
        }

        public void GettingHit() {
            PlayerAnimator.SetTrigger("GettingHit");
            //_dialogueController.CallDialogue(DialogId.Ouch);
            _dialogueController.CallGettingHitDialogue();
            _playerCollider.enabled = false;
            //_isGettingHit = true;
            StartCoroutine(WaitTillHit(_hitDelay));
        }
        public void GettingSmallHit()
        {
            if (_isWeildingGun) {
                PlayerAnimator.SetLayerWeight(1, 0);
            }

            if (!_isdeflecting)
            {
                _isdeflecting = true;
                PlayerAnimator.SetTrigger("GettingSmallHit");
                //_dialogueController.CallDialogue(DialogId.Ouch);
                _dialogueController.CallGettingHitDialogue();
            }
            
            if (!_iswieldedKatana)
            {
                Debug.Log("katanaHit Called");
                _playerCollider.enabled = false;
                _isGettingHit = true;
            }
            StartCoroutine(WaitTillHit(_smallHitDelay));
        }

        public void ReduceHeath(float Damage) {
            
            _playerHealth -= Damage;
            if (_playerHealth <= 0) {
                PlayerDieing();
            }
            Debug.Log(" Damage done by bullet is :" +Damage);
            GameManager.Game.Screen.GameScreen.SetHealth(_playerHealth / _playerTotalHealth); //Normalizeing
        }

        public void PlayerDieing() {
            _isPlayerDead = true;
            _playerAnimator.SetBool("Dead", _isPlayerDead);
            SetUnArmed();

            StartCoroutine(WaitForScreen());
        }

        public void PlayerRevived() {
            _playerAnimator.SetBool("Dead", false);
            StartCoroutine(WaitForRevive());
        }

        public void OnFireLeft() {
            GunLeft.GunAnimator.SetTrigger("Fire");
        }
        public void OnFireRight()
        {
            _gunRight.GunAnimator.SetTrigger("Fire");
        }

        IEnumerator WaitTillHit(float hitDelay) {
            yield return new WaitForSeconds(hitDelay);
            if (_isWeildingGun)
            {
                PlayerAnimator.SetLayerWeight(1, 1);
            }

            _isGettingHit = false;
            _playerCollider.enabled = true;
            _isdeflecting = false;
        }

        IEnumerator WaitForScreen() {
            yield return new WaitForSeconds(_deadScreenDelay);
            GameManager.Game.Screen.GameScreen.CallGameOverScreen();
        }

        IEnumerator WaitForRevive()
        {
            yield return new WaitForSeconds(_reviveDelay);
            _isPlayerDead = false;
            _playerHealth = _playerTotalHealth;
        }

        private void MovePlayer()
        {
            if (_playerDirection != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(_playerDirection.x, 0, _playerDirection.y).normalized;
                Vector3 newPosition = transform.localPosition + moveDirection * (_playerSpeed*_playerDirection.magnitude) * Time.deltaTime;
                transform.localPosition = new Vector3(newPosition.x, transform.localPosition.y, newPosition.z);

                // Rotate to look where it's moving
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _playerRotationSpeed); // Smooth rotation
            }
        }

        public void SetKatanaWeilder() {
            _iswieldedKatana = true;
            _isWeildingGun = false;
            _playerAnimator.SetBool("Katana", true);
            _playerAnimator.SetBool("Pistol", false);
            _playerAnimator.SetTrigger("Roll");
            _playerAnimator.SetLayerWeight(1, 0);
            _katana.gameObject.SetActive(true);
            _gunLeft.gameObject.SetActive(false);
        }

        public void SetPistolWeilder()
        {
            _iswieldedKatana = false;
            _isWeildingGun = true;
            _playerAnimator.SetBool("Katana", false);
            _playerAnimator.SetBool("Pistol", true);
            _playerAnimator.SetTrigger("Roll");
            _playerAnimator.SetLayerWeight(1, 1);
            _katana.gameObject.SetActive(false);
            _gunLeft.gameObject.SetActive(true);
        }

        public void SetUnArmed()
        {
            _iswieldedKatana = false;
            _isWeildingGun = false;
            _playerAnimator.SetBool("Katana", false);
            _playerAnimator.SetBool("Pistol", false);
            //_playerAnimator.SetTrigger("Roll");
            _playerAnimator.SetLayerWeight(1, 0);
            _katana.gameObject.SetActive(false);
            _gunLeft.gameObject.SetActive(false);
        }

    }
 
}
