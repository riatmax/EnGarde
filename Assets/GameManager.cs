using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string winSceneName;
    [SerializeField] private string lossSceneName;
    private int playerScore;
    private int oppScore;
    private void Update()
    {
        if (playerScore == 5)
        {
            SceneManager.LoadScene(winSceneName);
        }
        if (oppScore == 5)
        {
            SceneManager.LoadScene(lossSceneName);
        }
    }
}
