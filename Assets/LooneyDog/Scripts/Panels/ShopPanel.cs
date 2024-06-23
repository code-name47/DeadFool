using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LooneyDog
{
    public class ShopPanel : MonoBehaviour
    {
        [Header("Ship Details")]
        [SerializeField] private Image _speed,_hull,_armor,_thrust;
        [SerializeField] private TextMeshProUGUI _shipDescription;
        [SerializeField] private TextMeshProUGUI _shipName;
        public void SetShipAttributes(string shipName,float speed, float hull, float armor, float thrust, string description) {
            _speed.fillAmount = speed / 100f;
            _hull.fillAmount = hull / 100f;
            _armor.fillAmount = armor / 100f;
            _thrust.fillAmount = thrust / 100f;
            _shipDescription.text = description;
            _shipName.text = shipName;
        }
    }
}
