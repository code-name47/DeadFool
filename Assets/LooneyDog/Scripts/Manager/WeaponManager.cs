using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private GunData[] _gunObjects;
        [SerializeField] private KatanaData[] _katanaObjects;
        [SerializeField] private GunId _currentActiveGun;
        [SerializeField] private KatanaId _currentActiveKatana;

        public GunData[] GunObjects { get => _gunObjects; set => _gunObjects = value; }
        public GunId CurrentActiveGun { get => _currentActiveGun; set => _currentActiveGun = value; }
        public KatanaData[] KatanaObjects { get => _katanaObjects; set => _katanaObjects = value; }
        public KatanaId CurrentActiveKatana { get => _currentActiveKatana; set => _currentActiveKatana = value; }

        public void GetActiveGun()
        {
            foreach (GunData gunData in GunObjects)
            {
                if (gunData.Selected)
                {
                    CurrentActiveGun = gunData.GunId;
                }
            }
        }

        public void SetActiveGun(GunId gunid)
        {
            foreach (GunData gunData in GunObjects)
            {
                if (gunData.GunId == gunid)
                {
                    gunData.Selected = true;
                }
                else
                {
                    gunData.Selected = false;
                }
            }
        }

        //--------------------------------   Katana Section ---------------------------------

        public void GetActiveKatana()
        {
            foreach (KatanaData KatanaData in KatanaObjects)
            {
                if (KatanaData.Selected)
                {
                    CurrentActiveKatana = KatanaData.KatanaId;
                }
            }
        }

        public void SetActiveKatana(KatanaId Katanaid)
        {
            foreach (KatanaData KatanaData in KatanaObjects)
            {
                if (KatanaData.KatanaId == Katanaid)
                {
                    KatanaData.Selected = true;
                }
                else
                {
                    KatanaData.Selected = false;
                }
            }
        }
    }
}
