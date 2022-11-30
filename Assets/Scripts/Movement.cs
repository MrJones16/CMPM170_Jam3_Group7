using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// HARD
// Programmer: Puyi, Peyton, David C.
// Script: Movement
// Description: This script would be attatched to anything that should be able to move, like an enemy or a rock or the player.
// Variables:
// max moves & move count? (this might be on a different script like on a melee enemy script)
// Methods:
// bool MoveUp()
// bool MoveDown()
// bool MoveLeft()
// bool MoveRight()
// Implementation:
// Movement needs to check the ground tilemap for colliders, check the surface tilemap for any tile, and check with the gamehandler (using my function getGameObject(int x,int y))to see if any other gameobject is in the wanted position

// to see if a tile has a collider, i found some code that might help:
// UnityEngine.Tilemaps.Tile t = tilemap.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(x, y, 0));
// if (t != null){
// print(t.name + ", " + t.colliderType);
// }

// note: the position of the gameobject in the grid will just be it’s transform position! Also, the movement methods are bools and should return false if they are unable to move.

public class Movement : MonoBehaviour
{
    Tilemap ground;
    Tilemap surface;
    GameHandler gameHandler;
    private void Start() {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        ground = GameObject.Find("Tilemap_Ground").GetComponent<Tilemap>();
        surface = GameObject.Find("Tilemap_Surface").GetComponent<Tilemap>();
        gameHandler.addGameObject(this.gameObject);
    }
    public bool MoveUp(){
        return move(0, 1);
    }
    public bool MoveDown(){
        return move(0, -1);
    }
    public bool MoveLeft(){
        return move(-1, 0);
    }
    public bool MoveRight(){
        return move(1, 0);
    }
    private bool move(int xdir, int ydir){
        //first, get the tile position we are trying to check
        int tileX = (int)this.transform.position.x + xdir;
        int tileY = (int)this.transform.position.y + ydir;

        //check for other gameobjects in the way
        if (gameHandler.GetGameObject(tileX, tileY) != null){
            return false;
        }

        //get the tile from the ground tilemap
        Tile groundTile = ground.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(tileX, tileY, 0));
        //check if the ground tilemap has a collider. This would be for mountains or pits
        if (groundTile != null){
            if (groundTile.colliderType != Tile.ColliderType.None){
                //if there is any kind of collider on the tile, return false / cant move
                Debug.Log("Ground tile collider in the way! collidertype: " + groundTile.colliderType);
                return false;
            }
        }

        //do the same thing but for the surface tilemap
        Tile surfaceTile = surface.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(tileX, tileY, 0));
        if (surfaceTile != null){
            if (surfaceTile.colliderType != Tile.ColliderType.None){
                //if there is any kind of collider on the tile, return false / cant move
                Debug.Log("Surface tile in the way!  collidertype: " + surfaceTile.colliderType);
                return false;
            }
        }
        
        //since the tile is free to move on, move the gameobject!
        this.transform.position += new Vector3(xdir, ydir, 0);
        return true;
    }
}
