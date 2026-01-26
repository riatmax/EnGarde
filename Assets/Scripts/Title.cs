using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string levelName;

    public void loadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
