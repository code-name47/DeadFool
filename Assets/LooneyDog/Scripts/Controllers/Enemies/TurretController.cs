using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class TurretController : MonoBehaviour
    {
        [SerializeField] private float _turretRotationSpeed,_fireRate,_startingHealth = 100f,_turretDamage;
        [SerializeField] private TurretId _turretId;
        [SerializeField] Vector2 _turretRotationDegrees;
        [SerializeField] private Transform _turretHead;
        [SerializeField] private GameObject _bullet,_bulletSpwanLeftPoint, _bulletSpwanRightPoint,_bulletMuzzle;
        [SerializeField] private BulletController _bulletController;
        [SerializeField] private EnemyBodyController _enemyBodyController;

        private float currentRotationAngle = 0f; // Track the current rotation angle
        private int rotationDirection = 1; // 1 for clockwise, -1 for counterclockwise
        public float Health { get => _startingHealth; set => _startingHealth = value; }
        public float TurretRotationSpeed { get => _turretRotationSpeed; set => _turretRotationSpeed = value; }

        private void OnEnable()
        {
            _enemyBodyController.Health = _startingHealth;
        }

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
            /*  if (_turretHead.localRotation.y >= _turretRotationDegrees.x && _turretHead.localRotation.y <= _turretRotationDegrees.y) {
                  TurretRotationSpeed = TurretRotationSpeed;
              } else
              {
                  TurretRotationSpeed = TurretRotationSpeed * -1;
                  //Fire();
              }
              _turretHead.Rotate(_turretHead.localRotation.x, _turretHead.localRotation.y * Time.deltaTime, TurretRotationSpeed);*/
            // Convert the turret's local rotation to Euler angles (in degrees)
            /*float currentAngle = _turretHead.localEulerAngles.y;

            // Normalize the angle to be between 0 and 360
            if (currentAngle > 180)
                currentAngle -= 360;
*/

            currentRotationAngle = _turretHead.localEulerAngles.y;

            // Normalize the angle to the range [0, 360)
            if (currentRotationAngle > 180)
                currentRotationAngle -= 360;

            if (currentRotationAngle >= _turretRotationDegrees.y || currentRotationAngle <= _turretRotationDegrees.x)
            {
                // Reverse the rotation direction
                rotationDirection *= -1;
            }
            // Apply rotation
            float rotationStep = TurretRotationSpeed * rotationDirection * Time.deltaTime;
            _turretHead.Rotate(0, rotationStep, 0, Space.Self);

        }

        private void DoubleRotation()
        {

        }

        public void FireLeft() {
            Instantiate(_bulletMuzzle, _bulletSpwanLeftPoint.transform.position, Quaternion.LookRotation(_bulletSpwanLeftPoint.transform.forward));
            _bulletController.BulletDamage = _turretDamage;
            Instantiate(_bulletController.gameObject, _bulletSpwanLeftPoint.transform.position, Quaternion.LookRotation(_bulletSpwanLeftPoint.transform.forward));
        }
        public void FireRight()
        {
            Instantiate(_bulletMuzzle, _bulletSpwanRightPoint.transform.position, Quaternion.LookRotation(_bulletSpwanRightPoint.transform.forward));
            _bulletController.BulletDamage = _turretDamage;
            Instantiate(_bulletController.gameObject, _bulletSpwanRightPoint.transform.position, Quaternion.LookRotation(_bulletSpwanRightPoint.transform.forward));
        }

        public void FireDouble() {
            FireRight();
            FireLeft();
        }
    }


    public enum TurretId { 
        Alternate = 0,
        Double = 1,
    }
}
