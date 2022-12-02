using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public PlayerScript player;
    public Text turnText;
    public Text GameOverText;
    bool playersTurn;
    bool enemyTurns = false;
    bool isGameOver = false;

    public void addGameObject(GameObject item){
        if (gameObjects == null){
            gameObjects = new List<GameObject>();
        }
        gameObjects.Add(item);
    }
    public GameObject GetGameObject(int x, int y){
        if (gameObjects == null) return null;
        foreach(GameObject item in gameObjects){
            if (item.transform.position.x == x && item.transform.position.y == y){
                return item;
            }
        }
        return null;
    }
    private void Start() {
        playersTurn = true;
        GameOverText.enabled = false;
    }
    private void Update() {
        if (isGameOver){
            if (Input.GetKeyDown(KeyCode.R)){
                Scene scene = SceneManager.GetActiveScene(); 
                SceneManager.LoadScene(scene.name);
            }
            return;
        }
        if (playersTurn){
            player.PlayerTurn = true;
            turnText.text = "Player's Turn";
            enemyTurns = false;
        }else{
            if (enemyTurns == false){
                enemyTurns = true;
                turnText.text = "Enemies' Turn";
                //get all the enemies
                foreach (GameObject item in gameObjects){
                    Enemy enemy = item.GetComponent<Enemy>();
                    if (enemy != null){
                        //enemies.Add(item);
                        //Debug.Log("Calling take turn on enemy");
                        enemy.TakeTurn();
                    }
                }
                StartCoroutine(endEnemiesTurns());
            }
        }
    }
    public void GameOver(){
        Debug.Log("Game Over!");
        GameOverText.enabled = true;
        isGameOver = true;
    }
    public IEnumerator endPlayersTurn (){
        yield return new WaitForSeconds(1);
        playersTurn = false;
        player.PlayerTurn = false;
        player.actionsLeft = player.actionMax;
    }
    public IEnumerator endEnemiesTurns(){
        yield return new WaitForSeconds(3);
        playersTurn = true;
    }

    // how to use
    // byte item.stat = addToStat(number to add, item.stat)
    public static byte AddToStat(int addValue, int storeValue)
    {
        return checkRollover((storeValue + addValue));
    }
    public static byte SubtractFromStat(int subValue, int storValue)
    {
        return checkRollover((storValue - subValue));
    }

    // ensures that HP, MP, and other stats do not go into the neagtives
    public static byte checkRollover(int inVal)
    {
        return (byte)Math.Clamp(inVal, 0, 99);
    }
}
