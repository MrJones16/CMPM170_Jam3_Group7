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
    public HealthSystem playerHealth;
    //public scanning playScan;
    public Movement playerMove;
    public int damage = 2;
    GameHandler gameHandler;
    public bool PlayerTurn = false;
    public bool tempBool;
    public int actionMax = 5;
    public int actionsLeft = 5;
    public scanning playerScan;
    public List<GameObject> meleeTragets; 
    public List<GameObject> rangedTragets;
    public GameObject damagePrefab;
    public GameObject brokenBarrel;
    Interaction interaction;
    public AudioSource attackSource;
    // Update is called once per frame

    //call PlayerScript.takeTurn() to have the player take their turn
    private void Start() 
    {
        meleeTragets = new List<GameObject>();
        rangedTragets = new List<GameObject>();
        playerHealth.maxHealth = 5;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        actionsLeft = actionMax;
        playerScan = this.GetComponent<scanning>();
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    void Update()
    {
        if (PlayerTurn && actionsLeft > 0){
            if (Input.GetKeyDown(KeyCode.W))
                {
                    tempBool = playerMove.MoveUp();

                    if (tempBool) 
                    {
                        // playerScan.scanMelee();
                        // playerScan.scanRanged();
                        actionsLeft--; 
                    }
                }
            if (Input.GetKeyDown(KeyCode.A))
            {
                tempBool = playerMove.MoveLeft();
                if (tempBool)
                {
                    // playerScan.scanMelee();
                    // playerScan.scanRanged();
                    actionsLeft--;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                tempBool = playerMove.MoveRight();
                if (tempBool) {
                    // playerScan.scanMelee();
                    // playerScan.scanRanged();
                    actionsLeft--;
                } 
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                tempBool = playerMove.MoveDown();
                if (tempBool)
                {
                    // playerScan.scanMelee();
                    // playerScan.scanRanged();
                    actionsLeft--;
                }
            }
            if (Input.GetKeyDown(KeyCode.F)){
                bool didDamage = false;
                bool spoke = false;
                playerScan.scanMelee();
                //Attack all enemies in the melee range
                foreach (GameObject enemy in playerScan.meleeTargets){
                    if (enemy.gameObject.tag == "NPC"){
                        interaction.EnableUI();
                        actionsLeft = 0;

                        break;
                    }
                }
                if (!spoke){
                    foreach (GameObject enemy in playerScan.meleeTargets){
                        if (enemy.GetComponent<Enemy>() != null){
                            GameObject damageIndication = Instantiate(damagePrefab, enemy.transform.position + new Vector3(0,1,-2), this.transform.rotation);
                            StartCoroutine(destroyAfterSecond(damageIndication));
                            enemy.GetComponent<HealthSystem>().ChangeHealth(-damage);
                            didDamage = true;

                        }else if (enemy.GetComponent<Barrel>() != null){
                            GameObject damageIndication = Instantiate(damagePrefab, enemy.transform.position + new Vector3(0,1,-2), this.transform.rotation);
                            StartCoroutine(destroyAfterSecond(damageIndication));
                            interaction.updateDialoge(enemy.GetComponent<Barrel>().BarrelChoice);
                            Vector3 barrelPosition = enemy.transform.position;
                            enemy.GetComponent<HealthSystem>().ChangeHealth(-damage);
                            Instantiate(brokenBarrel, barrelPosition, this.transform.rotation);
                            didDamage = true;
                        }

                    }
                }
                playerScan.meleeTargets.Clear();
                if (didDamage){
                    attackSource.Play();
                    actionsLeft = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape)) actionsLeft = 0;
        }
        if (PlayerTurn && actionsLeft <= 0){
            StartCoroutine(gameHandler.endPlayersTurn());
        }
    }
    IEnumerator destroyAfterSecond(GameObject item){
        yield  return new WaitForSeconds(1);
        Destroy(item);
    }
}
