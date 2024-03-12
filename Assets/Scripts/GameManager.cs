using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject door;
    public TMP_Text cycleCountText;
    public GameObject exitDoorBlocker;
    private bool doorClosed = false;
    private int cyclesCompleted;
    private bool inExitDoor = false;

    public void OpenDoor()
    {
        if(cyclesCompleted < 3)
        {
            door.SetActive(false);
            cyclesCompleted = 0;
            doorClosed = false;
        }
        
    }

    public void CloseDoor()
    {
        door.SetActive(true);
        doorClosed = true;
    }

    void Win()
    {
        if(cyclesCompleted >= 3 && inExitDoor)
        {
            Debug.Log("You win!");
        }
    }

    public void IncrementCycle()
    {
        if(doorClosed)
        {
            cyclesCompleted++;
            cycleCountText.text = "Cycles: " + cyclesCompleted;
            if(cyclesCompleted == 3)
            {
                exitDoorBlocker.SetActive(false);
            }
            Win();
        }
    }

    public void EnterExit()
    {
        inExitDoor = true;
        Win();
    }

    public void ExitExit()
    {
        inExitDoor = false;
    }
}
