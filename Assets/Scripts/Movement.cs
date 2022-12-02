using System;
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
                //Debug.Log("Ground tile collider in the way! collidertype: " + groundTile.colliderType);
                return false;
            }
        }

        //do the same thing but for the surface tilemap
        Tile surfaceTile = surface.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(tileX, tileY, 0));
        if (surfaceTile != null){
            if (surfaceTile.colliderType != Tile.ColliderType.None){
                //if there is any kind of collider on the tile, return false / cant move
                //Debug.Log("Surface tile in the way!  collidertype: " + surfaceTile.colliderType);
                return false;
            }
        }
        
        //since the tile is free to move on, move the gameobject!
        this.transform.position += new Vector3(xdir, ydir, 0);
        return true;
    }

    public void pathfindAndMoveOnce(Vector2Int targetPosition){
        Vector2Int startPosition = new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y);
        //make a list of open and closed nodes
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        Node startNode = new Node(startPosition.x, startPosition.y);
        startNode.gCost = 0;
        startNode.hCost = Mathf.Sqrt(Mathf.Pow((targetPosition.x - startPosition.x), 2) + Mathf.Pow((targetPosition.y - startPosition.y), 2));
        startNode.fCost = 0 + startNode.hCost;
        openList.Add(startNode);
        Node currNode = null;
        bool success = false;

        while (openList.Count > 0){
            
            //get the current node
            float minCost = float.MaxValue;
            foreach (Node node in openList){
                if (node.fCost < minCost){
                    currNode = node;
                    minCost = node.fCost;
                }
            }
            if (currNode == null) break;
            //Debug.Log("Node Loop Call at ( " + currNode.posX + " , " + currNode.posY + " )");
            //Debug.Log("fCost = " + currNode.fCost);
            closedList.Add(currNode);
            openList.Remove(currNode);
            
            //check if node is the end target
            if (currNode.posX == targetPosition.x && currNode.posY == targetPosition.y){
                success = true;
                //Debug.Log("Pathfinding Successfull");
                break;
            }
            //create a list of neighbors
            List<Vector2Int> neighbors = new List<Vector2Int>();
            neighbors.Add(new Vector2Int(currNode.posX+1, currNode.posY));
            neighbors.Add(new Vector2Int(currNode.posX-1, currNode.posY));
            neighbors.Add(new Vector2Int(currNode.posX, currNode.posY+1));
            neighbors.Add(new Vector2Int(currNode.posX, currNode.posY-1));
            //do stuff for each neighbor
            foreach (Vector2Int neighbor in neighbors){
                //get the tile data
                Tile groundTile = ground.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(neighbor.x, neighbor.y, 0));
                Tile surfaceTile = surface.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(neighbor.x, neighbor.y, 0));
                //check if the tiles are walkable, and if not then go to next neighbor
                if (groundTile == null) continue;
                if (groundTile.colliderType != Tile.ColliderType.None) continue;
                if (surfaceTile != null){
                    if (surfaceTile.colliderType != Tile.ColliderType.None) continue;
                }
                if (!(neighbor.x == targetPosition.x && neighbor.y == targetPosition.y)){
                    if (gameHandler.GetGameObject(neighbor.x, neighbor.y) != null) continue;
                }
                //now check if the neighbor is in the closed list
                bool foundInList = false;
                foreach (Node n in closedList){
                    if (n.posX == neighbor.x && n.posY == neighbor.y) foundInList = true;
                }
                if (foundInList) continue;

                //check if the position is in the open list already
                foundInList = false;
                foreach (Node n in openList){
                    if (n.posX == neighbor.x && n.posY == neighbor.y) {
                        //found it in the list

                        //update the g cost if it is shorter
                        if (n.gCost > currNode.gCost + 1){
                            n.gCost = currNode.gCost + 1;
                            n.fCost = n.gCost + n.hCost;
                            n.parent = currNode;
                        }
                        foundInList = true;
                    }
                }
                if (foundInList)continue;
                //not in the list
                Node newNode = new Node(neighbor.x, neighbor.y);
                newNode.hCost = Mathf.Sqrt(Mathf.Pow((targetPosition.x - neighbor.x), 2) + Mathf.Pow((targetPosition.y - neighbor.y), 2));
                newNode.gCost = currNode.gCost + 1;
                newNode.fCost = newNode.hCost + newNode.fCost;
                //Debug.Log("Created neighbor node with fCost of: " + newNode.fCost);
                newNode.parent = currNode;
                openList.Add(newNode);
            }//end neighbor loop
        }//end while loop
        if (success){
            int distance = 0;
            Node newCurrent = currNode;
            if (newCurrent.parent != null){
                while (newCurrent.parent.parent != null){
                    distance++;
                    newCurrent = newCurrent.parent;
                }
            }
            //Debug.Log("Distance = " + distance);

            //now, FINALLY, we move to the closest node on the path
            if (newCurrent.posX > startPosition.x)      {MoveRight();}
            else if (newCurrent.posX < startPosition.x) {MoveLeft();}
            else if (newCurrent.posY > startPosition.y) {MoveUp();}
            else if (newCurrent.posY < startPosition.y) {MoveDown();}
        }

    }
    
}

public class Node{
    public int posX;
    public int posY;
    public float gCost;
    public float hCost;
    public float fCost;
    public Node parent;
    public Node(int x, int y){
        posX = x;
        posY = y;
        gCost = 0;
        hCost = 0;
        fCost = 0;
        parent = null;
    }
}
