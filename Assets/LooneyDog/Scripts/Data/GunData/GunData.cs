using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    [CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/GunDataScriptableObject", order = 5)]
    public class GunData : ScriptableObject
    {
        [SerializeField] private GunId _gunId;
        [SerializeField] private string _gunName;
        [SerializeField] [Range(0,100)] private int _fireRate;
        [SerializeField] [Range(0,100)] private int _damage;
        [SerializeField] bool _unlocked;
        [SerializeField] int _cost;
        [SerializeField] bool _owned,_selected;

        public GunId GunId { get => _gunId; set => _gunId = value; }
        public string GunName { get => _gunName; set => _gunName = value; }
        public int FireRate { get => _fireRate; set => _fireRate = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public bool Selected { get => _selected; set => _selected = value; }
        public bool Owned { get => _owned; set => _owned = value; }
    }
}
