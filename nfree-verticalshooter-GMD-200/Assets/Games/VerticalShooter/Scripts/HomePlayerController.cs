using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HomePlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] public Rigidbody2D rb;

    [Header("Animation")]
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public SpriteRenderer playerSpriteRender;

    // input variables
    public Vector2 moveInput;
    private bool attackPressed;
    private bool attackReleased;

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
        moveInput.Normalize();

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
    }
}
