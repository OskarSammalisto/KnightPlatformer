using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D Controller2D;
    public Joystick joystick;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public GameObject ceilingCheck;
    
    
    //Points for sword stab linecast
    public GameObject stabStart;
    public GameObject stabEnd;

    public GameObject crouchStabStart;
    public GameObject crouchStabEnd;
    
    //List of points for sword swing linecast
    public List<Transform> swingDownList = new List<Transform>();
    public List<Transform> swingUpList = new List<Transform>();
   
    
    //Animator Condition Hash Strings....
    private int speedHash = Animator.StringToHash("speed");
    private int isCrouchingHash = Animator.StringToHash("isCrouching");
    private int stabHash = Animator.StringToHash("stab");
    private int isJumpingHash = Animator.StringToHash("isJumping");
    private int swingUpHash = Animator.StringToHash("swingUp");
    private int swingDownHash = Animator.StringToHash("swingDown");
    private int shieldUpHash = Animator.StringToHash("shieldUp");
    //bow
    private int bowHash = Animator.StringToHash("bow");
    private int shootForwardHash = Animator.StringToHash("shootForward");


        //movement settings
    private float joystickOffset = 0.2f;  //Offset until player moves
    private float joystickJumpOffset = 0.8f;  //Offset until player jumps/crouches
    private  float joystickCrouchOffset = 0.5f;
    private bool canJump = true;          //prevents instant rejump on landing  
    private float nextJumpDelay = 1.2f;    //set delay between jumps
    private float horizontalMove = 0;        
    private float runSpeed = 15f;

    private bool jump = false;
    private bool crouch = false;
    private bool canUseShield = true;
    [SerializeField]
    private bool shieldActive = false;
    private float shieldActiveTime = 0.6f;
    private float shieldWaitTime = 1f;
    private bool bowActive = false;
    
    
    
    
    void Start() {
        
    }
    
    void Update() {
        if (joystick.Horizontal >= joystickOffset) {
            horizontalMove = runSpeed;
            animator.SetFloat(speedHash, Mathf.Abs(horizontalMove));
        }
        else if (joystick.Horizontal <= -joystickOffset) {
            horizontalMove = -runSpeed;
            animator.SetFloat(speedHash, Mathf.Abs(horizontalMove));
        }
        else {
            horizontalMove = 0;
            //animator.SetFloat(speed, Mathf.Abs(horizontalMove));
        }

        float jumpCrouchCheck = joystick.Vertical;

        if (jumpCrouchCheck >= joystickJumpOffset && canJump) {
            jump = true;
            animator.SetBool(isJumpingHash, jump);
//            StartCoroutine(ReJumpDelay());

        }
        

        if (jumpCrouchCheck <= -joystickCrouchOffset) {
            crouch = true;
            boxCollider.enabled = false;
               animator.SetBool(isCrouchingHash, crouch);
            
        }
        else {
            if (Physics2D.OverlapCircle(ceilingCheck.transform.position,0.2f, groundLayer)) {
                crouch = true;
                boxCollider.enabled = false;
                 animator.SetBool(isCrouchingHash, crouch);
            }
            else {
                crouch = false;
                boxCollider.enabled = true;
                 animator.SetBool(isCrouchingHash, crouch);
            }
            
        }
    }
    
    private void FixedUpdate() {
        Controller2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
    
    public void OnLanding() {
        animator.SetBool(isJumpingHash, false);
        
        
    }

    public void OnCrouching(bool crouching) {
       
       animator.SetBool(isCrouchingHash, crouching);
        animator.SetFloat(speedHash, Mathf.Abs(horizontalMove));
    }

    
//    IEnumerator ReJumpDelay() {
//        
//        yield return new WaitForSeconds(nextJumpDelay);
//        canJump = false;
//        yield return  new WaitForSeconds(nextJumpDelay);
//        canJump = true;
//    }

//Starts stab attack
    public void Stab() {

        Debug.Log("stab");
        if (!crouch) {
            animator.SetTrigger(stabHash);
            RaycastHit2D hit = Physics2D.Linecast(stabStart.transform.position, stabEnd.transform.position, enemyLayer);

            if (hit.collider != null) {
                EnemyHit(hit);
            }
        }
        else if (crouch) {
            animator.SetTrigger(stabHash);
            RaycastHit2D hit = Physics2D.Linecast(crouchStabStart.transform.position, crouchStabEnd.transform.position, enemyLayer);

            if (hit.collider != null) {
                EnemyHit(hit); 
            }
        }
        
    }

    //Start swing attack
    public void upSwing() {
        
        Debug.Log("upSwing");
        animator.SetTrigger(swingUpHash);
        for (int i = 0; i < swingUpList.Count-1; i++) {
            RaycastHit2D hit = Physics2D.Linecast(swingUpList[i].position, swingUpList[i + 1].position, enemyLayer);
            
            if (hit.collider != null) {
              
                EnemyHit(hit);
                break;
            }
        }
    }
    
    public void downSwing() {
        
        Debug.Log("downswing");
        animator.SetTrigger(swingDownHash);
        
        for (int i = 0; i < swingDownList.Count-1; i++) {
            RaycastHit2D hit = Physics2D.Linecast(swingDownList[i].position, swingDownList[i + 1].position, enemyLayer);
            
            if (hit.collider != null) {
              
                EnemyHit(hit);
                break;
            }
        }
    }
    
    
    
//
//    private void EnemyHit(RaycastHit2D hit) {
//        hit.collider.gameObject.GetComponent<EnemyController>().GotHit();
   
   
    private void EnemyHit(RaycastHit2D hit) {
        hit.collider.gameObject.GetComponent<EnemyController>().GotHit();
    }
    
    public void ShieldUp() {
        if (canUseShield) {
            animator.SetTrigger(shieldUpHash);
            shieldActive = true;
            StartCoroutine(SecondShieldDelay());
        }
        
    }

    public void ShootBow(float angle) {
        if (bowActive) {
            animator.SetTrigger(shootForwardHash);
        }
    }

    public void SwichWeapon() {
        bowActive = !bowActive;
        
        animator.SetBool(bowHash, bowActive);
        
    }


    IEnumerator SecondShieldDelay() {
        canUseShield = false;
        yield return new WaitForSeconds(shieldActiveTime);
        shieldActive = false;
        yield return new WaitForSeconds(shieldWaitTime);
        canUseShield = true;

    }
    
    
   
}
