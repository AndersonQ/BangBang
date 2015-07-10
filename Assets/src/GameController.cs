using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Camera firstPersonCamera;
	public Camera mainCamera;
    public Camera p1Camera;
    public Camera p2Camera;

	public GameObject player1;
	public GameObject player2;

	public string currentPlayerTag;


	Vector3 p1CameraPos;
	Vector3 p2CameraPos;
	Quaternion p1CameraRot;
	Quaternion p2CameraRot;

	int cameraThrust;

	float rotationX;
	float rotationY;

	void Awake()
	{
		currentPlayerTag = player1.tag;

        p1Camera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p1Camera.depth = mainCamera.depth + 1;

        p2Camera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p2Camera.depth = mainCamera.depth + 1;
	}

	void Start()
	{
		cameraThrust = 1;
		rotationX = 225f;
		rotationY = 5f;

		p1CameraPos = p2CameraPos = firstPersonCamera.transform.position;
        p1CameraRot = Quaternion.Euler(5, 225, 0);
        p2CameraRot = Quaternion.Euler(5, 45, 0);

		mainCamera.enabled = false;
        p2Camera.enabled = false;
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

	void swapPlayers()
	{
		if (player1.CompareTag(currentPlayerTag)) {
			p1CameraPos = firstPersonCamera.transform.position;
			p1CameraRot = firstPersonCamera.transform.rotation;

			firstPersonCamera.transform.position = p2CameraPos;
			firstPersonCamera.transform.rotation = p2CameraRot;

            p1Camera.enabled = false;
            p2Camera.enabled = true;

			currentPlayerTag = player2.tag;
		} else {
			p2CameraPos = firstPersonCamera.transform.position;
			p2CameraRot = firstPersonCamera.transform.rotation;
			
			firstPersonCamera.transform.position = p1CameraPos;
			firstPersonCamera.transform.rotation = p1CameraRot;

            p2Camera.enabled = false;
            p1Camera.enabled = true;
			
			currentPlayerTag = player1.tag;
		}
	}
}
