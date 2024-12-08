using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 30;
    public float lifetime = 2;
    public Vector2 damageRange = new Vector2 (10, 20);
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damage = Random.Range(damageRange.x, damageRange.y);
        // todo: damage to collision.gameObject
        print($"Dealt {damage} to {collision.gameObject.name}");
        Destroy(gameObject);
    }
}
