using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LooneyDog
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 _playerDirection;
        [SerializeField] private float _playerSpeed,_playerRotationSpeed;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private float _hitDelay, _smallHitDelay, _hitMoveDistance;
        [SerializeField] private BoxCollider _playerCollider;
        [SerializeField] private DialogueController _dialogueController;
        private bool _isGettingHit = false;
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
        }

        private void FixedUpdate()
        {
            if (!_isGettingHit)
            {
                MovePlayer();
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
    }
 
}
