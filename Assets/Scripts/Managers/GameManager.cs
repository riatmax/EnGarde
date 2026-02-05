using TMPro;
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
    [SerializeField] private GameObject playerStart;
    [SerializeField] private GameObject oppStart;
    [SerializeField] private TMP_Text score;

    [Header("Scene Names")]
    [SerializeField] private string winSceneName;
    [SerializeField] private string lossSceneName;
    [SerializeField] private string titleSceneName;
    [SerializeField] private string gameSceneName;

    ScoreCounter sc;

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

            score.text = $"{sc.playerScore} - {sc.oppScore}";

            if (sc.oppScore == 5)
            {
                SceneManager.LoadScene(lossSceneName);
                sc.playerScore = 0;
                sc.oppScore = 0;
            }
            if (sc.playerScore == 5)
            {
                SceneManager.LoadScene(winSceneName);
                sc.playerScore = 0;
                sc.oppScore = 0;
            }
        }
    }
    private void Awake()
    {
        sc = ScoreCounter.Instance;
        if (SceneManager.GetActiveScene().name != winSceneName &&
            SceneManager.GetActiveScene().name != lossSceneName &&
            SceneManager.GetActiveScene().name != titleSceneName)
        {
            intro = GameObject.FindGameObjectWithTag("Intro");
            player = GameObject.FindWithTag("PlayerAvatar");
            opp = GameObject.FindWithTag("OpponentAvatar");
        }
    }
    public void StartRound()
    {
        player.SetActive(true);
        opp.SetActive(true);
        oppCursor.SetActive(true);
        playerCursor.SetActive(true);
    }
    public void resetRound()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
