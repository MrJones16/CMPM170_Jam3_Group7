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
    GameHandler gameHandler;
    public GameObject HealthBar;
    public GameObject HealthBarScaler;
 
 
    private void Start()
    {
        currentHealth = maxHealth;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
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
            Debug.Log("Editing current health by " + value);
            currentHealth += value;
        }

        // set currentHealth to maxHealth when current out of bound after applying health
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        HealthBarScaler.transform.localScale = new Vector3(currentHealth / maxHealth,1,1);

        // if current health <= 0 after damage health, kill enemy or say game over
        if(currentHealth <= 0){
            if (this.gameObject.GetComponent<PlayerScript>() != null){
                gameHandler.GameOver();
            }else{
                gameHandler.gameObjects.Remove(this.gameObject);
                //gameHandler.player.playerScan.meleeTargets.Remove(this.gameObject);
                Destroy(HealthBar);
                Destroy(this.gameObject);
            }

        }
    }
}
