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
    [SerializeField] public GameObject[] projectiles;
    [SerializeField] public GameObject firePoint;
    public string currentWeapon;

    public bool shoot = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.z -= 0.01f;
        playerPos.y -= 0.05f;
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
        } else
        {
            weaponSpriteRenderer.flipY = false;
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
            Instantiate(projectiles[0], firePoint.transform.position, Quaternion.Euler(0, 0, weaponAngle));
            shoot = false;
        }
    }
}
