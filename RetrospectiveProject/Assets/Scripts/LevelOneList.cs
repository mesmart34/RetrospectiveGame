using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOneList : MonoBehaviour
{
    public Toggle[] Toggles;
    
    public void SetObject(Item item)
    {
        Toggles[item.Id].isOn = true;
    }

    public bool isFull()
    {
        for(var i = 0; i < Toggles.Length; i++)
        {
            if (!Toggles[i].isOn)
                return false;
        }
        return true;
    }
    
}
