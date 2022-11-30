using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanning : MonoBehaviour
{
    GameHandler scanHand;
    List<Vector3> possibleTargets;
    int[] iter = new int[2];
    public List<Vector3> scanMelee(Vector3 inCord)
    {
        iter[0] = (int)inCord.x - 1;
        iter[1] = (int)inCord.y - 1;

        for(int i = 0; i <= 9; i++)
        {
            for(int j = 0; j <= 3; j++)
            {
                if()
            }
        }
        
    }
}
