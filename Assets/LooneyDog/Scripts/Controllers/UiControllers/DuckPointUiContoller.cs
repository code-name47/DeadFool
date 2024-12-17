using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuckPointUiContoller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _duckpointsDisplayer;
    
    // Start is called before the first frame update

    public void DisplayDuckPoint(float DuckPoints) {
        _duckpointsDisplayer.text = "" + DuckPoints;
    }
}
