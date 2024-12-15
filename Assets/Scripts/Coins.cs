using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Coins : MonoBehaviour
{
    public AudioSource audSrc;
    public AudioClip snd;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        audSrc = player.audSrc;
        if (player != null)
        {
            
            audSrc.PlayOneShot(snd);
            player.coinAmount++;
            player.coinText.text = player.coinAmount.ToString();
            Destroy(gameObject);

        }
    }
}
