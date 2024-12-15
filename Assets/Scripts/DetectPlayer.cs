using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public bool Detected;
    public Collider2D Collider;
    // Start is called before the first frame update
    void Start()
    {
        Detected = false;
        Collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            Detected = true;
        };
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Detected = false;
        };
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
