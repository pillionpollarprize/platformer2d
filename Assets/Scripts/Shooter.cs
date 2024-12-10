using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioClip shootSnd;
    public float cooldownTime = 1;

    private AudioSource audSrc;
    private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = cooldownTime;
        audSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            audSrc.pitch = Random.Range(0.95f, 1.1f);
            audSrc.PlayOneShot(shootSnd);
            var mousePos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;
            var direction = worldPos - transform.position;
            direction.Normalize();

            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = direction;
            cooldown = cooldownTime;
        }
    }
}
