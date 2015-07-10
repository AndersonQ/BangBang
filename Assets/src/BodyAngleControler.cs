using UnityEngine;
using System.Collections;

public class BodyAngleControler : MonoBehaviour
{
	public GameObject projectilePrefab;
	public GameObject cannon;
    public GameObject shotRespaw;

    public bool shot;
    public float minMagnitude;

	private GameObject gameController;
	private GameController gameControllerScript;
    private float shotAt;

	void Awake()
	{
		gameController = GameObject.Find("GameController");
		gameControllerScript = gameController.GetComponent<GameController>();

        shot = false;
	}

	void FixedUpdate()
	{
        float magnitude;
		if (!Input.GetMouseButton (1) && 
		    !Input.GetMouseButton (2) &&
		    this.CompareTag(gameControllerScript.currentPlayerTag))
		{
            Move();

            if (Input.GetKeyDown("space"))
                shotAt = Time.time;
			
			if (Input.GetKeyUp("space")) {
                magnitude = minMagnitude + ((Time.time - shotAt) * 30);
				Fire(magnitude);
			}
		}
	}

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        this.transform.eulerAngles += new Vector3(0.0f, moveHorizontal, 0.0f);
        cannon.transform.eulerAngles -= new Vector3(moveVertical, 0.0f, 0.0f);
    }

	void Fire(float magnitude)
	{
        Debug.Log(this.name + " shot: " + shot);
        if (!shot)
        {
            shot = true;
            GameObject projectile = (GameObject)Instantiate(projectilePrefab,
                                                            shotRespaw.transform.position,
                                                            Quaternion.identity);

            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.velocity = cannon.transform.TransformVector(new Vector3(0.0f, magnitude, 0.0f));
        }
	}
}
