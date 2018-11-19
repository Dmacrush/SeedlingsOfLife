using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour {

    public GameObject pickupEffect;

    public float multiplier = 1.4f;

    public float duration = 4f;

    

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine (Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        // spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // apply effect to the player
            // make player bigger
        player.transform.localScale *= multiplier;
        // change stats
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.health *= multiplier;

        // Wait x amount of seconds
        yield return new WaitForSeconds(duration);

        // Reverse the effect on our player
        stats.health /= multiplier;

        // remove power up object
        Destroy(gameObject);


        // Hide Powerup
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
