using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    /*    public scanning scan;*/
    public GameObject UI;
    public Text dialogue;
    public GameObject NPC;
    GameHandler gameHandler;
    public GameObject slimePrefab;
    int choice = 0;

    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        //NPC = GameObject.Find("NPC");
        
    }

    public void updateDialoge(int decision){
        choice = decision;
        switch (choice){
            case 0:
                //beginning dialogue
                dialogue.text = "Hey, do you like slimes?  ....  You don't speak huh?  Well go break the left barrel if you do like them, and the right barrel if you don't";
                break;
            case 1:
                gameHandler.gameObjects.Remove(NPC);
                //dialogue.text = "Yo i'm dead lol";
                Vector3 position = NPC.transform.position;
                Destroy(NPC);
                Instantiate(slimePrefab, position, this.transform.rotation);
                break;
            case 2:
                dialogue.text = "Yeah, I don't like them either...  So make sure you kill all the slimes!";
                break;
            default:
                break;
        }
    }
    public void EnableUI(){
        UI.SetActive(true);
        updateDialoge(choice);
        StartCoroutine(closeUI());
    }
    IEnumerator closeUI(){
        yield return new WaitForSeconds(4);
        UI.SetActive(false);
    }
    // public void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "NPC")
    //     {
    //         UI.SetActive(true);
    //         updateDialoge(choice);
    //     }
    // }
    // public void OnCollisionExit2D(Collision2D other)
    // {
    //     UI.SetActive(false);
    // }

}
