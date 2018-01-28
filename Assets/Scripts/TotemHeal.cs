using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemHeal : MonoBehaviour {

    [SerializeField]
    GameObject parentTotem;
    Totem totem;

    void Start()
    {
        totem = parentTotem.GetComponent<Totem>();
    }

    void OnMouseDown()
    {
        Debug.Log("heal called");   
        if (totem.isPlayerNear)
        {
            totem.HealPlayer();
            
            Debug.Log("heal called");
        }
    }
}
