using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class scanning : MonoBehaviour
{
    UnityEngine.Tilemaps.Tile tempTile;
    GameObject tileTop;
    GameHandler scanHand;
    public List<GameObject> meleeTargets;
    public List<GameObject> rangedTargets;
    int[] iter = new int[2];
    private void Start() {
        meleeTargets = new List<GameObject>();
        rangedTargets = new List<GameObject>();
        scanHand = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }
    public void scanMelee()
    {
        GameObject enemy = scanHand.GetGameObject((int)this.transform.position.x+1, (int)this.transform.position.y);
        if (enemy != null){
            if (enemy.GetComponent<Enemy>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.GetComponent<Barrel>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.gameObject.tag == "NPC"){
                meleeTargets.Add(enemy);
            }
        }
        enemy = scanHand.GetGameObject((int)this.transform.position.x-1, (int)this.transform.position.y);
        if (enemy != null){
            if (enemy.GetComponent<Enemy>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.GetComponent<Barrel>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.gameObject.tag == "NPC"){
                meleeTargets.Add(enemy);
            }
        }
        enemy = scanHand.GetGameObject((int)this.transform.position.x, (int)this.transform.position.y+1);
        if (enemy != null){
            if (enemy.GetComponent<Enemy>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.GetComponent<Barrel>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.gameObject.tag == "NPC"){
                meleeTargets.Add(enemy);
            }
        }
        enemy = scanHand.GetGameObject((int)this.transform.position.x, (int)this.transform.position.y-1);
        if (enemy != null){
            if (enemy.GetComponent<Enemy>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.GetComponent<Barrel>() != null){
                meleeTargets.Add(enemy);
            }
            if (enemy.gameObject.tag == "NPC"){
                meleeTargets.Add(enemy);
            }
        }

    }
    public void scanRanged()
    {
        //rangedTargets = new List<GameObject>();
        // int iterX;
        // int iterY;
        // for(int dir = 0; dir < 9; dir++)
        // {
        //     switch (dir)
        //     {
        //         case 0:
        //             iterX = 0;
        //             iterY = 1;
        //             break;
        //         case 1:
        //             iterX = 1;
        //             iterY = 0;
        //             break;
        //         case 2:
        //             iterX = 0;
        //             iterY = -1;
        //             break;
        //         case 3:
        //             iterX = -1;
        //             iterY = 0;
        //             break;
        //         case 4:
        //             iterX = 1;
        //             iterY = 1;
        //             break;
        //         case 5:
        //             iterX = -1;
        //             iterY = -1;
        //             break;
        //         case 6:
        //             iterX = -1;
        //             iterY = 1;
        //             break;
        //         case 7:
        //             iterX = 1;
        //             iterY = -1;
        //             break;
        //     }
        // }
        //return rangedTargets;
    }
    public void scanRangeIterator(int iterX,int iterY, Vector3 inCord)
    {
        iter[0] = (int)inCord.x + iterX;
        iter[1] = (int)inCord.y + iterY;
        
        while (true)
        {
            tempTile = GameObject.Find("Tilemap_Surface").GetComponent<Tilemap>().GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int((int)iter[0], (int)iter[1], 0));
            tileTop = scanHand.GetGameObject((int)iter[0], (int)iter[1]);
            if (tileTop != null && tempTile != null)
            {
                rangedTargets.Add(scanHand.GetGameObject(iter[0], iter[1]));
                break;
            }
            else
            {
                iter[0] += iterX;
                iter[1] += iterY;
            }
        }
    }
}
