using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
public class PlayerController : MonoBehaviour
{
    public CharacterController2D Controller2D;
    public Joystick joystick;
    public GameObject joystickGameObject;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public GameObject ceilingCheck;
    public GameObject arrowPrefab;
    public GameObject berserkerParticles;
    public HealthBarController healthBarController;
    
    
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
//    private int swingUpHash = Animator.StringToHash("swingUp");
//    private int swingDownHash = Animator.StringToHash("swingDown");
    private int shieldUpHash = Animator.StringToHash("shieldUp");
    //bow
    private int bowHash = Animator.StringToHash("bow");
    private int shootForwardHash = Animator.StringToHash("shootForward");
    //death
    private int deadHash = Animator.StringToHash("dead");


    //movement settings
    private float joystickOffset = 0.2f;  //Offset until player moves
    private float joystickJumpOffset = 0.5f;  //Offset until player jumps/crouches
    private  float joystickCrouchOffset = 0.5f;
    private bool canJump = true;          //prevents instant rejump on landing  
    private float nextJumpDelay = 1.5f;    //set delay between jumps
    private float horizontalMove = 0;        
    private float runSpeed = 15f;

    private bool jump = false;
    private bool crouch = false;
    private bool canUseShield = true;
    
    //weapon buttons
    public GameObject shieldButton;
    public GameObject swordButton;
    public GameObject bowButton;


    //weapon settings
    [SerializeField]
    private bool shieldActive = false;
    private float shieldActiveTime = 1.2f;
    private float shieldWaitTime = 0.2f;
    private bool bowActive = false;
    private bool canStab = true;
    private float stabDelay = 0.5f;
    private float secondStabDelay = 1f;
    private int arrowsInQuiver = 5;
    private int fireArrowsInQuiver = 0;
    private float arrowMaxVelocity = 6f;

    private int hitForce = 150;
    
    //powerups
    private float berserkerDuration = 30;
    
    //sword damage
    private const float normalDamage = 2;
    private float weaponDamage = 2;
    private float berserkerDamage = 4;
    
    //health settings
    [SerializeField]
    private float health = 100f;
    private float maxHealth = 100f;
    private int lives;
    private bool dead = false;
    
    //enemy tag Strings
//    private string blueKnight = "BlueKnight";
//    private string fireMage = "FireMage"; 

    private static PlayerController _instance;
    
    void Start() {
        health = maxHealth;
        berserkerParticles.SetActive(false);
        ToggleWeaponButtons();
        DontDestroyOnLoad(gameObject);
        //SetHealthBar();
    }

    public static PlayerController Instance { get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("PlayerKnight")).GetComponent<PlayerController>();
            }

            return _instance;
        }

    }
    
    void OnEnable()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
      
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
        
    private void Awake()
    {

        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        transform.position = new Vector2(-8f, 0f); //TODO: eventually make this different for different levels, at least make a V2 variable.
        
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
        if (!dead) {
            Controller2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }
        
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
    
    //set health bar
    private void SetHealthBar() {
        
             healthBarController.SetHealthBar(health / 100);
        
       
    }

    //Starts stab attack
    public void Stab() {

        if (!bowActive && ! dead && canStab) {
            canStab = false;

            StartCoroutine(StabWithDelay());
            StartCoroutine(SecondAttackDelay());
        }
    }

    IEnumerator StabWithDelay() {
        yield return new WaitForSeconds(stabDelay);
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

    //prevents spamming attack
    IEnumerator SecondAttackDelay() {
        yield return new WaitForSeconds(secondStabDelay);
        canStab = true;
    }

    //Start swing attack
//    public void UpSwing() {
//
//        if (!bowActive && !dead) {
//            
//            animator.SetTrigger(swingUpHash);
//            for (int i = 0; i < swingUpList.Count - 1; i++) {
//                RaycastHit2D hit = Physics2D.Linecast(swingUpList[i].position, swingUpList[i + 1].position, enemyLayer);
//
//                if (hit.collider != null) {
//
//                    EnemyHit(hit);
//                    break;
//                }
//            }
//        }
//    }
    
//    public void DownSwing() {
//
//
//        if (!bowActive && !dead) {
//
//            animator.SetTrigger(swingDownHash);
//
//            for (int i = 0; i < swingDownList.Count - 1; i++) {
//                RaycastHit2D hit =
//                    Physics2D.Linecast(swingDownList[i].position, swingDownList[i + 1].position, enemyLayer);
//
//                if (hit.collider != null) {
//
//                    EnemyHit(hit);
//                    break;
//                }
//            }
//        }
//    }
  
   
    
    public void ShieldUp() {
        if (canUseShield && !bowActive && !dead) {
            animator.SetTrigger(shieldUpHash);
            shieldActive = true;
            StartCoroutine(SecondShieldDelay());
        }

        if (health <= 0) {
            Respawn();   //TODO: remove this!!!!
        }
        
    }

    public void ShootBow(float speedX,float speedY) {

        
        if (speedX >= arrowMaxVelocity) {
            speedX = arrowMaxVelocity;
        }
        
        if (bowActive && !crouch && arrowsInQuiver > 0 && !dead && canStab) {
            canStab = false;
            StartCoroutine(SecondAttackDelay());
            animator.SetTrigger(shootForwardHash);
           GameObject arrow = Instantiate(arrowPrefab, arrowStartPoint.position, arrowStartPoint.transform.rotation);
           arrow.GetComponent<Rigidbody2D>().velocity = new Vector2( speedX * 2 * transform.right.x , -speedY * 2);
           arrowsInQuiver--;
           
           if (fireArrowsInQuiver > 0) {
               ArrowController arrowController = arrow.GetComponent<ArrowController>();
               arrowController.IncreaseDamage();
               arrowController.StartParticles();
               fireArrowsInQuiver--;
           }
           
        }
    }
    

    public void SwitchWeapon() {
        if (!crouch && !dead) {
            bowActive = !bowActive;
            animator.SetBool(bowHash, bowActive);
            ToggleWeaponButtons();
        }
        
    }

    private void ToggleWeaponButtons() {
        if (bowActive) {
            bowButton.SetActive(true);
            swordButton.SetActive(false);
            shieldButton.SetActive(false);
        }
        else {
            bowButton.SetActive(false);
            swordButton.SetActive(true);
            shieldButton.SetActive(true);
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
            if (health < 0) {
                health = 0;
            }
            SetHealthBar();
        }
        
        if (health <= 0) {
            Debug.Log("DED");
            Die();
            //health = 100f; //TODO remove godmode and add death animation + respawn
        }
    }

    public void Instakill() {
        health = 0;
        SetHealthBar();
        Die();
        Debug.Log("instakilled");
    }

    private void Die() {
        animator.SetBool(deadHash, true);
        dead = true;
    }
     private void EnemyHit(RaycastHit2D hit) {
          GameObject objHit = hit.collider.gameObject;
          string objTag = objHit.tag;
        
         switch (objTag) {
         case "BlueKnight":
             objHit.GetComponent<EnemyController>().GotHit(weaponDamage);
             objHit.GetComponent<Rigidbody2D>().AddForce(-transform.right * hitForce );
             
             break;
             
         case "FireMage":
             objHit.GetComponent<FireMageController>().GotHit(weaponDamage);
             objHit.GetComponent<Rigidbody2D>().AddForce(-transform.right * hitForce);
             break;
         
         case "Minotaur":
             objHit.GetComponent<MinotaurController>().GotHit(weaponDamage);
             objHit.GetComponent<Rigidbody2D>().AddForce(-transform.right * hitForce);
             break;
             
         }
         
         
            
     }

     public void PickUpArrows(int arrowsPickedUp) {
         arrowsInQuiver += arrowsPickedUp;
     }
     
     
     public int ArrowsRemaining() {
         return arrowsInQuiver;
     }

     public bool FireArrows() {
         return fireArrowsInQuiver > 0;
     }
     
     
     //power ups
     public void Berserker() {
         StartCoroutine(GoBerserk());
     }
     

     private IEnumerator GoBerserk() {
         weaponDamage = berserkerDamage;
         berserkerParticles.SetActive(true);
         yield return new WaitForSeconds(berserkerDuration);
         berserkerParticles.SetActive(false);
         weaponDamage = normalDamage;
     }

     public void GetHealth(float healthGiven) {
         health += healthGiven;
         if (health > maxHealth) {
             health = maxHealth;
         }
         SetHealthBar();
     }
     public void PickUpFireArrows(int fireArrowsPickedUp) {
         fireArrowsInQuiver += fireArrowsPickedUp;
         arrowsInQuiver += fireArrowsPickedUp;
     }

     private void Respawn() {
         animator.SetBool(deadHash, false);
         gameObject.transform.position = new Vector2(-7, -1);
         health = maxHealth;
         dead = false;
         SetHealthBar();
     }
     
}
