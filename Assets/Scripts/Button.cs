using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private GameManager manager;

    private bool playerPresent = false;

    void Awake()
    {
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerPresent = true;
            manager.CloseDoor();
            Debug.Log("Player present");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerPresent = false;
            manager.OpenDoor();
            Debug.Log("Player not present");
        }
    }
}
