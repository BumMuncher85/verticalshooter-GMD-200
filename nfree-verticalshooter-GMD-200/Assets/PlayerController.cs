using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] public Rigidbody2D rb;

    [Header("Animation")]
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public SpriteRenderer playerSpriteRender;

    [Header("Weapon shit")]
    [SerializeField] public GameObject currentWeapon;
    [SerializeField] public int weaponCooldown = 60;
    [SerializeField] public WeaponScript weaponWeaponScript;

    // input variables
    public Vector2 moveInput;
    private bool attackPressed;
    private bool attackReleased;
    private bool shooting = false;
    private int shootingTime;
    private int playerEXP;

    //animation variables
    public int animDirection;
    public bool animIdle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        attackPressed = Input.GetMouseButtonDown(0);
        attackReleased = Input.GetMouseButtonUp(0); ;
        moveInput.Normalize();

        if (moveInput.y != 0 || moveInput.x != 0) {
            playerAnimator.SetBool("idle", false);
        } else {
            playerAnimator.SetBool("idle", true);
        }

        if(moveInput.x != 0)
        {
            animDirection = 2;
            if(moveInput.x < 0) {
                playerSpriteRender.flipX = true;
            } else {
                playerSpriteRender.flipX = false;
            }
        }

        if(moveInput.y != 0)
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

        if(attackPressed == true)
        {
            shooting = true;
        }
        if(attackReleased == true)
        {
            shooting = false;
        }

        if(shooting == true)
        {
            shootingTime += 1;
            if(shootingTime >= weaponCooldown)
            {
                weaponWeaponScript.shoot = true;
                shootingTime = 0;
            }
        }
    }
     public void IncreaseEXP(int amount)
    {
        Debug.Log("INCREASED EXP: " + amount);
        playerEXP += amount;
    }
}
