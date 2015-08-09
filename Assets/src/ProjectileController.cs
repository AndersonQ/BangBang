using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public Camera camera;
    
    private GameObject gameController;
    private GameController gameControllerScript;
	private GameObject enemyPlayer; //The projectile's target

    void Awake()
    {
        gameController = GameObject.Find("GameController");
        gameControllerScript = gameController.GetComponent<GameController>();
        gameControllerScript.setMainCameraEnable(false);
        camera.transform.LookAt(this.gameObject.transform);
    }

    void Start()
    {
        enemyPlayer = gameControllerScript.enemyPlayer;
        camera.transform.LookAt(enemyPlayer.transform);
    }

    void Update()
    {
		// Destroys the projectile if it gets out of the scenario
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
            gameControllerScript.swapPlayers();
        }
    }

    void LateUpdate()
    {
		if (enemyPlayer != null)
		{
			// Camera always look to the enemy player
	        transform.LookAt(enemyPlayer.transform);
	        camera.transform.LookAt(enemyPlayer.transform);
		}
    }

    void OnCollisionEnter(Collision other)
    {
        gameControllerScript.ShotHit(other.gameObject);
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        gameControllerScript.swapPlayers();
        gameControllerScript.setMainCameraEnable(true);
    }
}
