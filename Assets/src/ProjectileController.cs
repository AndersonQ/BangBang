using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public Camera camera;
    
    private GameObject gameController;
    private GameController gameControllerScript;
    private GameObject player;

    void Awake()
    {
        gameController = GameObject.Find("GameController");
        gameControllerScript = gameController.GetComponent<GameController>();
        gameControllerScript.setMainCameraEnable(false);
        camera.transform.LookAt(this.gameObject.transform);
    }

    void Start()
    {
        player = gameControllerScript.enemyPlayer;
        camera.transform.LookAt(player.transform);
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(this.gameObject);
            gameControllerScript.swapPlayers();
        }

    }

    void LateUpdate()
    {
		if (player != null)
		{
	        transform.LookAt(player.transform);
	        camera.transform.LookAt(player.transform);
		}
    }

    void OnCollisionEnter(Collision other)
    {
        gameControllerScript.ShotHit(other.gameObject);
        Destroy(this.gameObject);
        gameControllerScript.swapPlayers();
    }

    void OnDestroy()
    {
        gameControllerScript.setMainCameraEnable(true);
    }
}
