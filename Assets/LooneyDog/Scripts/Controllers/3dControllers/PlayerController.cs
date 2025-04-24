using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LooneyDog
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 _playerDirection,_lastKnownPlayerDirection=Vector2.zero;
        [SerializeField] private float _playerSpeed,_playerRotationSpeed,_playerHealth,_playerTotalHealth;
        [SerializeField] private float _playerRecoverRate;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private float _hitDelay, _smallHitDelay, _hitMoveDistance,_deadScreenDelay,_reviveDelay,_stoppingDelay,_stoppingDistance;
        [SerializeField] private BoxCollider _playerCollider;
        [SerializeField] private DialogueController _dialogueController;
        private bool _isGettingHit = false, _isPlayerDead = false;
        [SerializeField] private bool _iswieldedKatana=false,_isdeflecting=false,_isWeildingGun;
        [SerializeField] private KatanaController _katana;
        [SerializeField] private GunController _gunLeft,_gunRight;
        [SerializeField] private Vector3 currentVelocity=Vector3.zero;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("ActionPerformers")]
        [SerializeField] private ObjectActionController _actionPerformee;
        [SerializeField] private Transform _actionPerformerPosition;
        [SerializeField] private bool _performingAction, _playerImmortal = false,_snapToPosition=false;
        [SerializeField] private float _actionTime;
        [SerializeField] private ActionID _actionId;

        [Header("CharacterTrails")]
        [SerializeField] private ParticleSystem _cloudDustTrail;

        public bool IswieldedKatana { get => _iswieldedKatana; set => _iswieldedKatana = value; }
        public GunController GunLeft { get => _gunLeft; set => _gunLeft = value; }
        public KatanaController Katana { get => _katana; set => _katana = value; }
        public Animator PlayerAnimator { get => _playerAnimator; set => _playerAnimator = value; }
        public ObjectActionController ActionPerformee { get => _actionPerformee; set => _actionPerformee = value; }

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
            SetUnArmed();//Player Arm status
        }

        public void RecoverHealth() {
            if (_playerHealth< _playerTotalHealth) {
                _playerHealth += (_playerRecoverRate / 100f) * Time.deltaTime;
                GameManager.Game.Screen.GameScreen.SetHealth(_playerHealth/_playerTotalHealth);
            }
        }
        private void Update()
        {
            _rigidbody.velocity = Vector3.zero;
            RecoverHealth();
            if (!_isGettingHit)
            {
                if (!_isPlayerDead)
                {
                    if (_performingAction)
                    {
                        PerformAction();
                    }
                    else
                    {
                        MovePlayer();
                    }
                }
            }
            TimeSlow();
            
        }
      
        //---------------------------------   Perform Action ------------------------------------------
        public void SetNearbyActionObject(ObjectActionController actionperformee,ActionID actionid,Transform Object,float duration) {
            ActionPerformee = actionperformee;
            _actionId = actionid;
            _actionPerformerPosition = Object;
            _actionTime = duration;
        }
     
        public void PerformAction() {
            
            if (!_snapToPosition)
            {
                if (_playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Stop")
                {
                    AnimateAction(_actionId);
                    AlignToTarget(_actionPerformerPosition, _actionTime);
                }
            }
        }

        private void AnimateAction(ActionID actionID) {
            _playerAnimator.SetTrigger("" + actionID);
        }

        public void AlignToTarget(Transform target, float duration)
        {
            _snapToPosition = true;
            StartCoroutine(AlignToPositionAndRotation(target, duration));
        }

        private IEnumerator AlignToPositionAndRotation(Transform target, float duration)
        {
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            Vector3 endPos = target.position;
            Quaternion endRot = target.rotation;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                transform.position = Vector3.Lerp(startPos, endPos, t);
                transform.rotation = Quaternion.Slerp(startRot, endRot, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            
            // Snap to final transform
            transform.position = endPos;
            transform.rotation = endRot;
            //AnimateAction(_actionId);
            _snapToPosition = false;
        }

        //------------------------------------------  End Action  -----------------------------------



        //------------------------------------------ Character Movement -----------------------------

        public void OnMove(InputValue input)
        {
            _playerDirection = input.Get<Vector2>();
            PlayerAnimator.SetFloat("Moving", _playerDirection.magnitude);
        }

        private void MovePlayer()
        {

            if (_playerDirection != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(_playerDirection.x, 0, _playerDirection.y).normalized;
                currentVelocity = moveDirection * (_playerSpeed * _playerDirection.magnitude) * Time.deltaTime;
                Vector3 newPosition = transform.localPosition + currentVelocity;
                transform.localPosition = new Vector3(newPosition.x, transform.localPosition.y, newPosition.z);

                // Rotate to look where it's moving
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _playerRotationSpeed); // Smooth rotation
                ActivateCharacterTrail();
            }
            else
            {
                DeActivateCharacterTrail();
            }
        }

        //------------------------------------------End Character Movement -----------------------------


        //------------------------------------------Character getting Hit ------------------------------
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
            if (!_playerImmortal)
            {
                if (_isWeildingGun)
                {
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
                    //Debug.Log("katanaHit Called");
                    _playerCollider.enabled = false;
                    _isGettingHit = true;
                }
                StartCoroutine(WaitTillHit(_smallHitDelay));
            }
        }

        public void ReduceHeath(float Damage) {
            if (!_playerImmortal)
            {
                _playerHealth -= Damage;
                if (_playerHealth <= 0)
                {
                    PlayerDieing();
                }
                Debug.Log(" Damage done by bullet is :" + Damage);
                GameManager.Game.Screen.GameScreen.SetHealth(_playerHealth / _playerTotalHealth); //Normalizeing
            }
        }

        IEnumerator WaitTillHit(float hitDelay)
        {
            yield return new WaitForSeconds(hitDelay);
            if (_isWeildingGun)
            {
                PlayerAnimator.SetLayerWeight(1, 1);
            }

            _isGettingHit = false;
            _playerCollider.enabled = true;
            _isdeflecting = false;
        }


        //------------------------------------------End Character getting Hit ------------------------------

        //------------------------------------------ Player Dieing ---------------------------------------
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


        IEnumerator WaitForScreen()
        {
            yield return new WaitForSeconds(_deadScreenDelay);
            GameManager.Game.Screen.GameScreen.CallGameOverScreen();
        }

        IEnumerator WaitForRevive()
        {
            yield return new WaitForSeconds(_reviveDelay);
            _isPlayerDead = false;
            _playerHealth = _playerTotalHealth;
        }

        //------------------------------------------End Player Dieing ---------------------------------------

        //------------------------------------------Character Fire ----------------------------------------
        public void OnFireLeft() {
            GunLeft.GunAnimator.SetTrigger("Fire");
        }
        public void OnFireRight()
        {
            _gunRight.GunAnimator.SetTrigger("Fire");
        }

        //------------------------------------------End Character Fire ---------------------------------

        //------------------------------------------ Character Trails ----------------------------------
        private void ActivateCharacterTrail() {
            _cloudDustTrail.gameObject.SetActive(true);
        }

        private void DeActivateCharacterTrail() {
            _cloudDustTrail.gameObject.SetActive(true);
        }



        //------------------------------------------ End Of Character Trails ---------------------------
        //------------------------------------------Time Slow -------------------------------
        private void TimeSlow() {
            if (_playerDirection != Vector2.zero)
            {
                GameManager.Game.Screen.GameScreen.SlowPanelUnFade();
            }
            else
            {
                GameManager.Game.Screen.GameScreen.SlowPanelFade();
            }
        }

        //------------------------------------------End Of Time Slow ---------------------------

        //------------------------------------------Set Animation States ----------------------
        public void setPerformingAction(bool isperforming) {
            _performingAction = isperforming;
        }

        public void ResetPerformPositionSnap() {
            _snapToPosition = false;
        }

        public void setPlayerImmortal() {
            _playerImmortal = true;
        }

        public void setPlayerMortal() {
            _playerImmortal = false;
        }
        //----------------------------------------- End of Animaiton states --------------------------
        //-----------------------------------   Set character States ---------------------------------

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
        //----------------------------------- End Of Set character States ---------------------------------
    }

}
