using UnityEngine;
using System;

public class Mine : MonoBehaviour
{
    public int damage = 30;
    public float pushForce = 10f;
    public GameObject explosionEffect;
    private bool isTriggered = false;
    
    public event Action<Mine> ExplodeEvent;

    void OnCollisionEnter(Collision collision)
    {
        if (isTriggered || !collision.gameObject.CompareTag("Player")) return;

        isTriggered = true;
        Explode();
        ApplyDamageAndPush(collision.gameObject);
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 3f);
        gameObject.SetActive(false);
        ExplodeEvent?.Invoke(this);
    }

    void ApplyDamageAndPush(GameObject player)
    {
        HungerSystem hungerSystem = player.GetComponent<HungerSystem>();
        if (hungerSystem != null)
        {
            hungerSystem.DecreaseHunger(damage);
        }

        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 pushDirection = player.transform.position - transform.position;
            pushDirection.Normalize();
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
    
    public void ResetMine()
    {
        isTriggered = false;
        gameObject.SetActive(true);
    }
}