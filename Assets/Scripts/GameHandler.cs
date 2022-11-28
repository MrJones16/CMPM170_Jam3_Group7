using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameHandler : MonoBehaviour
{
    List<GameObject> gameObjects;
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
