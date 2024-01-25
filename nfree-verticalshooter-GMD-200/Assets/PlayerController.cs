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

    // input variables
    private Vector2 moveInput;
    private bool attackInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        attackInput = Input.GetKeyDown(KeyCode.J);

        moveInput.Normalize();

        if (moveInput.y != 0 || moveInput.x != 0) {
            playerAnimator.SetBool("idle", false);
        } else {
            playerAnimator.SetBool("idle", true);
        }

        if(moveInput.x != 0)
        {
            playerAnimator.SetInteger("direction", 2);
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
                playerAnimator.SetInteger("direction", 0);
            }
            else
            {
                playerAnimator.SetInteger("direction", 1);
            }
        }



        rb.velocity = moveInput * moveSpeed;
    }
}
