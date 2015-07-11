using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    private GameObject gameController;
    private GameController gameControllerScript;



    void Awake()
    {
        gameController = GameObject.Find("GameController");
        gameControllerScript = gameController.GetComponent<GameController>();
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(this.gameObject);
            gameControllerScript.swapPlayers();
        }

    }

    void OnCollisionEnter(Collision other)
    {
        gameControllerScript.ShotHit(other.gameObject);
        Destroy(this.gameObject);
        gameControllerScript.swapPlayers();
    }
}
