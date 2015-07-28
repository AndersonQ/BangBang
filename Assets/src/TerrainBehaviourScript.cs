using UnityEngine;
using System.Collections;

public class TerrainBehaviourScript : MonoBehaviour {
	//public Terrain terrain;
	private float[,] heightsMatrix;
	
	public int xBase = 0;
	public int yBase = 0;
	private int xResolution , zResolution;
	private Terrain terrain;

	// Use this for initialization
	void Start () {
		terrain = Terrain.activeTerrain;
		terrain = this.GetComponent<Terrain>();
		xResolution = terrain.terrainData.heightmapWidth;
		zResolution = terrain.terrainData.heightmapHeight;
		heightsMatrix = terrain.terrainData.GetHeights(0, 0, xResolution,zResolution);
		PerlinNoiseChangeMatrix ();



		//ResetHeightMatrix ();
		terrain.terrainData.SetHeights(0, 0, heightsMatrix);
	}

		// Update is called once per frame
	void Update () {
		/*
		ResetHeightMatrix ();
		terrain.terrainData.SetHeights(0, 0, heightsMatrix);
		PerlinNoiseChangeMatrix ();
		terrain.terrainData.SetHeights(0, 0, heightsMatrix);
		*/

	}

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
		float scale = terrain.terrainData.heightmapHeight;
		float timeFactor = Time.time*1;
		float simpleFactor = 10f;
		timeFactor = 1f;
		//simpleFactor = 1f;
		for (i=0; i<xResolution; i++) {
			for(j=0;j<zResolution;j++)
			{
				heightsMatrix[i,j] = Mathf.PerlinNoise((simpleFactor*timeFactor*i)/scale,(simpleFactor*timeFactor*j)/scale);
			}
		}

	}
}
