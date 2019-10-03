using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
public class PlayerController : MonoBehaviour
{
    public CharacterController2D Controller2D;
    public Joystick joystick;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public GameObject ceilingCheck;
    public GameObject arrowPrefab;
    
    
    //Points for sword stab linecast
    public GameObject stabStart;
    public GameObject stabEnd;

    public GameObject crouchStabStart;
    public GameObject crouchStabEnd;
    
    //points for arrow
    public Transform arrowStartPoint;
    
    
    //List of points for sword swing lineCast
    public List<Transform> swingDownList = new List<Transform>();
    public List<Transform> swingUpList = new List<Transform>();
   
    
    //Animator Condition Hash Strings....
    //movement
    private int speedHash = Animator.StringToHash("speed");
    private int isCrouchingHash = Animator.StringToHash("isCrouching");
    private int isJumpingHash = Animator.StringToHash("isJumping");
    //Sword and Shield
    private int stabHash = Animator.StringToHash("stab");
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
    private float nextJumpDelay = 1.5f;    //set delay between jumps
    private float horizontalMove = 0;        
    private float runSpeed = 15f;

    private bool jump = false;
    private bool crouch = false;
    private bool canUseShield = true;
    
    //weapon settings
    [SerializeField]
    private bool shieldActive = false;
    private float shieldActiveTime = 0.6f;
    private float shieldWaitTime = 1f;
    private bool bowActive = false;
    private int arrowsInQuiver = 5;
    private float arrowMaxVelocity = 6f;
    private float weaponDamage = 1; 
    
    //health settings
    [SerializeField]
    private float health = 100f;
    private int lives;
    
    
    
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
            animator.SetFloat(speedHash, Mathf.Abs(horizontalMove));
        }

        float jumpCrouchCheck = joystick.Vertical;

        if (jumpCrouchCheck >= joystickJumpOffset && canJump) {
            jump = true;
            animator.SetBool(isJumpingHash, jump);
            StartCoroutine(ReJumpDelay());

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

    //prevents instant rejump upon landing
    IEnumerator ReJumpDelay() {
        
        //yield return new WaitForSeconds(nextJumpDelay);
        canJump = false;
        yield return  new WaitForSeconds(nextJumpDelay);
        canJump = true;
    }

    //Starts stab attack
    public void Stab() {

        if (!bowActive) {
         
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
    }

    //Start swing attack
    public void UpSwing() {

        if (!bowActive) {
            
            animator.SetTrigger(swingUpHash);
            for (int i = 0; i < swingUpList.Count - 1; i++) {
                RaycastHit2D hit = Physics2D.Linecast(swingUpList[i].position, swingUpList[i + 1].position, enemyLayer);

                if (hit.collider != null) {

                    EnemyHit(hit);
                    break;
                }
            }
        }
    }
    
    public void DownSwing() {


        if (!bowActive) {

            animator.SetTrigger(swingDownHash);

            for (int i = 0; i < swingDownList.Count - 1; i++) {
                RaycastHit2D hit =
                    Physics2D.Linecast(swingDownList[i].position, swingDownList[i + 1].position, enemyLayer);

                if (hit.collider != null) {

                    EnemyHit(hit);
                    break;
                }
            }
        }
    }
  
   
    
    public void ShieldUp() {
        if (canUseShield && !bowActive) {
            animator.SetTrigger(shieldUpHash);
            shieldActive = true;
            StartCoroutine(SecondShieldDelay());
        }
        
    }

    public void ShootBow(float speedX,float speedY) {

        
        if (speedX >= arrowMaxVelocity) {
            speedX = arrowMaxVelocity;
        }
        
        if (bowActive && !crouch && arrowsInQuiver > 0) {
            arrowsInQuiver--;
            animator.SetTrigger(shootForwardHash);
           GameObject arrow = Instantiate(arrowPrefab, arrowStartPoint.position, arrowStartPoint.transform.rotation);
           //arrow.GetComponent<Rigidbody2D>().velocity = transform.right * -10f;
           arrow.GetComponent<Rigidbody2D>().velocity = new Vector2( speedX * 2 * transform.right.x , -speedY * 2);
           // arrow.transform.position = arrowStartPoint.position;

        }
    }
    

    public void SwitchWeapon() {
        if (!crouch) {
            bowActive = !bowActive;
            animator.SetBool(bowHash, bowActive);
        }
        
    }


    IEnumerator SecondShieldDelay() {
        canUseShield = false;
        yield return new WaitForSeconds(shieldActiveTime);
        shieldActive = false;
        yield return new WaitForSeconds(shieldWaitTime);
        canUseShield = true;

    }
    
    //Registers incoming damage
    public void TakeDamage(float damage) {  //TODO make shield active dependent on direction facing, ie not active if hit in back
        if (!shieldActive) {
            health -= damage;
        }
        
        if (health <= 0) {
            Debug.Log("DED");
            health = 100f; //TODO remove godmode and add death animation + respawn
        }
    }
     private void EnemyHit(RaycastHit2D hit) {
            hit.collider.gameObject.GetComponent<EnemyController>().GotHit(weaponDamage);
     }

     public void PickUpArrows(int arrowsPickedUp) {
         arrowsInQuiver += arrowsPickedUp;
     }
     
     
     public int ArrowsRemaining() {
         return arrowsInQuiver;
     }
}
