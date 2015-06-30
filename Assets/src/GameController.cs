using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Camera playerCamera;
	public Camera mainCamera;
	public int cameraThrust;
	public int playing;
	public int turn;

	float rotationX;
	float rotationY;

	void Start()
	{
		playing = 0;

		rotationX = 225f;
		rotationY = 5f;

		//Swap camera
		mainCamera.enabled = false;
		playerCamera.enabled = true;

	}

	void FixedUpdate()
	{
		mainCamera.enabled = false;
		playerCamera.enabled = true;

		if (Input.GetMouseButton (1)) {

			Rotate();
			Move();

		} else if (Input.GetMouseButton (2))  {
			mainCamera.enabled = true;
			playerCamera.enabled = false;
		}
	}

	void Move() {
		Vector3 forward = playerCamera.transform.forward * Input.GetAxis("Vertical");
		Vector3 right = playerCamera.transform.right * Input.GetAxis("Horizontal");

		playerCamera.transform.position += forward * cameraThrust;
		playerCamera.transform.position += right * cameraThrust;
	}

	void Rotate() {
		rotationX += Input.GetAxis("Mouse X");
		rotationY += Input.GetAxis("Mouse Y");
		rotationY = Mathf.Clamp (rotationY, -90, 90);
		
		playerCamera.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		playerCamera.transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
	}
}
