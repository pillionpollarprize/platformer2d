using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float cooldownTime = 1;
    private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            var mousePos = Input.mousePosition;
            var worldPos = Camera.main.WorldToScreenPoint(mousePos);
            worldPos.z = 0;
            var direction = worldPos - transform.position;
            direction.Normalize();

            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = direction;
            cooldown = cooldownTime;
        }
    }
}
