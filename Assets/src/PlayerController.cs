using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject projectilePrefab;
	public GameObject cannon;
    public GameObject shotRespaw;
	public GameObject explosionPrefab;
    public GameObject shootSmokePrefab;

    public Image shootImage;

    public bool shot;
    public float minMagnitude;
    float magnitude = 0f;

	private GameObject gameController;
	private GameController gameControllerScript;
    private AudioSource shootSound;
    private float shotAt;

	//""
	void Awake()
	{
		gameController = GameObject.Find("GameController");
		gameControllerScript = gameController.GetComponent<GameController>();
        shootSound = GetComponent<AudioSource>();

        shot = false;

        shootImage.fillAmount = 0f;
	}

	void Update()
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
				
				if (Input.GetKey(KeyCode.Space))
					shootImage.fillAmount = ((Time.time - shotAt) * 0.3f) * 4;
				
				if (Input.GetKeyUp(KeyCode.Space))
				{
					magnitude = minMagnitude + ((Time.time - shotAt) * 30);
					magnitude = (magnitude > 40f) ? 40f : magnitude;
					Fire(magnitude);
					shootImage.fillAmount = magnitude = 0f;
				}

                if (Input.GetKeyUp(KeyCode.I))
                    Debug.Log("cannon.transform.eulerAngles.x: " + cannon.transform.eulerAngles.x);
			}
		}
	}

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        this.transform.eulerAngles += new Vector3(0.0f, moveHorizontal, 0.0f);

        //if (cannon.transform.eulerAngles.x >= 0 && cannon.transform.eulerAngles.x <= 320)
        	cannon.transform.eulerAngles -= new Vector3(moveVertical, 0.0f, 0.0f);
    }

	void Fire(float magnitude)
	{
        if (!shot)
        {
            shootSound.Play();
            shot = true;
            GameObject projectile = (GameObject)Instantiate(projectilePrefab,
                                                            shotRespaw.transform.position,
                                                            Quaternion.identity);
            Instantiate(shootSmokePrefab, shotRespaw.transform.position, Quaternion.identity);

            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.velocity = cannon.transform.TransformVector(new Vector3(0.0f, magnitude, 0.0f));
        }
	}
}
