using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{

    public class MissileManager : MonoBehaviour
    {
        [Header("PlaceHolder")]
        [SerializeField] private Transform[] _missilePlaceHolders;
        [SerializeField] private Transform _centerPlaceHolder;
        [SerializeField] private Transform _player;

        [Header("Prefabs")]
        [SerializeField] private GameObject _homingMissileSlow;
        [SerializeField] private GameObject _straightMissileSlow;
        [SerializeField] private GameObject _homingMissileMed;
        [SerializeField] private GameObject _straightMissileMed;
        [SerializeField] private GameObject _homingMissileFast;
        [SerializeField] private GameObject _straightMissileFast;

        [Header("Runtime")]
        [SerializeField] private List<MissileController> _missiles;

        private void OnEnable()
        {
            GameManager.Game.Level.MissileManager = this;
        }

        public void StartMissile(float[] missileDelay) {
            StartCoroutine(missileInstantiator(missileDelay));
        }

        public void LaunchMissile(int position,MissileType missileType) {
            if (GameManager.Game.Level.CurrentPlayerController.transform != null) {
                _player = GameManager.Game.Level.CurrentPlayerController.transform;
            }
            if (_player != null)
            {
                switch (missileType)
                {
                    case MissileType.HomingMissileSlow:
                        CreateMissile(_homingMissileSlow, _player, position);
                        break;
                    case MissileType.StraightMissileSlow:
                        CreateMissile(_straightMissileSlow, null, position);
                        /*Vector3 StraightPosition = new Vector3(_missilePlaceHolders[position].position.x, _straightMissile.transform.position.y, _missilePlaceHolders[position].position.z);
                        MissileController tempStraightMissileController = Instantiate(_straightMissile, StraightPosition, _missilePlaceHolders[position].rotation).GetComponent<MissileController>();
                        _missiles.Add(tempStraightMissileController);*/
                        break;
                    case MissileType.HomingMissileMed:
                        CreateMissile(_homingMissileMed, _player, position);
                        break;
                    case MissileType.HomingMissileFast:
                        CreateMissile(_homingMissileFast, _player, position);
                        break;
                    case MissileType.StraightMissileMedium:
                        CreateMissile(_straightMissileMed, _player, position);
                        break;
                    case MissileType.StraightMissileFast:
                        CreateMissile(_straightMissileFast, _player, position);
                        break;
                }


                //_missiles[_missiles.Count].Target = _player;
            }
            //_missiles(_missiles.l).Target = GameManager.Game.Level.CurrentPlayerController.transform;
        }

        private void CreateMissile(GameObject missile,Transform target, int position) {
            Vector3 HomingPosition = new Vector3(_missilePlaceHolders[position].position.x, missile.transform.position.y, _missilePlaceHolders[position].position.z);
            MissileController tempHomingMissileController = Instantiate(missile, HomingPosition, _missilePlaceHolders[position].rotation).GetComponent<MissileController>();
            tempHomingMissileController.Target = target;
            _missiles.Add(tempHomingMissileController);
        }



        private IEnumerator missileInstantiator(float[] missileDelay)
        {
            for (int i = 0; i < missileDelay.Length; i++)
            {
                yield return new WaitForSeconds(missileDelay[i]);
                _missiles[i] = Instantiate(_homingMissileSlow).GetComponent<MissileController>();
                _missiles[i].Target = GameManager.Game.Level.CurrentPlayerController.transform;
                //instantiate missile
            }

            yield return null;
        }
    }
}
