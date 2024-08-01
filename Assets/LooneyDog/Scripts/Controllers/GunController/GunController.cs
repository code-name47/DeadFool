using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private float _fireRate;
        [SerializeField] private float _gunDamage;
        [SerializeField] private Transform _bulletSpwanPoint,_bulletShellSpwanPoint;
        [SerializeField] private GameObject _bullet, _bulletMuzzle;
        [SerializeField] private ParticleSystem _bulletShell;
        [SerializeField] private Animator _gunAnimator;
        [SerializeField] private Transform _parentTransform;
        

        public Animator GunAnimator { get => _gunAnimator; set => _gunAnimator = value; }

        public void Fire()
        {
            Instantiate(_bulletMuzzle, _bulletSpwanPoint.transform.position, Quaternion.LookRotation(_bulletSpwanPoint.transform.forward));
            Instantiate(_bullet, _bulletSpwanPoint.transform.position, Quaternion.LookRotation(_bulletSpwanPoint.transform.forward));
            _bulletShell.Play();
        }
    }
}
