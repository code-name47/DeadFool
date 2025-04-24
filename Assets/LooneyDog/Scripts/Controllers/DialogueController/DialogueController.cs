using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace LooneyDog
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _dialogBoxes;
        [SerializeField] private float _dialogOnScreenDelay;
        private bool _isDialogueBeingDisplayed=false;
        [SerializeField] private Vector2 _gettingHitDRange, _thoughtDialogRange, _randomTalkDRange;
        

        private void Update() {
            transform.LookAt(Camera.main.transform);
        }

        public void CallGettingHitDialogue() {
            if (!_isDialogueBeingDisplayed)
            {
                int Dialogue = Random.Range((int)_gettingHitDRange.x,(int) _gettingHitDRange.y);
                CallDialogue((DialogId)Dialogue);
            } else { 
                //do nothing
                //Debug.Log("Dialogue already running");
            }
        }

        public void CallDialogue(DialogId dialogId) {
            _isDialogueBeingDisplayed = true;
            float scale = _dialogBoxes[(int)dialogId].transform.localScale.x;
            _dialogBoxes[(int)dialogId].transform.localScale = Vector3.zero;
            _dialogBoxes[(int)dialogId].SetActive(true);
            _dialogBoxes[(int)dialogId].transform.DOScale(scale, _dialogOnScreenDelay / 4f).OnComplete(()=> {
                StartCoroutine(WaitOnScreenDialogue(dialogId,scale));
            });
        }

        private IEnumerator WaitOnScreenDialogue(DialogId dialogId, float scale) {
            yield return new WaitForSeconds(_dialogOnScreenDelay / 2f);
            _dialogBoxes[(int)dialogId].transform.DOScale(Vector3.zero, _dialogOnScreenDelay / 4f).OnComplete(()=>{
                _dialogBoxes[(int)dialogId].SetActive(false);
                _dialogBoxes[(int)dialogId].transform.localScale = Vector3.one*scale;
                _isDialogueBeingDisplayed = false;
            });
        }
    }
    public enum DialogId{
        Ouch = 0,
        BWord = 1,
        WifeThought = 2,
        TRex = 3
    }
}
