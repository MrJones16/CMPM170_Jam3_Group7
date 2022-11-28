using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
 
    public float damage;
    public float healing;

    public GameObject reduceHP;
    public GameObject applyHP;
 
 
    private void Start()
    {
        currentHealth = maxHealth;
        reduceHP = GameObject.Find("ReduceHealth");
        applyHP = GameObject.Find("ApplyHealth");
    }
 
 
     public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.name == "ApplyHealth")
        {
            ApplyHealing();
            Destroy(applyHP);
        }
 
        if(collisionInfo.collider.name == "ReduceHealth")
        {
            DamageHealth();
            Destroy(reduceHP);
        }

        Debug.Log("Current Health: " + currentHealth);
    }
 
    void ApplyHealing()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healing;
        }
    }
 
    public void DamageHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
    }
}
