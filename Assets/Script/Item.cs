using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool IsCollected;

    public bool IsCollectedProp {
        get
        {
            return IsCollected;
        }
        set
        { 
            IsCollected = value;
        }
    }
}
