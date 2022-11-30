using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * Programmer: Puyi, Peyton, David C.
Script: Movement
Description: This script would be attatched to anything
that should be able to move, like an enemy or a rock or the player.
Variables:
max moves & move count? 
(this might be on a different script like on a melee enemy script)
Methods:
bool MoveUp()
bool MoveDown()
bool MoveLeft()
bool MoveRight()
Implementation:
Movement needs to check the ground tilemap for colliders, 
check the surface tilemap for any tile,
and check with the gamehandler (using my function getGameObject(int x,int y))
to see if any other gameobject is in the wanted position

to see if a tile has a collider, i found some code that might help:
UnityEngine.Tilemaps.Tile t = tilemap.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(x, y, 0));
if (t != null){
print(t.name + ", " + t.colliderType);
}

note: the position of the gameobject in the grid will just 
be it’s transform position! Also, the movement methods
are bools and should return false if they are unable to move
 */

public class Movement : MonoBehaviour
{
    /*
     * The Up, Down, Left, & Right methods define which tile is being checked.
     * EX: UP checks player/enemy location +1 on Y axis. Gets it, assigns it to T. In future, if this tile is a ditch, it will prevent movement
     *      TileTop stores any entities (obsticles tress, enemies, barrels etc) is at the searched location and assigns it to tileTop
     * Once both Vars are assigned, its passed to the "applyMovemoment" function which checks the tile is empty and navicable.
     *      if both are true (both are null) then whatever is moving will move to that location. 
     * applyMovement returns a bool to Up,Down,Left,Right which return it back as well.
     */
    GameHandler handler;
    UnityEngine.Tilemaps.Tile t;
    GameObject tileTop;
    public bool MoveUp(Vector3 inPos)
    {
        t = GameObject.Find("Tilemap_Surface").GetComponent<Tilemap>().GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int((int)inPos.x, (int)inPos.y +1, 0));
        tileTop = handler.GetGameObject((int)inPos.x, (int)inPos.y + 1);
        return applyMovement(t, tileTop);
        
    }
    public bool MoveDown(Vector3 inPos)
    {
        t = GameObject.Find("Tilemap_Surface").GetComponent<Tilemap>().GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int((int)inPos.x, (int)inPos.y -1, 0));
        tileTop = handler.GetGameObject((int)inPos.x, (int)inPos.y -1);
        return applyMovement(t, tileTop);
    }
    public bool MoveLeft(Vector3 inPos)
    {
        t = GameObject.Find("Tilemap_Surface").GetComponent<Tilemap>().GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int((int)inPos.x -1, (int)inPos.y, 0));
        tileTop = handler.GetGameObject((int)inPos.x - 1, (int)inPos.y);
        return applyMovement(t, tileTop);       
    }
    public bool MoveRight(Vector3 inPos)
    {
        t = GameObject.Find("Tilemap_Surface").GetComponent<Tilemap>().GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int((int)inPos.x + 1, (int)inPos.y, 0));
        tileTop = handler.GetGameObject((int)inPos.x + 1, (int)inPos.y);
        return applyMovement(t, tileTop);
    }

    public bool applyMovement(UnityEngine.Tilemaps.Tile inT, GameObject inTileTop)
    {
        if (inT == null && tileTop == null)
        {
            this.transform.position = new Vector3(inTileTop.transform.position.x, inTileTop.transform.position.x, 0);
            return true;
        }
        else if (inT != null)
        {
            print(inT.name + ", " + inT.colliderType);
            return false;
        }
        else
        {
            print("object obstructing movement");
            return false;
        }
    }
    
}
