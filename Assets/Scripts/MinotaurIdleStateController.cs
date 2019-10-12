using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurIdleStateController : StateMachineBehaviour {
    //objects
    private GameObject player;
    private Transform playerTransform;
    
    //animator hash
    private int tauntHash = Animator.StringToHash("taunt");
    private int distanceHash = Animator.StringToHash("distance");

    private float timeIdle;
    private float tauntDelay;
    private float tauntDelayMin = 2f;
    private float tauntDelayMax = 7f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) {
            player = GameObject.FindWithTag("Player");
            playerTransform = player.transform;
        }
        
        timeIdle = 0;
        tauntDelay = Random.Range(tauntDelayMin, tauntDelayMax);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        Vector2 currentPosition = animator.transform.position;
        Vector2 playerPosition = playerTransform.position;
        
        timeIdle += Time.deltaTime;
        if (timeIdle > tauntDelay) {
            animator.SetTrigger(tauntHash);
        }
        
        float playerDistance = Vector2.Distance(currentPosition, playerPosition);
        animator.SetFloat(distanceHash, playerDistance);

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
