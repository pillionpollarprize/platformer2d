using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 6;
    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite mptyHeart;

    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0) 
        {
            health = 0;
            if (gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        UpdateHearts();
    }
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if(health >= (i+1) * 2)
            {
                hearts[i].sprite = fullHeart;
            }
            else if(health == (i * 2) + 1)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = mptyHeart
                    ;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
