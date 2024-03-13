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
            case 1:
                SceneManager.LoadScene("1.1", LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadScene("1.2", LoadSceneMode.Single);
                break;
            case 3:
                SceneManager.LoadScene("1.3", LoadSceneMode.Single);
                break;
            case 4:
                SceneManager.LoadScene("1.4", LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }
}
