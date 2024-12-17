using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class TurretManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _turrets;
        [SerializeField] private GameObject[] _turretPrefab;
        [SerializeField] private TurretController[] _turretContollers = new TurretController[9];

        public void SpwanTurretByFormation(TurretType[] turretTypes) {
            for (int i = 0; i < _turrets.Length; i++) {
                if (turretTypes[i] != TurretType.Null) {
                    _turretContollers[i]=Instantiate(_turretPrefab[(int)turretTypes[i]], _turrets[i].position,Quaternion.identity).GetComponent<TurretController>();
                    
                }
            }
        }
        
    }

    public enum TurretType
    {
        Null = 0,
        SingleBarrelBullet = 1,
        DoubleBarrelAlternatingBullet = 2,
        DoubleBarrelBullet = 3,
        TripleBarrelBullet = 4,
        SingleBarrelLaser = 5,
        DoubleBarrelLaser = 6
    }
}
