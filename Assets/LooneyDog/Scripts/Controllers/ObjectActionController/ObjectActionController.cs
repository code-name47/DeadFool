using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace LooneyDog
{
    public class ObjectActionController : MonoBehaviour
    {
        [SerializeField] private Transform _objectActionPerformerPosition;
        [SerializeField] private ActionID _actionID;
        [SerializeField] private float _actionDuration,_moveDistance,_moveDuration,_moveStartDelay,_stoppingDistance,_rayHorizontalDistance;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _parentTransform,_mainObject;
        [SerializeField] private KickId _kickId;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Ray _ray;
        [SerializeField] private ParticleSystem _slideParticle;
        [SerializeField] private Ease _ease;

        public Transform MainObject { get => _mainObject; set => _mainObject = value; }
        public KickId KickId { get => _kickId; set => _kickId = value; }

        public void EnablePowerButtonAndSetPlyerController() {
            GameManager.Game.Level.CurrentPlayerController.SetNearbyActionObject(this, _actionID, _mainObject, _actionDuration);
            GameManager.Game.Screen.GameScreen.EnableActionButton(_actionID);
        }

        public void DisablePowerButtonAndSetPlyerController()
        {
            GameManager.Game.Level.CurrentPlayerController.SetNearbyActionObject(this, ActionID.Normal, null, 0);
            GameManager.Game.Screen.GameScreen.DisableActionButton(_actionID);
        }

        public void Kicked() {
            
            float ObjectDistance=_moveDistance;
            bool ObjectInBetween = false;
            Transform Object;
            ObjectDistance =  CheckObjectPath(out ObjectInBetween,out Object);
            Vector3 EndPosition = _parentTransform.position + (ObjectDistance * MainObject.forward);
            float AlteredMoveDuration = _moveDuration;
            
            if (ObjectInBetween)
            {
                _animator.SetTrigger("" + _kickId + "Collide");
                AlteredMoveDuration = (ObjectDistance / _moveDistance);
                AlteredMoveDuration = AlteredMoveDuration * _moveDuration;
            }
            else
            {
                _animator.SetTrigger("" + _kickId);
            }

            _parentTransform.DOMove(EndPosition, AlteredMoveDuration).SetDelay(_moveStartDelay).SetEase(_ease);

            _slideParticle.transform.rotation = MainObject.rotation;
            _slideParticle.gameObject.SetActive(true);
            
        }

        private float CheckObjectPath(out bool ObjectInBetween,out Transform Object) {
            RaycastHit hitInfo;
            
            bool didHit = Physics.Raycast(_parentTransform.position, MainObject.forward, out hitInfo, _moveDistance,_layerMask);
            if (didHit == false) {
                didHit = Physics.Raycast(_parentTransform.position + new Vector3(_rayHorizontalDistance, 0,0), MainObject.forward, out hitInfo, _moveDistance, _layerMask);
            }else if(didHit == false)
            {
                didHit = Physics.Raycast(_parentTransform.position - new Vector3(_rayHorizontalDistance, 0, 0), MainObject.forward, out hitInfo, _moveDistance, _layerMask);
            }
            ObjectInBetween = didHit;
            //Check if we hit something
            if (didHit)
            {
                //Get the hit object and its position
                GameObject hitObject = hitInfo.collider.gameObject;
                Vector3 hitPoint = hitInfo.point;
                Object = hitObject.transform;
                //Do something with the hit information, e.g., print to console
                Debug.Log("Raycast hit " + hitObject.name + " at " + hitPoint);
                float distanceObject = Vector3.Distance(_parentTransform.position, hitInfo.collider.gameObject.transform.position) - _stoppingDistance;
                return distanceObject;
            }
            else
            {
                //Raycast didn't hit anything
                Object = null;
                Debug.Log("Raycast did not hit anything");
                return _moveDistance;
            }
            

        }

    }

    public enum KickId {
        KickFromLeft = 0,
        KickFromRight = 1
    }
}
