﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public bool gameOver;

	public Camera freeFlyingCamera;
	public Camera topCamera;
    public Camera p1CannonCamera;
    public Camera p2CannonCamera;

	public GameObject player1;
	public GameObject player2;
    public GameObject currentPlayer;
    public GameObject enemyPlayer;
    public Text playerWinsTxt;

	public GameObject explosionPrefab;

	public string currentPlayerTag;

	GameObject explosion;

    AudioSource explosionSound;

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
        gameOver = false;

		currentPlayerTag = player1.tag;
        currentPlayer = player1;
        enemyPlayer = player2;

        topCamera.rect = new Rect(0.015f, 0.01f, 0.215f, 0.215f);
        topCamera.depth = freeFlyingCamera.depth + 1;

        p1CannonCamera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p1CannonCamera.depth = freeFlyingCamera.depth + 1;

        p2CannonCamera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p2CannonCamera.depth = freeFlyingCamera.depth + 1;

        topCamera.enabled = true;
        freeFlyingCamera.enabled = true;
        p2CannonCamera.enabled = false;

        explosionSound = GetComponent<AudioSource>();
	}

	void Start()
	{
		cameraThrust = 1;
		p1RotationX = rotationX = 50f;
		p1RotationY = p2RotationY = rotationY = 0f;

        p2RotationX = 225f;

        p1CameraPos = 
            freeFlyingCamera.transform.position = 
            new Vector3(-40f, 15f, -40);
        p2CameraPos = new Vector3(40f, 15f, 40f);

        p1CameraRot =
            freeFlyingCamera.transform.rotation = 
            Quaternion.Euler(10, 45, 0);
        p2CameraRot = Quaternion.Euler(10, 225, 0);

	}

	void Update()
	{
		if (Input.GetMouseButton (1)) 
        {
			Rotate();
			Move();
		}
        if (Input.GetKey(KeyCode.Escape))
            ShotHit(currentPlayer);

	}

	void Move()
	{
		Vector3 upDown = new Vector3(0, 1, 0);
		Vector3 forward = freeFlyingCamera.transform.forward * Input.GetAxis("Vertical");
		Vector3 right = freeFlyingCamera.transform.right * Input.GetAxis("Horizontal");

		freeFlyingCamera.transform.position += forward * cameraThrust;
		freeFlyingCamera.transform.position += right * cameraThrust;

		if (Input.GetKey("e"))
			freeFlyingCamera.transform.position += upDown;
		else if (Input.GetKey("q"))
			freeFlyingCamera.transform.position -= upDown;
	}

	void Rotate()
	{
		rotationX += Input.GetAxis("Mouse X");
		rotationY += Input.GetAxis("Mouse Y");
		rotationY = Mathf.Clamp (rotationY, -90, 90);
			
		freeFlyingCamera.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		freeFlyingCamera.transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
	}

	public void swapPlayers()
	{
		if (player1.CompareTag(currentPlayerTag)) 
        {
			p1CameraPos = freeFlyingCamera.transform.position;
			p1CameraRot = freeFlyingCamera.transform.rotation;

			freeFlyingCamera.transform.position = p2CameraPos;
			freeFlyingCamera.transform.rotation = p2CameraRot;

            p1CannonCamera.enabled = false;
            p2CannonCamera.enabled = true;

            p1RotationX = rotationX;
            p1RotationY = rotationY;

            rotationX = p2RotationX;
            rotationY = p2RotationY;

			currentPlayerTag = player2.tag;
            currentPlayer = player2;
            enemyPlayer = player1;
            player2.GetComponent<PlayerController>().shot = false;
		}
        else 
        {
			p2CameraPos = freeFlyingCamera.transform.position;
			p2CameraRot = freeFlyingCamera.transform.rotation;
			
			freeFlyingCamera.transform.position = p1CameraPos;
			freeFlyingCamera.transform.rotation = p1CameraRot;

            p2CannonCamera.enabled = false;
            p1CannonCamera.enabled = true;

            p2RotationX = rotationX;
            p2RotationY = rotationY;

            rotationX = p1RotationX;
            rotationY = p1RotationY;
			
			currentPlayerTag = player1.tag;
            currentPlayer = player1;
            enemyPlayer = player2;
            player1.GetComponent<PlayerController>().shot = false;
		}
	}

    public void ShotHit(GameObject hit)
    {
        if (hit != null && hit.tag.Contains("Player"))
        {
            char winner = currentPlayerTag[currentPlayerTag.Length - 1];
            gameOver = true;
            playerWinsTxt.text = "Player " + winner + " wins!";

            explosionSound.timeSamples = 44100*2;
            explosionSound.Play();
            explosion = (GameObject)Instantiate(explosionPrefab,
                                                            hit.transform.position,
                                                            Quaternion.identity);
            
            Destroy(hit);
            StartCoroutine("explosionGrow");
        }
    }

	IEnumerator explosionGrow()
	{
		float startTime = Time.time;
		while (Time.time < startTime + 1.5)
		{
            float lerpStep = (Time.time - startTime) / 1.5f;
            float lerp = Mathf.Lerp(3f, 15f, lerpStep);
			explosion.transform.localScale = new Vector3(lerp, lerp, lerp);
			yield return null;
		}
        Destroy(explosion);
        explosionSound.Stop();
        yield break;
	}

    public void setMainCameraEnable(bool enable)
    {
        freeFlyingCamera.enabled = enable;
    }
}
