using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class SetActionPerfomer : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameManager.Game.Level.CurrentPlayerController.setPerformingAction(true);
            GameManager.Game.Level.CurrentPlayerController.ActionPerformee.Kicked();
            
            //Debug.Log("statemachine enter being called");
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameManager.Game.Level.CurrentPlayerController.setPerformingAction(false);
            GameManager.Game.Level.CurrentPlayerController.ResetPerformPositionSnap();
            //Debug.Log("statemachine exit being called");
        }
    }
}
