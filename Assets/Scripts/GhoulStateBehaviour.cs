using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulStateBehaviour : StateMachineBehaviour {

    public List<Transform> ghoulRunPath;
    private Transform ghoulTransform;

    private float rotateDelay = 30;

    private int next = 0;

    private float speed = 5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ghoulTransform = animator.transform;
        ghoulRunPath = animator.gameObject.GetComponent<MenuGhoulController>().GetTransformList();
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Vector2 currentPosition = animator.transform.position;
        Vector2 target = ghoulRunPath[next].position;
        
        
        
        if (Vector2.Distance(currentPosition, target) > 1f)
        {
            animator.transform.position = Vector2.MoveTowards(currentPosition, target, speed * Time.deltaTime);
            if (ghoulTransform.position.x > target.x) {
                ghoulTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else {
                ghoulTransform.localRotation = Quaternion.Euler(0, 180, 0);
            } 
        } else {
            next = Random.Range(0, 2);
            Debug.Log(next);
        }
        
        
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
