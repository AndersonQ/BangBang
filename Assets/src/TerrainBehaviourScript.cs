using UnityEngine;
using System.Collections;

public class TerrainBehaviourScript : MonoBehaviour {
	//public Terrain terrain;
	private float[,] heightsMatrix;
	
	public int xBase = 0;
	public int yBase = 0;
	private int xResolution , zResolution;
	// Use this for initialization
	void Start () {

		//Terrain terrain = this.GetComponents<Terrain>()[0];
		Terrain terrain = Terrain.activeTerrain;
		xResolution = terrain.terrainData.heightmapWidth;
		zResolution = terrain.terrainData.heightmapHeight;
		heightsMatrix = terrain.terrainData.GetHeights(0, 0, xResolution,zResolution);

		/*
		this.terrain.terrainData.
		this.terrain.terrainData.SetHeights(0, 0, this.originalHeights);
		*/
		int i, j = 0;
		i = 0;
		//for (i=0; i<terrain.terrainData.heightmapHeight/10; i++) {
		//	for(j=0;j<terrain.terrainData.heightmapWidth/10;j++)
		for (i=0; i<xResolution; i++) {
			for(j=0;j<zResolution;j++)
			{
				heightsMatrix[i,j] = Mathf.Cos(i) - Mathf.Cos(j);
				//originalHeights[30,30] = 100;

			}
		}

		terrain.terrainData.SetHeights(0, 0, heightsMatrix);

	}


	// Update is called once per frame
	void Update () {
	
	}
}
