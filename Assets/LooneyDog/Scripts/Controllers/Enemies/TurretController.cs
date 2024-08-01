using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class TurretController : MonoBehaviour
    {
        [SerializeField] private float _turretRotationSpeed,_fireRate;
        [SerializeField] private TurretId _turretId;
        [SerializeField] Vector2 _turretRotationDegrees;
        [SerializeField] private Transform _turretHead;
        [SerializeField] private GameObject _bullet,_bulletSpwanLeftPoint, _bulletSpwanRightPoint,_bulletMuzzle;

        private void Update()
        {
            switch (_turretId)
            {
                case TurretId.Alternate:
                    AlternateRotation();
                    break;
                case TurretId.Double:
                    DoubleRotation();
                    break;
            }
        }

        private void AlternateRotation() {
            if (_turretHead.localRotation.z >= _turretRotationDegrees.x && _turretHead.localRotation.z <= _turretRotationDegrees.y) {
                _turretRotationSpeed = _turretRotationSpeed;
            } else
            {
                _turretRotationSpeed = _turretRotationSpeed * -1;
                //Fire();
            }
            _turretHead.Rotate(_turretHead.localRotation.x, _turretHead.localRotation.y, _turretRotationSpeed*Time.deltaTime);

        }

        private void DoubleRotation()
        {

        }

        public void FireLeft() {
            Instantiate(_bulletMuzzle, _bulletSpwanLeftPoint.transform.position, Quaternion.LookRotation(_bulletSpwanLeftPoint.transform.forward));
            Instantiate(_bullet, _bulletSpwanLeftPoint.transform.position, Quaternion.LookRotation(_bulletSpwanLeftPoint.transform.forward));
        }
        public void FireRight()
        {
            Instantiate(_bulletMuzzle, _bulletSpwanRightPoint.transform.position, Quaternion.LookRotation(_bulletSpwanRightPoint.transform.forward));
            Instantiate(_bullet, _bulletSpwanRightPoint.transform.position, Quaternion.LookRotation(_bulletSpwanRightPoint.transform.forward));
        }
    }


    public enum TurretId { 
        Alternate = 0,
        Double = 1,
    }
}
