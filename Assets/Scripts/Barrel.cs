using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    GameHandler gameHandler;
    public int BarrelChoice = 1;
    private void Start() {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        gameHandler.addGameObject(this.gameObject);
    }
}
