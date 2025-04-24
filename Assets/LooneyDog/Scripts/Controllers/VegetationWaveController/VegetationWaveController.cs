using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class VegetationWaveController : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private float _waveStartPosition,_waveEndPosition,_waveStrength,
            _blendWeight;
        [SerializeField] private WaveType _waveType;
        [SerializeField] private Counting MovingFormat;
        private void FixedUpdate()
        {
            /* switch (_waveType)
             {
                 case WaveType.WeakHorizontal:
                     Wave(_waveType, _waveStrength);
                     break;
                 case WaveType.StrongHorizontal:
                     break;
                 case WaveType.WeakVertical:
                     break;
                 case WaveType.StrongVertical:
                     break;
             }*/
            Wave(_waveType, _waveStrength);
        }

        private void Wave(WaveType waveDirection,float WaveStrength) {
            /*if (MovingFormat == Counting.Ascending)
            {
                _skinnedMeshRenderer.SetBlendShapeWeight((int)waveDirection, Mathf.Lerp(0, 100, Time.deltaTime * WaveStrength));
                if (_skinnedMeshRenderer.GetBlendShapeWeight((int)waveDirection) > 90f) {
                    MovingFormat = Counting.Descending;
                }
            }
            else
            {
                _skinnedMeshRenderer.SetBlendShapeWeight((int)waveDirection, Mathf.Lerp(100, 0, Time.deltaTime * WaveStrength));
                if (_skinnedMeshRenderer.GetBlendShapeWeight((int)waveDirection) < 10f)
                {
                    MovingFormat = Counting.Ascending;
                }
            }*/

            if (MovingFormat == Counting.Ascending)
            {
                _blendWeight += _waveStrength * Time.deltaTime * 100f; // Scale factor for smoother animation
                if (_blendWeight >= _waveEndPosition)
                {
                    _blendWeight = _waveEndPosition;
                    MovingFormat = Counting.Descending;
                }
            }
            else
            {
                _blendWeight -= _waveStrength * Time.deltaTime * 100f;
                if (_blendWeight <= _waveStartPosition)
                {
                    _blendWeight = _waveStartPosition;
                    MovingFormat = Counting.Ascending;
                }
            }

            // Apply to blend shape
            _skinnedMeshRenderer.SetBlendShapeWeight((int)_waveType, _blendWeight);
        } 
    }
    public enum WaveType { 
        WeakHorizontal = 0,
        StrongHorizontal = 1,
        WeakVertical = 2,
        StrongVertical = 3,
    }

    public enum Counting { 
        Ascending = 0,
        Descending =1
    }
}
