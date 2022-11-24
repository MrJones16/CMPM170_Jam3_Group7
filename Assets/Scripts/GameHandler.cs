using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public List<GameObject> gameObjects;
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
}
