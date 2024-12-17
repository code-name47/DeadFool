using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LooneyDog {
    public class TopPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _duckpoints;

        private void OnEnable()
        {
            UpdateTopPanel();
        }

        public void UpdateTopPanel()
        {
            int duckPoints = GameManager.Game.Data.player.UpdateDuckPoints();
            _duckpoints.text = "" + duckPoints;
        }
    }
}
