using UnityEngine;
using System.Collections;

public class WaterBehaviour : MonoBehaviour {
	//public Terrain terrain;
	private float[,] heightsMatrix;
	public float lakeWaveVel = 0.1f;
	private int xResolution , zResolution;
	private Terrain water;
	
	// Use this for initialization
	void Start () {

		water = this.GetComponent<Terrain> ();
		xResolution = water.terrainData.heightmapWidth;
		zResolution = water.terrainData.heightmapHeight;
		heightsMatrix = water.terrainData.GetHeights(0, 0, xResolution,zResolution);
	}
	

	void Update () {

		ResetHeightMatrix ();
		water.terrainData.SetHeights(0, 0, heightsMatrix);
		PerlinNoiseChangeMatrix ();
		water.terrainData.SetHeights(0, 0, heightsMatrix);

	}
	/*
	void FixedUpdate () {
		
		ResetHeightMatrix ();
		water.terrainData.SetHeights(0, 0, heightsMatrix);
		PerlinNoiseChangeMatrix ();
		water.terrainData.SetHeights(0, 0, heightsMatrix);
		
	}
	 */

	private void ResetHeightMatrix()
	{
		int i, j = 0;
		i = 0;
		
		for (i=0; i<xResolution; i++) {
			for(j=0;j<zResolution;j++)
			{
				heightsMatrix[i,j] = 0;
			}
		}
		
	}
	
	private void PerlinNoiseChangeMatrix()
	{
		int i, j = 0;
		i = 0;
		float scale = water.terrainData.heightmapHeight;
		float timeFactor = Time.time*0.1f; //Tempo multiplicado por um fator simulando a velocidade da pertubacao
		float simpleFactor = 10f;

		if (timeFactor > 100f) // Ajuste na scala da funcao perlinNoise
			timeFactor = 10f + Mathf.Floor (timeFactor) % 10;


		for (i=0; i<xResolution; i++) {
			for(j=0;j<zResolution;j++)
			{
				heightsMatrix[i,j] = Mathf.PerlinNoise((simpleFactor*timeFactor*i)/scale,(simpleFactor*timeFactor*j)/scale);
			}
		}
		
	}
}
