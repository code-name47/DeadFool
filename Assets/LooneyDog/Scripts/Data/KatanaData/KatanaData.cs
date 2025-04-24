using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    [CreateAssetMenu(fileName ="Katana",menuName = "ScriptableObjects/KatanaDataScriptableObject", order =5)]
    public class KatanaData : ScriptableObject
    {
        [SerializeField] private KatanaId _katanaId;
        [SerializeField] private string _katanaName;
        [SerializeField] [Range(0, 100)] private int _damage;
        [SerializeField] bool _unlocked;
        [SerializeField] int _cost;
        [SerializeField] bool _owned, _selected;

        public KatanaId KatanaId { get => _katanaId; set => _katanaId = value; }
        public string KatanaName { get => _katanaName; set => _katanaName = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public bool Unlocked { get => _unlocked; set => _unlocked = value; }
        public int Cost { get => _cost; set => _cost = value; }
        public bool Owned { get => _owned; set => _owned = value; }
        public bool Selected { get => _selected; set => _selected = value; }
    }
}