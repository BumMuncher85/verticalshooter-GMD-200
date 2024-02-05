using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] Transform coinTransform;

    public int magnetRadius;
    public float pickupRadius;
    public float magnetSpeed;
    private float zIndex;
    public int amount = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        zIndex = coinTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pPos = player.transform.position;
        Vector3 ePos = coinTransform.position;
        float dist = Vector2.Distance(pPos, ePos);

        if (dist < magnetRadius)
        {
            if (dist < pickupRadius)
            {
                player.GetComponent<PlayerController>().IncreaseCash(amount);
                Destroy(gameObject);
            }
            else
            {
                Vector3 position = Vector3.MoveTowards(ePos, pPos, magnetSpeed);
                position.z = zIndex;
                coinTransform.position = position;
            }
        }


    }
}
