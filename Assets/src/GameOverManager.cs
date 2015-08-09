using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameController;
    private GameController gameControllerScript;
    public float restartDelay = 5f;

    Animator animator;
    float restartTimer;


    void Awake()
    {
        gameController = GameObject.Find("GameController");
        gameControllerScript = gameController.GetComponent<GameController>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameControllerScript.gameOver)
        {
            animator.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                int nextLevel = Application.loadedLevel + 1;
                
                nextLevel = nextLevel > 3 ? 0 : nextLevel;
                Application.LoadLevel(nextLevel);
            }
        }
    }
}