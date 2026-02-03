using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCursor;
    [SerializeField] private GameObject opp;
    [SerializeField] private GameObject oppCursor;

    [Header("Scene Names")]
    [SerializeField] private string winSceneName;
    [SerializeField] private string lossSceneName;
    [SerializeField] private string titleSceneName;

    private int playerScore;
    private int oppScore;

    public bool introDone = false;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != winSceneName &&
           SceneManager.GetActiveScene().name != lossSceneName &&
           SceneManager.GetActiveScene().name != titleSceneName)
        {
            if (intro.GetComponent<Intro>().introDone)
            {
                introDone = true;
                intro.SetActive(false);
            }
        }
    }
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != winSceneName &&
            SceneManager.GetActiveScene().name != lossSceneName &&
            SceneManager.GetActiveScene().name != titleSceneName)
        {
            intro = GameObject.FindGameObjectWithTag("Intro");
        }
    }
    public void StartRound()
    {
        player.SetActive(true);
        opp.SetActive(true);
        oppCursor.SetActive(true);
        playerCursor.SetActive(true);
    }
}
