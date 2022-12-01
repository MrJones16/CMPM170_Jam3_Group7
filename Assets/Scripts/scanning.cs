using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class scanning : MonoBehaviour
{
    UnityEngine.Tilemaps.Tile tempTile;
    GameObject tileTop;
    GameHandler scanHand;
    List<GameObject> meleeTargets;
    List<GameObject> rangedTargets;
    int[] iter = new int[2];
    public List<GameObject> scanMelee(Vector3 inCord)
    {
        iter[0] = (int)inCord.x - 1;
        iter[1] = (int)inCord.y - 1;

        for(int y = 0; y <= 9; y++)
        {
            for(int x = 0; x <= 3; x++)
            {
                if(iter[0] + x != inCord.x && iter[1] + y != inCord.y)
                {
                    if (scanHand.GetGameObject(iter[0] + x, iter[1] + y) != null) meleeTargets.Add(scanHand.GetGameObject(iter[0] + x, iter[1] + y));
                }
            }
        }
        return meleeTargets;
    }
    public List<GameObject> scanRanged(Vector3 inCord)
    {
        int iterX;
        int iterY;
        for(int dir = 0; dir < 9; dir++)
        {
            switch (dir)
            {
                case 0:
                    iterX = 0;
                    iterY = 1;
                    break;
                case 1:
                    iterX = 1;
                    iterY = 0;
                    break;
                case 2:
                    iterX = 0;
                    iterY = -1;
                    break;
                case 3:
                    iterX = -1;
                    iterY = 0;
                    break;
                case 4:
                    iterX = 1;
                    iterY = 1;
                    break;
                case 5:
                    iterX = -1;
                    iterY = -1;
                    break;
                case 6:
                    iterX = -1;
                    iterY = 1;
                    break;
                case 7:
                    iterX = 1;
                    iterY = -1;
                    break;
            }
        }
        return rangedTargets;
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
