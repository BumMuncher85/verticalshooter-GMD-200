using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public Animator slimeAnimator;
    [SerializeField] public Transform slimeTransform;
    [SerializeField] public Rigidbody2D slimeRB;
    [SerializeField] public BoxCollider2D slimeBC;
    [SerializeField] public SpriteRenderer slimeSpriteRenderer;
    [SerializeField] public float jumpDistance = 0.1f;
    [SerializeField] GameObject expOrb;

    private bool jumping = false;
    private Vector3 jumpPoint;
    private float zIndex;
    private float heath = 3;
    public bool dead = false;
    private float despawnDistance = 20;
    public bool despawned = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Idle();
        zIndex = slimeTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (heath > 0 && dead == false)
        {
            if (jumping)
            {
                Vector2 position = Vector2.MoveTowards(slimeTransform.position, jumpPoint, jumpDistance);
                slimeRB.MovePosition(position);
            }
            else
            {
                slimeRB.velocity = new Vector2(0, 0);
            }
            if (player.transform.position.x < slimeTransform.position.x)
            {
                slimeSpriteRenderer.flipX = true;
            }
            else
            {
                slimeSpriteRenderer.flipX = false;
            }
        } else
        {
            if(dead == false)
            {
                Death();
            }
            
        }
        if (slimeTransform.position.y > player.transform.position.y + despawnDistance || slimeTransform.position.y < player.transform.position.y - despawnDistance)
        {
            Despawn();
        }
    }

    void Jump()
    {
        if (!dead)
        {
            jumping = true;
            slimeAnimator.SetInteger("state", 1);
            Invoke("Idle", 0.6f);
            jumpPoint = player.transform.position;
            jumpPoint.z = zIndex;
        }
    }
    void Despawn()
    {
        despawned = true;
    }
    void Death()
    {
        // declare death
        dead = true;
        // stop rb physics
        slimeRB.velocity = new Vector2(0, 0);
        // play death animation
        slimeAnimator.SetInteger("state", 2);

        // spawn exp orb
        Vector3 expPos = slimeTransform.position;
        GameObject exp = Instantiate(expOrb, expPos, Quaternion.identity);
        exp.GetComponent<ExperienceController>().experience = 1;


        // destroy collisions (rigidbody and box collider)
        Destroy(slimeRB);
        Destroy(slimeBC);

        //Put slimeguts under other stuff
        Vector3 selfPos = slimeTransform.position;
        selfPos.z = -1.4f;
        slimeTransform.position = selfPos;
    }

    void Idle()
    {
        if(!dead)
        {
            jumping = false;
            slimeAnimator.SetInteger("state", 0);
            Invoke("Jump", 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        
        if (collisionGameObject.CompareTag("Projectile"))
        {
            Transform bulletTransformer = collisionGameObject.GetComponent<Transform>();
            slimeTransform.Translate(bulletTransformer.right * 0.1f);
            float damage = collisionGameObject.GetComponent<ProjectileController>().damage;
            heath -= damage;
            Destroy(collision.gameObject);
        }
    }
}
