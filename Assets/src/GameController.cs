using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Camera firstPersonCamera;
	public Camera mainCamera;
    public Camera p1CannonCamera;
    public Camera p2CannonCamera;

	public GameObject player1;
	public GameObject player2;

	public string currentPlayerTag;

	Vector3 p1CameraPos;
	Vector3 p2CameraPos;
	Quaternion p1CameraRot;
	Quaternion p2CameraRot;

	int cameraThrust;

    float p1RotationX;
    float p2RotationX;
    float p1RotationY;
    float p2RotationY;

	float rotationX;
	float rotationY;

	void Awake()
	{
		currentPlayerTag = player1.tag;

        p1CannonCamera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p1CannonCamera.depth = mainCamera.depth + 1;

        p2CannonCamera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p2CannonCamera.depth = mainCamera.depth + 1;
	}

	void Start()
	{
		cameraThrust = 1;
		p1RotationX = rotationX = 225f;
		p1RotationX = p2RotationY = rotationY = 5f;

        p2RotationX = 45f;

        p1CameraPos = 
            firstPersonCamera.transform.position = 
            new Vector3(-40f, 15f, -40);
        p2CameraPos = new Vector3(40f, 15f, 40f);

        p1CameraRot =
            firstPersonCamera.transform.rotation = 
            Quaternion.Euler(10, 45, 0);
        p2CameraRot = Quaternion.Euler(10, 225, 0);

		mainCamera.enabled = false;
        p2CannonCamera.enabled = false;
		firstPersonCamera.enabled = true;
	}

	void FixedUpdate()
	{
		mainCamera.enabled = false;
		firstPersonCamera.enabled = true;

        if (Input.GetKeyDown(KeyCode.Escape))
            swapPlayers();

		if (Input.GetMouseButton (1)) {
			Rotate();
			Move();
		} else if (Input.GetMouseButton (2))  {
			mainCamera.enabled = true;
			firstPersonCamera.enabled = false;
		}
	}

	void Move()
	{
		Vector3 upDown = new Vector3(0, 1, 0);
		Vector3 forward = firstPersonCamera.transform.forward * Input.GetAxis("Vertical");
		Vector3 right = firstPersonCamera.transform.right * Input.GetAxis("Horizontal");

		firstPersonCamera.transform.position += forward * cameraThrust;
		firstPersonCamera.transform.position += right * cameraThrust;

		if (Input.GetKey("e"))
			firstPersonCamera.transform.position += upDown;
		else if (Input.GetKey("q"))
			firstPersonCamera.transform.position -= upDown;
	}

	void Rotate()
	{
		rotationX += Input.GetAxis("Mouse X");
		rotationY += Input.GetAxis("Mouse Y");
		rotationY = Mathf.Clamp (rotationY, -90, 90);
		
		firstPersonCamera.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		firstPersonCamera.transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
	}

	public void swapPlayers()
	{
		if (player1.CompareTag(currentPlayerTag)) {
			p1CameraPos = firstPersonCamera.transform.position;
			p1CameraRot = firstPersonCamera.transform.rotation;

			firstPersonCamera.transform.position = p2CameraPos;
			firstPersonCamera.transform.rotation = p2CameraRot;

            p1CannonCamera.enabled = false;
            p2CannonCamera.enabled = true;

            p1RotationX = rotationX;
            p1RotationY = rotationY;

            rotationX = p2RotationX;
            rotationY = p2RotationY;

			currentPlayerTag = player2.tag;
            player2.GetComponent<PlayerController>().shot = false;
		} else {
			p2CameraPos = firstPersonCamera.transform.position;
			p2CameraRot = firstPersonCamera.transform.rotation;
			
			firstPersonCamera.transform.position = p1CameraPos;
			firstPersonCamera.transform.rotation = p1CameraRot;

            p2CannonCamera.enabled = false;
            p1CannonCamera.enabled = true;

            p2RotationX = rotationX;
            p2RotationY = rotationY;

            rotationX = p1RotationX;
            rotationY = p1RotationY;
			
			currentPlayerTag = player1.tag;
            player1.GetComponent<PlayerController>().shot = false;
		}
	}

    public void ShotHit(GameObject hit)
    {
        Debug.Log("Hit: " + hit.name + " - " + hit.tag);
    }
}
