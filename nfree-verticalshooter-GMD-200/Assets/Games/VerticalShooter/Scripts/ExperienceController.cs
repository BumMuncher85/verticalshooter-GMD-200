using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] Transform expTransform;

    public int magnetRadius;
    public float pickupRadius;
    public float magnetSpeed;
    public int experience = 1;
    private float zIndex;
    // Start is called before the first frame update
    void Start()
    {
        player =  GameObject.Find("Player");
        zIndex = expTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pPos = player.transform.position;
        Vector3 ePos = expTransform.position;
        float dist = Vector2.Distance(pPos, ePos);

        if(dist < magnetRadius)
        {
            if (dist < pickupRadius)
            {
                player.GetComponent<PlayerController>().IncreaseEXP(experience);
                Destroy(gameObject);
            }
            else
            {
                Vector3 position = Vector3.MoveTowards(ePos, pPos, magnetSpeed);
                position.z = zIndex;
                expTransform.position = position;
            }
        }
    }
}
