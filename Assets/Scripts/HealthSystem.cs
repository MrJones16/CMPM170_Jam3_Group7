using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
 
 
     public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "ApplyHealth")
        {
            ChangeHealth(1);
            Destroy(collisionInfo.gameObject);
        }
 
        if(collisionInfo.gameObject.tag == "ReduceHealth")
        {
            ChangeHealth(-2);
            Destroy(collisionInfo.gameObject);
        }

        Debug.Log("Current Health: " + currentHealth);
    }
 

    public void ChangeHealth(int value){
        
        if(currentHealth <= maxHealth && currentHealth > 0){
            // do not add health when currentHealth == maxHealth
            if(value >= 0 && currentHealth == maxHealth){
                return;
            }

            currentHealth += value;
        }

        // set currentHealth to maxHealth when current out of bound after applying health
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

        // if current health <= 0 after damage health, restart the game
        if(currentHealth <= 0){
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }
}
