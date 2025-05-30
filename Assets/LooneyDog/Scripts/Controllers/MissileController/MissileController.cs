using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class MissileController : MonoBehaviour
    {

        [SerializeField] private GameObject _hitParticle;
        [SerializeField] private Transform target; // Reference to the player or target
        [SerializeField] private float _speed = 10f; // Speed of the missile
        [SerializeField] private float _rotationSpeed = 5f; // Speed at which the missile rotates
        [SerializeField] private float _damage = 50f;
        [SerializeField] private MissileType _missileType;
        [SerializeField] private LayerMask _collidableLayers;

        private Rigidbody rb;

        public MissileType MissileType { get => _missileType; set => _missileType = value; }
        public Transform Target { get => target; set => target = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
        public float Damage { get => _damage; set => _damage = value; }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            switch (_missileType)
            {
                case MissileType.HomingMissileSlow:
                    HomingMissileMovement();
                    break;
                case MissileType.HomingMissileMed:
                    HomingMissileMovement();
                    break;
                case MissileType.HomingMissileFast:
                    HomingMissileMovement();
                    break;
                case MissileType.StraightMissileSlow:
                    StraightMissileMovement();
                    break;
                case MissileType.StraightMissileMedium:
                    StraightMissileMovement();
                    break;
                case MissileType.StraightMissileFast:
                    StraightMissileMovement();
                    break;
            }

        }

        private void HomingMissileMovement() {
            if (Target != null)
            {
                // Calculate the direction to the target
                Vector3 direction = (Target.position - transform.position).normalized;

                // Apply force in the direction of the target
                //rb.AddForce(direction * speed, ForceMode.Force);

                // Rotate the missile to face the target, constrained to the Y-axis
                Vector3 flatTargetDirection = new Vector3(direction.x, 0, direction.z);
                if (flatTargetDirection != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(flatTargetDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
                }
                rb.AddForce(transform.forward * Speed, ForceMode.Force);
            }
        }

        private void StraightMissileMovement() {
            rb.AddForce(transform.forward * Speed, ForceMode.Force);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                if (other.GetComponent<PlayerController>() != null)
                {
                    other.GetComponent<PlayerController>().GettingHit(Damage);
                    DestroyGameObject();

                    //GameManager.Game.Level.ObjectPooler.KillMissile(this.gameObject);
                }
                else
                {
                    //Do Nothing
                    Debug.Log("Error : Player Controller Not Found When Missile Hit");
                }

            }

            if (other.CompareTag("Obstacles"))
            {
                DestroyGameObject();
                //GameManager.Game.Level.ObjectPooler.KillMissile(this.gameObject);
            }

            if (other.CompareTag("Enemy"))
            {
                DestroyGameObject();
                if (other.GetComponent<EnemyBodyController>() != null)
                {
                    other.GetComponent<EnemyBodyController>().GettingHit(Damage);
                }
                else
                {
                    Debug.Log("No Emeny Body Controller found on Enemy");
                }
                //GameManager.Game.Level.ObjectPooler.KillMissile(this.gameObject);
                /* Instantiate(_hitParticle, transform.position, Quaternion.identity);
                 Destroy(gameObject);*/
            }
        }

        public void DestroyGameObject() {
            //Instantiate(_hitParticle, transform.position, Quaternion.identity);
            GameManager.Game.Level.ObjectPooler.SpwanMissileHit(transform);
            //Destroy(gameObject);
            GameManager.Game.Level.ObjectPooler.KillMissile(this.gameObject);
        }
    }

    public enum MissileType { 
        HomingMissileSlow = 0,
        HomingMissileMed =1,
        HomingMissileFast=2,
        StraightMissileSlow = 3,
        StraightMissileMedium = 4,
        StraightMissileFast = 5
    }
}
