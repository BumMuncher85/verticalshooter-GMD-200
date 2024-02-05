using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Transform projectileTransform;
    public float projectileSpeed;
    public float damage;
    public float knockback;
    public float penetration;
    private float zIndex;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        zIndex = projectileTransform.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 1) {
            Destroy(gameObject);
        }
        projectileTransform.Translate((Vector3.right * projectileSpeed * Time.deltaTime));
    }
}
