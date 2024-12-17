using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class EnemyBodyController : MonoBehaviour
    {
        [SerializeField] private Image _healthMeter;
        [SerializeField] private float _health = 100f,_duckProbability,_randomizeSpwanOffset;
        [SerializeField] private GameObject _destructionObject,_baseObject;
        [SerializeField] private EnemyClass _class;
        [SerializeField] private DuckController _duckpoint;
        public float Health { get => _health; set => _health = value; }

        public void GettingHit(float Quantity) {
            switch (_class)
            {
                case EnemyClass.Turret:
                    TurretGettinghit(Quantity);
                    break;
                case EnemyClass.Missile:
                    MissileGettingHit(Quantity);
                    break;
                default:
                    break;
            }
            
        }

        private void TurretGettinghit(float Quantity) {
            _health -= Quantity;
            _healthMeter.fillAmount = _health / 100f;
            ExpelDucks();
            if (_health <= 0)
            {
                _baseObject.SetActive(false);
                _destructionObject.SetActive(true);
            }
        }


        private void MissileGettingHit(float Quantity) {
            _health -= Quantity;
            if (_health <= 0)
            {
                if (gameObject.GetComponent<MissileController>() != null)
                {
                    gameObject.GetComponent<MissileController>().DestroyGameObject();
                }
                else {
                    Debug.Log("Missile Controller Not found");
                }
            }
        }

        private void ExpelDucks() {
            int duckcount = Random.Range(0, (int)_duckProbability);
            DuckController newDuck=null;
            Vector3 randomizeSpwanPosition;

            for (int i = 0; i < duckcount; i++)
            {
                randomizeSpwanPosition = new Vector3(Random.Range((transform.position.x - _randomizeSpwanOffset), transform.position.x + _randomizeSpwanOffset),
                                         Random.Range(transform.position.y - _randomizeSpwanOffset, transform.position.y + _randomizeSpwanOffset),
                                         Random.Range(transform.position.z - _randomizeSpwanOffset, transform.position.z + _randomizeSpwanOffset));

                newDuck = Instantiate(_duckpoint.gameObject, randomizeSpwanPosition, _duckpoint.transform.rotation).GetComponent<DuckController>();
                newDuck.SummonDuck(transform.position);
            }
        }
    }

    public enum EnemyClass { 
        Turret = 1,
        Missile = 2
    }
}
