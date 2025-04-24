using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class SetPlayerMortality : StateMachineBehaviour
    {
        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            GameManager.Game.Level.CurrentPlayerController.setPlayerImmortal();

        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            GameManager.Game.Level.CurrentPlayerController.setPlayerMortal();
        }
    }
}
