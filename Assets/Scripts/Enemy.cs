using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HARD
// Programmer: Peyton
// Script: Enemy
// Description:
// Make a script to put on enemies to make them move and attack. Because we want different types of enemies (melee, ranged & magic) the movement/pathfinding needs to be based on what kind of enemy it is
// Variables:
// what kind of enemy it is (melee or ranged)
// How much damage they do
// MoveDistance
// Methods:
// TakeTurn() This should make the enemy use their pathfinding and also try to attack if they can

// Notes: 
// While there are 3 planned enemy types, the AI for the ranger and magic will pretty much be the same. You could have a variable as a sprite that can be changed, so a ranger could have an arrrow sprite and the mage could have a fireball or something
// I have no idea how to make pathfinding, and im sure it would be difficult. I believe unity has some stuff you can use for pathfinding, but you could always do pathfinding last, and just make a basic AI to move towards the player
// Attack

public class Enemy : MonoBehaviour
{
    Movement movement;
    GameHandler gameHandler;
    //want a health script
    public string enemyType = "Melee";
    public int damage = 5;
    public int actions = 10;
    private void Start() {
        movement = this.gameObject.GetComponent<Movement>();
        if (movement == null){
            movement = this.gameObject.AddComponent<Movement>();
        }
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }
    public void TakeTurn(){
        //get the player game object, this can be changed to find a script instead of a name.
        GameObject target;
        foreach(GameObject item in gameHandler.gameObjects){
            if (item.name == "Player"){
                target = item;
                break;
            }
        }
        switch(enemyType){
            case "Melee":
                
                break;
            case "Ranger":
                break;
            case "Mage":
                break;
            default:
                Debug.Log("Didn't specify an enemy type! Types: Melee, Ranger, Mage");
                break;
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)){
            if(movement.MoveLeft()){

            }
        }
        if (Input.GetKeyDown(KeyCode.S)){
            if(movement.MoveDown()){

            }
        }
        if (Input.GetKeyDown(KeyCode.D)){
            if(movement.MoveRight()){

            }
        }
        if (Input.GetKeyDown(KeyCode.W)){
            if(movement.MoveUp()){

            }
        }


    }
}
