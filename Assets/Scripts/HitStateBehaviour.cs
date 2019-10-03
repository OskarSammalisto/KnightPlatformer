using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStateBehaviour : StateMachineBehaviour {
    [HideInInspector] public EnemyController enemyController;

    private int timeDisabledHash = Animator.StringToHash("timeDisabled");

    private float timeDisabled;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyController.Particles();
        timeDisabled = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timeDisabled += Time.deltaTime;
        animator.SetFloat(timeDisabledHash, timeDisabled);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyController.Particles();
        animator.SetFloat(timeDisabledHash, 0);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
