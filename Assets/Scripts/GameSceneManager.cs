using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void SelectLevel(int level)
    {
        switch (level)
        {
            case 0:
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene("1.1", LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadScene("lvl2", LoadSceneMode.Single);
                break;
            case 3:
                SceneManager.LoadScene("lvl3", LoadSceneMode.Single);
                break;
            case 4:
                SceneManager.LoadScene("lvl4", LoadSceneMode.Single);
                break;
            case 5:
                SceneManager.LoadScene("win", LoadSceneMode.Single);
                break;
            case 6:
                SceneManager.LoadScene("credit", LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }
}
