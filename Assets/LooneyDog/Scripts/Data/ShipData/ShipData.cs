using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    [CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObjects/ShipDataScriptableObject", order = 2)]
    public class ShipData : ScriptableObject
    {
        public ShiPId ShipId { get => _shipId; set => _shipId = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float Health { get => _health; set => _health = value; }
        public float Shield { get => _shield; set => _shield = value; }
        public float Boost { get => _boost; set => _boost = value; }
        public string ShipDiscription { get => _shipDiscription; set => _shipDiscription = value; }
        public string ShipName { get => _shipName; set => _shipName = value; }
        public int Weapon1Capacity { get => _weapon1Capacity; set => _weapon1Capacity = value; }
        public int Weapon2Capacity { get => _weapon2Capacity; set => _weapon2Capacity = value; }
        public float BaseDamage { get => _baseDamage; set => _baseDamage = value; }

        [SerializeField] private ShiPId _shipId;
        [Range(0,100)][SerializeField] private float _speed, _health, _shield, _boost, _baseDamage;
        [Range(0, 20)] [SerializeField] private int _weapon1Capacity, _weapon2Capacity;

        [SerializeField] private string _shipName;

        [SerializeField]
        [Multiline(10)]
        private string _shipDiscription;

        [SerializeField] private Dictionary<ShiPId, Object> _shipAttributes;

       
    }
    public enum ShiPId { 
        RoundRobin = 0,
        RedMamba = 1
    }
}
