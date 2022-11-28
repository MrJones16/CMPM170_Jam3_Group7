using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: David
    Script: Player
    Description:
    [x] Make a script that will be attatched to the player, 
    and this script will use the movement script to move. 
    
    [x] This script should also tell the game handler’s turn based 
    system when the player is finished with their turn

    [] The player should be able to interact with things, so you should 
    check for gameobjects through the gamehandler, and look for 
    the different scripts like the Enemy script, 
    the NPC script, rock script, ect
    When it sees a script like the enemy script, 
    a pop-up should appear and let the player choose to attack.
 */
public class PlayerScript : MonoBehaviour
{
    public HealthSystem playerHealth = new HealthSystem();
    public Movement playerMove;
    public bool PlayerTurn = false;
    public const int actionMax = 5;
    public int actionsLeft = 5;
    // Update is called once per frame

    //call PlayerScript.takeTurn() to have the player take their turn
    private void Start() 
    {
        playerHealth.maxHealth = 20;
    }

    public void takeTurn()
    {
        // while loop
        while (actionsLeft != 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerMove.MoveUp();
                actionsLeft--;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerMove.MoveLeft();
                actionsLeft--;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerMove.MoveRight();
                actionsLeft--;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerMove.MoveDown();
                actionsLeft--;
            }
            if (Input.GetKeyDown(KeyCode.Escape)) break;
        }
        //resets actions left for next turn
        actionsLeft = actionMax;
        // take turn function concludes, returning to the turn order
    }
    void Update()
    {
        

    }
}
