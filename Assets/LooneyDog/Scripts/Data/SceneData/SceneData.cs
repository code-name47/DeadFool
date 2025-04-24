using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LooneyDog
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/SceneDataScriptableObject", order = 3)]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private SceneIds _sceneId;
        public SceneIds SceneId { get => _sceneId; set => _sceneId = value; }
    }

    public enum SceneIds { 
        BasketBallCourt_1=2,
        BasketBallCourt_2 = 3,
        BasketBallCourt_3 = 4,
        BasketBallCourt_4 = 5,
        BasketBallCourt_5 = 6
    }
}
