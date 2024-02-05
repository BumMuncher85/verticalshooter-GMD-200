using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 objectPos;
    [SerializeField] public int weaponAngle;
    [SerializeField] public Transform weaponTransform;
    [SerializeField] public GameObject player;
    [SerializeField] public PlayerController playerController;
    [SerializeField] public SpriteRenderer weaponSpriteRenderer;
    [SerializeField] public Sprite[] weaponSprites;
    [SerializeField] public GameObject[] projectiles;
    [SerializeField] public GameObject firePoint;
    [SerializeField] public PrefrenceHandler prefrenceHandler;
    public string currentWeapon;
    public int weaponID;
    private Vector3 weaponOffset;
    private Vector3 weaponFirePosition;

    public bool shoot = false;

    System.Random RAND = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        // setup the default weapon
        switch (currentWeapon)
        {
            case "Pistol":
                weaponID = 0;
                weaponTransform.localScale = new Vector3(1, 1, 1);
                weaponOffset = new Vector3(0, -0.2f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.417f, 0.167f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
            case "Shotgun":
                weaponID = 1;
                weaponTransform.localScale = new Vector3(2, 2, 2);
                weaponOffset = new Vector3(0, -0.2f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.247f, 0.036f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
            case "SMG":
                weaponID = 2;
                weaponTransform.localScale = new Vector3(2, 2, 2);
                weaponOffset = new Vector3(0, -0.2f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.256f, 0.052f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
            case "AK-47":
                weaponID = 3;
                weaponTransform.localScale = new Vector3(4, 4, 4);
                weaponOffset = new Vector3(0, -0.1f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.1073f, -0.0055f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
        }
        weaponSpriteRenderer.sprite = weaponSprites[weaponID];
    }

    public void SwitchWeapon(int weapon)
    {
        // change weapon open input (z x c v)
        switch (weapon)
        {
            case 0:
                weaponID = 0;
                weaponTransform.localScale = new Vector3(1, 1, 1);
                weaponOffset = new Vector3(0, -0.2f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.417f, 0.167f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
            case 1:
                weaponID = 1;
                weaponTransform.localScale = new Vector3(2, 2, 2);
                weaponOffset = new Vector3(0, -0.2f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.247f, 0.036f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
            case 2:
                weaponID = 2;
                weaponTransform.localScale = new Vector3(2, 2, 2);
                weaponOffset = new Vector3(0, -0.2f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.256f, 0.052f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
            case 3:
                weaponID = 3;
                weaponTransform.localScale = new Vector3(4, 4, 4);
                weaponOffset = new Vector3(0, -0.1f, -0.01f);
                weaponTransform.localPosition = weaponOffset;
                weaponFirePosition = new Vector3(0.1073f, -0.0055f, 0);
                firePoint.transform.localPosition = weaponFirePosition;
                break;
        }
        weaponSpriteRenderer.sprite = weaponSprites[weaponID];
    }

    // Update is called once per frame
    void Update()
    { 
        // align gun to mouse
        Vector3 playerPos = player.transform.position;
        playerPos += weaponOffset;
        weaponTransform.position = playerPos;
        mousePos = Input.mousePosition;
        mousePos.z = weaponTransform.position.z;
        objectPos = Camera.main.WorldToScreenPoint(weaponTransform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float weaponAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(0, 0, weaponAngle);
        if(weaponAngle > 90 || weaponAngle < -90)
        {
            weaponSpriteRenderer.flipY = true;
            Vector3 pointPos = weaponFirePosition;
            pointPos.y = Mathf.Abs(pointPos.y) * -1;
            weaponFirePosition = pointPos;
            firePoint.transform.localPosition = weaponFirePosition;
        } else
        {
            weaponSpriteRenderer.flipY = false;
            Vector3 pointPos = weaponFirePosition;
            pointPos.y = Mathf.Abs(pointPos.y);
            weaponFirePosition = pointPos;
            firePoint.transform.localPosition = weaponFirePosition;
        }

        // turn the player if weapon is pointing hard in a direction
        if (playerController.moveInput.x == 0 && playerController.moveInput.y == 0) {
            if (weaponAngle > 170 || weaponAngle < -170)
            {
                playerController.animDirection = 2;
                playerController.playerSpriteRender.flipX = true;
            }
            if (weaponAngle < 10 && weaponAngle > -10)
            {
                playerController.animDirection = 2;
                playerController.playerSpriteRender.flipX = false;
            }
            if(weaponAngle < 100 && weaponAngle > 80)
            {
                playerController.animDirection = 1;
            }
            if (weaponAngle > -100 && weaponAngle < -80)
            {
                playerController.animDirection = 0;
            }
        }

        // put gun infront or behind player
        if (playerController.animDirection == 1)
        {
            Vector3 _curPos = weaponTransform.position;
            _curPos.z = player.transform.position.z + 0.01f;
            weaponTransform.position = _curPos;
        } else if (playerController.animDirection == 0)
        {
            Vector3 _curPos = weaponTransform.position;
            _curPos.z = player.transform.position.z - 0.01f;
            weaponTransform.position = _curPos;
        }

        if(shoot == true)
        {
            int[] weaponStats = prefrenceHandler.getGunStats(weaponID);
            switch (weaponID) {
                case 0: // pistol
                    float pPower = weaponStats[0] * 0.2f + 5f;
                    float pKnockback = weaponStats[1] * 0.5f;
                    GameObject pBullet = Instantiate(projectiles[0], firePoint.transform.position, Quaternion.Euler(0, 0, weaponAngle));
                    pBullet.GetComponent<ProjectileController>().damage = pPower;
                    pBullet.GetComponent<ProjectileController>().knockback = pKnockback;
                    break;
                case 1: // shotgun
                    float shPower = weaponStats[0] * 0.5f;
                    int shBullets = weaponStats[1]+2;
                    int shSpread = 39 - weaponStats[2]*3;
                    for(int i = 0; i < shBullets; i++)
                    {
                        GameObject shBullet = Instantiate(projectiles[1], firePoint.transform.position, Quaternion.Euler(0, 0, weaponAngle + (RAND.Next(-shSpread, shSpread))));
                        shBullet.GetComponent<ProjectileController>().damage = shPower;
                    }
                    break;
                case 2: // smg
                    float smgPower = weaponStats[0] * 0.3f;
                    int smgSpread = 33 - weaponStats[2]*3;
                    GameObject smgBullet = Instantiate(projectiles[0], firePoint.transform.position, Quaternion.Euler(0, 0, weaponAngle + (RAND.Next(-smgSpread, smgSpread))));
                    smgBullet.GetComponent<ProjectileController>().damage = smgPower;
                    break;
                case 3: // ak
                    float akPower = weaponStats[0] * 1f;
                    float  akKnockback = weaponStats[2] * 0.5f;
                    int akPenetration = weaponStats[3];
                    GameObject akBullet = Instantiate(projectiles[0], firePoint.transform.position, Quaternion.Euler(0, 0, weaponAngle));
                    akBullet.GetComponent<ProjectileController>().damage = akPower;
                    akBullet.GetComponent<ProjectileController>().knockback = akKnockback;
                    akBullet.GetComponent<ProjectileController>().penetration = akPenetration; 
                    break;
            }
            shoot = false;
        }
    }
}
