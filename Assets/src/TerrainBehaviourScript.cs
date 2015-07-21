using UnityEngine;
using System.Collections;

public class TerrainBehaviourScript : MonoBehaviour {
	//public Terrain terrain;
	private float[,] originalHeights;
	
	public int xBase = 0;
	public int yBase = 0;

	// Use this for initialization
	void Start () {

		//Terrain terrain = this.GetComponents<Terrain>()[0];
		Terrain terrain = Terrain.activeTerrain;
		originalHeights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth,
		                                                 terrain.terrainData.heightmapHeight);

		/*
		this.terrain.terrainData.
		this.terrain.terrainData.SetHeights(0, 0, this.originalHeights);
		*/
		int i, j = 0;
		i = 0;
		Debug.Log(terrain.terrainData.heightmapWidth);
		Debug.Log(terrain.terrainData.heightmapHeight);	
		//for (i=0; i<terrain.terrainData.heightmapHeight/10; i++) {
		//	for(j=0;j<terrain.terrainData.heightmapWidth/10;j++)
		for (i=0; i<513; i++) {
			for(j=0;j<513;j++)
			{
				Debug.Log((float)i/513.0f);
				Debug.Log((float)j/513.0f);
				originalHeights[i,j] = (Mathf.PerlinNoise((float)i/513.0f,(float)j/513.0f) - Mathf.Floor(Mathf.PerlinNoise((float)i/513.0f,(float)j/513.0f)))*100;
				//originalHeights[30,30] = 100;

			}
		}

		terrain.terrainData.SetHeights(0, 0, originalHeights);

	}


	// Update is called once per frame
	void Update () {
	
	}
}
