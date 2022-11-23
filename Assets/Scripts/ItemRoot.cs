using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ItemRoot : MonoBehaviour
{
    byte quantity;
    string name;
    
    public string getName()
    {
        return name;
    }
    public void setName(string newName)
    {
        name = newName;
    }
}
