using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class CharacterPrefab : MonoBehaviour
    {
        [SerializeField] private CharacterId _characterId;
        [SerializeField] private GunController _gunController;
        [SerializeField] private KatanaController _katanaController;
        [SerializeField] private Avatar _characterAvatar;

        public GunController GunController { get => _gunController; set => _gunController = value; }
        public KatanaController KatanaController { get => _katanaController; set => _katanaController = value; }
        public CharacterId CharacterId { get => _characterId; set => _characterId = value; }
        public Avatar CharacterAvatar { get => _characterAvatar; set => _characterAvatar = value; }
    }
}