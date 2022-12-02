using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    /*    public scanning scan;*/
    public GameObject UI;

    void Start()
    {
        
    }


    void Update()
    {
        /*for (int i = 0; i < scan.scanMelee(this.transform.position).Count; i++)
        {
            if (scan.scanMelee(this.transform.position)[i].name == "Player")
            {
                Debug.Log("1");
            }
        }*/
            
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            UI.SetActive(true);
        }
    }
    public void OnCollisionExit2D(Collision2D other)
    {
        UI.SetActive(false);
    }

}
