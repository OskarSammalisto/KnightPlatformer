using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurWalkState : StateMachineBehaviour
{
    
    //objects
    private GameObject player;
    private Transform playerTransform;
    
    //animator hash
    private int distanceHash = Animator.StringToHash("distance");
    
    //walk speed
    private float speed = 2f;
    
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) {
            player = GameObject.FindWithTag("Player");
            playerTransform = player.transform;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 target = playerTransform.position;

        animator.transform.position = Vector2.MoveTowards(current, target, speed * Time.deltaTime);

        animator.SetFloat(distanceHash, Vector2.Distance(current, target));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
