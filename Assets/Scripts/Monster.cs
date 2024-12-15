using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;
    public float speed = 3;
    public float maxDelayDamage = 1;
    public float enemyHealth = 3;
    public float idleDelay = 5;
    private float delayBtwnDamage;
    public GameObject Detector;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        delayBtwnDamage = maxDelayDamage;
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();

        if (delayBtwnDamage <= 0 && health != null)
        {
            health.TakeDamage(1);
            delayBtwnDamage = maxDelayDamage;
        }
    }
    void Idle()
    {

    }
    void FixedUpdate()
    {
        if (Detector.GetComponent<DetectPlayer>().Detected)
        {
            delayBtwnDamage -= Time.deltaTime;
            var targetPosition = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            rb.MovePosition(targetPosition);
        }
        
    }
}
