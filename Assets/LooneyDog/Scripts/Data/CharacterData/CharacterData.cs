using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/CharacterDataScriptableObject", order = 3)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterId _characterId;
        [SerializeField] [Range(0,100f)] private float _gunPower;
        [SerializeField] [Range(0, 100f)] private float _katanaDamage;
        [SerializeField] [Range(0, 1000f)] private float _health;
        [SerializeField] [Range(0, 100f)] private float _armor;
        [SerializeField] [Range(0, 100f)] private float _heathRecoverRate;
        [SerializeField] private bool _owned, _selected;

        public CharacterId CharacterId { get => _characterId; set => _characterId = value; }
        public float GunPower { get => _gunPower; set => _gunPower = value; }
        public float KatanaDamage { get => _katanaDamage; set => _katanaDamage = value; }
        public float Health { get => _health; set => _health = value; }
        public float Armor { get => _armor; set => _armor = value; }
        public bool Owned { get => _owned; set => _owned = value; }
        public bool Selected { get => _selected; set => _selected = value; }
        public float HeathRecoverRate { get => _heathRecoverRate; set => _heathRecoverRate = value; }
    }
}
