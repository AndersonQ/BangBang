using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject projectilePrefab;
	public GameObject movingCentre;
    public GameObject shotRespaw;
	public GameObject explosionPrefab;
    public GameObject shootSmokePrefab;

    public Image shootMagnitudeIndicator;

    public bool shot;
    public float minMagnitude;
    
	private GameObject gameController;
	private GameController gameControllerScript;
    private AudioSource shootSound;
    private float shotAt;
	private float magnitude = 0f;

	//""
	void Awake()
	{
		gameController = GameObject.Find("GameController");
		gameControllerScript = gameController.GetComponent<GameController>();
        shootSound = GetComponent<AudioSource>();

        shot = false;

        shootMagnitudeIndicator.fillAmount = 0f;
	}

	void Update()
	{

		// Does not move or fire if a mouse button is pressed or if it's not the player's turn
		if (!Input.GetMouseButton (1) && 
		    !Input.GetMouseButton (2) &&
		    this.CompareTag(gameControllerScript.currentPlayerTag))
		{
			Move();

			if (!shot)
			{

				if (Input.GetKeyDown(KeyCode.Space)) 
					shotAt = Time.time;
				//Idicates it's charging to shoot
				if (Input.GetKey(KeyCode.Space))
					shootMagnitudeIndicator.fillAmount = ((Time.time - shotAt) * 0.3f) * 4;
				//Do shoot
				if (Input.GetKeyUp(KeyCode.Space))
				{
					magnitude = minMagnitude + ((Time.time - shotAt) * 30);
					magnitude = (magnitude > 40f) ? 40f : magnitude;
					Fire(magnitude);
					shootMagnitudeIndicator.fillAmount = magnitude = 0f;
				}

       
                    

			}
		}
	}

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        this.transform.eulerAngles += new Vector3(0.0f, moveHorizontal, 0.0f);

		//Limits the pitch range
        if (320f <= movingCentre.transform.eulerAngles.x && movingCentre.transform.eulerAngles.x <= 360f)
        	movingCentre.transform.eulerAngles -= new Vector3(-moveVertical, 0.0f, 0.0f);

		//Put the pitch back to the allowed range
		if (((movingCentre.transform.eulerAngles.x > 359f) && (movingCentre.transform.eulerAngles.x <= 360f)) ||
		    ((movingCentre.transform.eulerAngles.x >= 0f) && (movingCentre.transform.eulerAngles.x <= 1f)))
		{
			movingCentre.transform.eulerAngles = new Vector3(359f, movingCentre.transform.eulerAngles.y, movingCentre.transform.eulerAngles.z);
		}
		else if ((movingCentre.transform.eulerAngles.x < 320f))
		{
			movingCentre.transform.eulerAngles = new Vector3(320f, movingCentre.transform.eulerAngles.y, movingCentre.transform.eulerAngles.z);
		}
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
            projectileRb.velocity = shotRespaw.transform.TransformVector(new Vector3(0.0f, magnitude, 0.0f));
        }
	}
}
