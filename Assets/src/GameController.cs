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
    public Text playingTxt;

	public GameObject explosionPrefab;

	public string currentPlayerTag;

	GameObject explosion;

    AudioSource explosionSound;

	Vector3 p1CameraPos;
	Vector3 p2CameraPos;
	Quaternion p1CameraRot;
	Quaternion p2CameraRot;

	int cameraThrust;

    float p1CameraRotationX;
	float p2CameraRotationX;
	float p1CameraRotationY;
	float p2CameraRotationY;

	float camCurrentRotationX;
	float amCurrentRotationY;

    string[] freeCameraCheat;
    bool freeCameraCheatOn;

    int cheatIndex;
    float lastKeyPressedAt;
    private float clearCheatAfterSeconds;

	void Awake()
	{
        gameOver = false;

		currentPlayerTag = player1.tag;
        playingTxt.text = "Player " + currentPlayerTag[currentPlayerTag.Length - 1];
        currentPlayer = player1;
        enemyPlayer = player2;

        topCamera.rect = new Rect(0.015f, 0.01f, 0.215f, 0.215f);
        topCamera.depth = freeFlyingCamera.depth + 1;

        p1CannonCamera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p1CannonCamera.depth = freeFlyingCamera.depth + 1;
		p1CannonCamera.GetComponent<AudioListener>().enabled = true;

        p2CannonCamera.rect = new Rect(0.78f, 0.025f, 0.2f, 0.2f);
        p2CannonCamera.depth = freeFlyingCamera.depth + 1;

        topCamera.enabled = true;
        freeFlyingCamera.enabled = true;
        p2CannonCamera.enabled = false;

        explosionSound = GetComponent<AudioSource>();

        prepareCheats();
	}

	void Start()
	{
		cameraThrust = 1;
		p1CameraRotationX = camCurrentRotationX = 50f;
		p1CameraRotationY = p2CameraRotationY = amCurrentRotationY = 0f;

        p2CameraRotationX = 225f;

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
        if (Input.GetMouseButton(1) && freeCameraCheatOn) 
        {
			Rotate();
			Move();
		}

        // Debug only
        if (Input.GetKey(KeyCode.Escape))
            ShotHit(enemyPlayer);

        updateCheat();
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
		camCurrentRotationX += Input.GetAxis("Mouse X");
		amCurrentRotationY += Input.GetAxis("Mouse Y");
		amCurrentRotationY = Mathf.Clamp (amCurrentRotationY, -90, 90);
			
		freeFlyingCamera.transform.localRotation = Quaternion.AngleAxis(camCurrentRotationX, Vector3.up);
		freeFlyingCamera.transform.localRotation *= Quaternion.AngleAxis(amCurrentRotationY, Vector3.left);
	}

	public void swapPlayers()
	{
		if (player1 == null || gameOver)
			return;

		if (player1.CompareTag(currentPlayerTag)) 
        {
			p1CameraPos = freeFlyingCamera.transform.position;
			p1CameraRot = freeFlyingCamera.transform.rotation;

			freeFlyingCamera.transform.position = p2CameraPos;
			freeFlyingCamera.transform.rotation = p2CameraRot;

			p1CannonCamera.GetComponent<AudioListener>().enabled = false;
            p1CannonCamera.enabled = false;
			p2CannonCamera.enabled = true;
			p2CannonCamera.GetComponent<AudioListener>().enabled = true;

            p1CameraRotationX = camCurrentRotationX;
            p1CameraRotationY = amCurrentRotationY;

            camCurrentRotationX = p2CameraRotationX;
            amCurrentRotationY = p2CameraRotationY;

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

			p2CannonCamera.GetComponent<AudioListener>().enabled = false;
            p2CannonCamera.enabled = false;
            p1CannonCamera.enabled = true;
			p1CannonCamera.GetComponent<AudioListener>().enabled = true;

            p2CameraRotationX = camCurrentRotationX;
            p2CameraRotationY = amCurrentRotationY;

            camCurrentRotationX = p1CameraRotationX;
            amCurrentRotationY = p1CameraRotationY;
			
			currentPlayerTag = player1.tag;
            currentPlayer = player1;
            enemyPlayer = player2;
            player1.GetComponent<PlayerController>().shot = false;
		}

        playingTxt.text = "Player " + currentPlayerTag[currentPlayerTag.Length - 1];
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

    private void updateCheat()
    {
        if (Input.anyKey)
            lastKeyPressedAt = Time.time;

        if (Time.time - lastKeyPressedAt > clearCheatAfterSeconds)
        {
            cheatIndex = 0;
            return;
        }

        if (Input.GetKeyUp(freeCameraCheat[cheatIndex]))
        {
            cheatIndex++;
            if (cheatIndex == freeCameraCheat.Length)
            {
                freeCameraCheatOn = true;
                cheatIndex = 0;
            }

            int index = cheatIndex > 0 ? cheatIndex : 1;
            Debug.Log("Cheat: " + freeCameraCheat[index - 1]);
        }
    }

    private void prepareCheats()
    {
        freeCameraCheat = new string[] { "i", "d", "c", "l", "i", "p"};//"idclip";
        freeCameraCheatOn = false;

        cheatIndex = 0;
        clearCheatAfterSeconds = 1;
    }
}
