using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject projectilePrefab;
	public GameObject cannon;
    public GameObject shotRespaw;

    public Image shootImage;

    public bool shot;
    public float minMagnitude;
    float magnitude = 0f;

	private GameObject gameController;
	private GameController gameControllerScript;
    private float shotAt;

	void Awake()
	{
		gameController = GameObject.Find("GameController");
		gameControllerScript = gameController.GetComponent<GameController>();

        shot = false;

        shootImage.fillAmount = 0f;
	}

	void FixedUpdate()
	{
		if (!Input.GetMouseButton (1) && 
		    !Input.GetMouseButton (2) &&
		    this.CompareTag(gameControllerScript.currentPlayerTag))
		{
            Move();

            if (!shot)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    shotAt = Time.time;

                if (!shot && Input.GetKey(KeyCode.Space))
                    shootImage.fillAmount = ((Time.time - shotAt) * 0.3f) * 4;

                if (!shot && Input.GetKeyUp(KeyCode.Space))
                {
                    magnitude = minMagnitude + ((Time.time - shotAt) * 30);
				    Fire(magnitude);
                    shootImage.fillAmount = magnitude = 0f;
			    }
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
