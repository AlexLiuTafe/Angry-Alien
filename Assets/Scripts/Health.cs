using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Health : MonoBehaviour
{
    [System.Serializable]
    public struct HealthSprite
    {
        public Sprite sprite;
        public float health;
    }
    public HealthSprite[] healthSprites;

    public float maxHealth = 100f;
    public float damage = 25f; // How much damage to deal 
    public float painThreshold = 4f; // Velocity to deal pain
    

    private float health = 0f;
    private SpriteRenderer rend;
    private Rigidbody2D rigid;

    void GetReferences()
    {
        rend = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        GetReferences();
    }

    void Start()
    {
        // Set health to max
        health = maxHealth;
        //Sort health list based on health
        healthSprites = healthSprites.OrderBy(a => a.health).ToArray();

    }

    private void OnDrawGizmos()
    {
        GetReferences();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rigid.velocity);
    }

    void OnCollisionEnter2D(Collision2D info)
    {
        // Has the relative velocity reached the pain threshold?
        if(info.relativeVelocity.magnitude > painThreshold)
        {
            // Deal damage with the script's stored damage
            DealDamage(damage);
        }
    }
    public Sprite GetHealthSprite(float health)
    {
        // loop through all health sprite
        foreach (var healthSprite in healthSprites)
        {
            //is health less than current healthSprite?
            if(health <= healthSprite.health)
            {
                //Return the sprite for that health
                return healthSprite.sprite;
            }
        }
        //return nothing
        return rend.sprite;
    }
    // Method for Dealing Damage to object (externally)
    public void DealDamage(float damage)
    {
        // Reduce health by damage
        health -= damage;
        //Assign Sprite based on health
        rend.sprite = GetHealthSprite(health);
        // If health is < 0
        if (health <= 0)
        {
            // Destroy object!
            Destroy(gameObject);
        }
    }
}
