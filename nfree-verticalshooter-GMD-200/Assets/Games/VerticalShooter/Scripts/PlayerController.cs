using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] public Rigidbody2D rb;

    [Header("Animation")]
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public SpriteRenderer playerSpriteRender;
    [SerializeField] SpriteRenderer weaponSpriteRenderer;

    [Header("Weapon shit")]
    [SerializeField] public GameObject currentWeapon;
    [SerializeField] public WeaponScript weaponWeaponScript;

    [Header("UI Stuf")]
    [SerializeField] public RawImage experienceBar;
    [SerializeField] RawImage healthBar;
    public TextMeshProUGUI coinTextContainer;
    public Text coinText;
    [SerializeField] public GameObject LevelUpContainer;
    [SerializeField] MenuHandler menuHandler;

    [Header("Prefrences")]
    [SerializeField] public PrefrenceHandler prefrenceHandler;

    [Header("Audio shenenigans")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip pistolShoot;
    [SerializeField] public AudioClip shotgunShoot;
    [SerializeField] public AudioClip smgShoot;
    [SerializeField] public AudioClip akShoot;
    [SerializeField] public AudioClip oucheyWawa;

    // input variables
    public Vector2 moveInput;
    private bool attackPressed;
    private bool attackReleased;
    private bool pistolPressed;
    private bool shotgunPressed;
    private bool smgPressed;
    private bool akPressed;


    private bool shooting = false;
    private int shootingTime;
    private double playerEXP = 0;
    public double expToNextLevel = 10;
    private int pistolCooldown = 200;
    private int shotgunCooldown = 300;
    private int smgCooldown = 100;
    private int akCooldown = 300;

    //animation variables
    public int animDirection;
    public bool animIdle;

    //health
    public double curPlayerHealth = 10;
    public double maxPlayerHealth = 10;

    private bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        experienceBar.rectTransform.localScale = new Vector3(0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuHandler.menuOpen == false && menuHandler.levelUpOpen == false)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            attackPressed = Input.GetMouseButtonDown(0);
            attackReleased = Input.GetMouseButtonUp(0);
            pistolPressed = Input.GetKeyDown(KeyCode.Alpha1);
            shotgunPressed = Input.GetKeyDown(KeyCode.Alpha2);
            smgPressed = Input.GetKeyDown(KeyCode.Alpha3);
            akPressed = Input.GetKeyDown(KeyCode.Alpha4);
            moveInput.Normalize();

            if(pistolPressed)
            {
                weaponWeaponScript.SwitchWeapon(0);
            }
            if (shotgunPressed && PlayerPrefs.GetInt("shotgunUnlocked") == 1)
            {
                weaponWeaponScript.SwitchWeapon(1);
            }
            if (smgPressed && PlayerPrefs.GetInt("smgUnlocked") == 1)
            {
                weaponWeaponScript.SwitchWeapon(2);
            }
            if (akPressed && PlayerPrefs.GetInt("akUnlocked") == 1)
                {
                weaponWeaponScript.SwitchWeapon(3);
            }

            if (moveInput.y != 0 || moveInput.x != 0)
            {
                playerAnimator.SetBool("idle", false);
            }
            else
            {
                playerAnimator.SetBool("idle", true);
            }

            if (moveInput.x != 0)
            {
                animDirection = 2;
                if (moveInput.x < 0)
                {
                    playerSpriteRender.flipX = true;
                }
                else
                {
                    playerSpriteRender.flipX = false;
                }
            }

            if (moveInput.y != 0)
            {
                if (moveInput.y < 0)
                {
                    animDirection = 0;
                }
                else
                {
                    animDirection = 1;
                }
            }
            playerAnimator.SetInteger("direction", animDirection);

            rb.velocity = moveInput * moveSpeed;

            if (attackPressed == true)
            {
                shooting = true;
            }
            if (attackReleased == true)
            {
                shooting = false;
            }

            int weaponID = weaponWeaponScript.weaponID;
            shootingTime++;

            switch (weaponID)
            {
                case 0:
                    if (attackPressed == true)
                    {
                        weaponWeaponScript.shoot = true;
                        audioSource.PlayOneShot(pistolShoot, 0.5f);
                        shootingTime = 0;
                    }
                    else if (shooting == true)
                    {
                        if (shootingTime >= pistolCooldown)
                        {
                            weaponWeaponScript.shoot = true;
                            shootingTime = 0;
                            audioSource.PlayOneShot(pistolShoot, 0.5f);
                        }
                    }
                    break;
                case 1:
                    if (shooting == true)
                    {
                        if (shootingTime >= shotgunCooldown)
                        {
                            weaponWeaponScript.shoot = true;
                            shootingTime = 0;
                            audioSource.PlayOneShot(shotgunShoot, 0.5f);
                        }
                    }
                    break;
                case 2:
                    if (shooting == true)
                    {
                        var pointCooldown = smgCooldown - (PlayerPrefs.GetInt("smgFireRate")*20);
                        if (shootingTime >= pointCooldown)
                        {
                            weaponWeaponScript.shoot = true;
                            shootingTime = 0;
                            audioSource.PlayOneShot(smgShoot, 0.5f);
                        }
                    }
                    break;
                case 3:
                    if (shooting == true)
                    {
                        var pointCooldown = akCooldown - (PlayerPrefs.GetInt("akFireRate") * 15);
                        if (shootingTime >= pointCooldown)
                        {
                            weaponWeaponScript.shoot = true;
                            shootingTime = 0;
                            audioSource.PlayOneShot(akShoot, 1f);
                        }
                    }
                    break;
            }
        }
    }
     public void IncreaseEXP(int amount)
    {
        playerEXP += amount;
        double percentToLevelUp = playerEXP / expToNextLevel;
        Vector3 expBarScale = new Vector3((float)percentToLevelUp, 1, 1);
        experienceBar.rectTransform.localScale = expBarScale;

        if(playerEXP >= expToNextLevel)
        {
            playerEXP = 0;
            expToNextLevel += 5;
            curPlayerHealth += 2;
            if (curPlayerHealth > 10) { curPlayerHealth = 10; }

            percentToLevelUp = playerEXP / expToNextLevel;
            expBarScale = new Vector3((float)percentToLevelUp, 1, 1);
            experienceBar.rectTransform.localScale = expBarScale;

            int curPoints = PlayerPrefs.GetInt("PLAYER_POINTS");
            PlayerPrefs.SetInt("PLAYER_POINTS", curPoints += 2);
            LevelUpContainer.SetActive(true);
            menuHandler.LevelUp();
            Time.timeScale = 0;
        }
    }
    public void IncreaseCash(int amount)
    {
        var curCash = PlayerPrefs.GetInt("PLAYER_COINS");
        PlayerPrefs.SetInt("PLAYER_COINS", curCash + amount);
        string cashString = (curCash + amount).ToString();

        coinText.text = cashString;
    }
    
    private void Invincibility()
    {
        invincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.CompareTag("Enemy") && invincible == false) 
        {
            
            curPlayerHealth -= 2;
            Debug.Log("I HAVE BEEN ATTACKED: " + curPlayerHealth);
            double healthPercent = curPlayerHealth / maxPlayerHealth;
            Vector3 healthBarScale = new Vector3((float)healthPercent, 1, 1);
            healthBar.rectTransform.localScale = healthBarScale;
            audioSource.PlayOneShot(oucheyWawa, 10f); 
            invincible = true;
            Invoke("Invincibility", 2.124f);
            InvincibilityFlickerOff();
            Invoke("InvincibilityFlickerOn", 0.25f);
            Invoke("InvincibilityFlickerOff", 0.5f);
            Invoke("InvincibilityFlickerOn", 0.75f);
            Invoke("InvincibilityFlickerOff", 1f);
            Invoke("InvincibilityFlickerOn", 1.125f);
            Invoke("InvincibilityFlickerOff", 1.250f);
            Invoke("InvincibilityFlickerOn", 1.375f);
            Invoke("InvincibilityFlickerOff", 1.500f);
            Invoke("InvincibilityFlickerOn", 1.625f);
            Invoke("InvincibilityFlickerOff", 1.750f);
            Invoke("InvincibilityFlickerOn", 1.875f);
            Invoke("InvincibilityFlickerOff", 2f);
            Invoke("InvincibilityFlickerOn", 2.125f);
        }
        if(curPlayerHealth <= 0)
        {
            menuHandler.EndGame();
        }
    }

    private void InvincibilityFlickerOn()
    {
        playerSpriteRender.enabled = true;
        weaponSpriteRenderer.enabled = true;
    }

    private void InvincibilityFlickerOff()
    {
        playerSpriteRender.enabled = false;
        weaponSpriteRenderer.enabled = false;
    }
}
