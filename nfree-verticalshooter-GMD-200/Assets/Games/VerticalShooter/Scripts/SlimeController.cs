using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] GameObject coinPrefab;
    System.Random RAND = new System.Random();

    private bool jumping = false;
    private Vector3 jumpPoint;
    private float zIndex;
    public float heath = 3;
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

        int dropChance = RAND.Next(10);
        // spawn exp orb
        if (dropChance > 4)
        {
            if (dropChance > 8)
            {
                Vector3 coinPos = slimeTransform.position;
                GameObject coin = Instantiate(coinPrefab, coinPos, Quaternion.identity);
            }
            else
            {
                Vector3 expPos = slimeTransform.position;
                GameObject exp = Instantiate(expOrb, expPos, Quaternion.identity);
                exp.GetComponent<ExperienceController>().experience = 1;
            }
        }


        // destroy collisions (rigidbody and box collider)
        Destroy(slimeRB);
        Destroy(slimeBC);

        //Put slimeguts under other stuff
        Vector3 selfPos = slimeTransform.position;
        selfPos.z = -1.4f;
        slimeTransform.position = selfPos;
        Invoke("DestroySelf", 40f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
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
            ProjectileController bulletController = collisionGameObject.GetComponent<ProjectileController>();
            Transform bulletTransformer = collisionGameObject.GetComponent<Transform>();
            if (bulletController.knockback > 0)
            {
                slimeTransform.Translate(bulletTransformer.right * bulletController.knockback / 10f);
            }
            float damage = collisionGameObject.GetComponent<ProjectileController>().damage;
            heath -= damage;

            if (bulletController.penetration > 0)
            {
                bulletController.penetration--;
                if(bulletController.penetration <= 0)
                {
                    Destroy(collision.gameObject);
                }
            } else
            { 
                Destroy(collision.gameObject);
            }
        }
    }
}
