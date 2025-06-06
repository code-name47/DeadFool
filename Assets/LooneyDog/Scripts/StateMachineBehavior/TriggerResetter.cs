using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class TriggerResetter : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var p in animator.parameters)
            {
                if (p.type == AnimatorControllerParameterType.Trigger)
                {
                    animator.ResetTrigger(p.name);
                }
            }
        }
    }
}
