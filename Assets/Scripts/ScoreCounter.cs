using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; private set; }
    public int playerScore = 0;
    public int oppScore = 0;

    private void Awake()
    {
        // 2. Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If one exists that isn't me, destroy myself
            Destroy(gameObject);
            return;
        }

        // 3. Set the instance to this script
        Instance = this;

        DontDestroyOnLoad(gameObject);

        oppScore = 0;
        playerScore = 0;
    }
}
