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
        [SerializeField] private TurretBaseRotation _turretBaseRotation;
        [SerializeField] private TurretHeadRotation _turretHeadRotation;

        private float currentRotationAngle = 0f; // Track the current rotation angle
        private int rotationDirection = 1; // 1 for clockwise, -1 for counterclockwise
        public float Health { get => _startingHealth; set => _startingHealth = value; }
        public float TurretRotationSpeed { get => _turretRotationSpeed; set => _turretRotationSpeed = value; }
        public TurretBaseRotation TurretBaseRotation { get => _turretBaseRotation; set => _turretBaseRotation = value; }
        public TurretHeadRotation TurretHeadRotation { get => _turretHeadRotation; set => _turretHeadRotation = value; }

        private void OnEnable()
        {
            _enemyBodyController.Health = _startingHealth;
            //ApplyBaseRotation(TurretBaseRotation);
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
            if (_turretHeadRotation != TurretHeadRotation.Fixed)
            {
                _turretHead.Rotate(0, rotationStep, 0, Space.Self);
            }

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

        public void ApplyBaseRotation() {
            switch (_turretBaseRotation)
            {
                case TurretBaseRotation.Left:
                    //Debug.Log("in switch case left");
                    transform.Rotate(0,-90,0,Space.World);
                    break;
                case TurretBaseRotation.Top:
                    //Debug.Log("in switch case left");
                    transform.Rotate(0, 0, 0, Space.World);
                    break;
                case TurretBaseRotation.Right:
                    //Debug.Log("in switch case left");
                    transform.Rotate(0, 90, 0, Space.World);
                    break;
                case TurretBaseRotation.Bottom:
                    //Debug.Log("in switch case left");
                    transform.Rotate(0, 180, 0, Space.World);
                    break;
            }
        }

        public void ApplyHeadRotation()
        {
            switch (_turretHeadRotation)
            {
                case TurretHeadRotation.Fixed:
                    break;
                case TurretHeadRotation.LeftQuater:
                    _turretRotationDegrees.x = -89;
                    _turretRotationDegrees.y = 1;
                    break;
                case TurretHeadRotation.RightQuater:
                    _turretRotationDegrees.x = -1;
                    _turretRotationDegrees.y = 89;
                    break;
                case TurretHeadRotation.HalfHemisphere:
                    _turretRotationDegrees.x = -89;
                    _turretRotationDegrees.y = 89;
                    break;
                case TurretHeadRotation.Full360Rotation:
                    _turretRotationDegrees.x = -180;
                    _turretRotationDegrees.y = 180;
                    break;
            }
        }
    }


    public enum TurretId { 
        Alternate = 0,
        Double = 1,
    }

    public enum TurretBaseRotation {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }

    public enum TurretHeadRotation
    {
        Fixed = 0,
        LeftQuater = 1,
        RightQuater = 2,
        HalfHemisphere = 3,
        Full360Rotation = 4
    }
}
