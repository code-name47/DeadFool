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


        public void SpwanTurretByFormation(TurretType[] turretTypes,TurretBaseRotation[] baseRotation, TurretHeadRotation[] headRotation) {
            for (int i = 0; i < _turrets.Length; i++) {
                if (turretTypes != null)
                {
                    if (turretTypes[i] != TurretType.Null)
                    {
                        _turretContollers[i] = Instantiate(_turretPrefab[(int)turretTypes[i]], _turrets[i].position, Quaternion.identity).GetComponent<TurretController>();
                        _turretContollers[i].TurretBaseRotation = baseRotation[i];
                        _turretContollers[i].ApplyBaseRotation();
                        _turretContollers[i].TurretHeadRotation = headRotation[i];
                        _turretContollers[i].ApplyHeadRotation();
                    }
                }
                else
                {
                    Debug.Log("Error : Turret Type Arrey Empty");
                }
            }
        }

        public bool CheckActiveTurrets() {
            bool turretsAlive=false;
            for (int i = 0; i < _turretContollers.Length; i++) {
                if (_turretContollers[i] != null && _turretContollers[i].gameObject.activeSelf==true) {
                    turretsAlive = true;
                }
            }
            return turretsAlive;
        }

        public void DisableAllTurrets()
        {
            for (int i = 0; i < _turretContollers.Length; i++)
            {
                if (_turretContollers[i] != null && _turretContollers[i].gameObject.activeSelf == true)
                {
                    _turretContollers[i].gameObject.SetActive(false);
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
        LargeTurretAlternatingBullet = 4,
        LargeTurretDoubleBullet = 5,
        LaserTurret = 6
    }
}
