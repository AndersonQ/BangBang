using UnityEngine;
using System.Collections;

public class CenarioBehaviour : MonoBehaviour {
	private Terrain cenarioTerrain;
	private float[,] heightsMatrix;
	private int xResolution , zResolution;

	// Use this for initialization
	void Start () {
		cenarioTerrain = this.GetComponent<Terrain> ();

		xResolution = cenarioTerrain.terrainData.heightmapWidth;
		zResolution = cenarioTerrain.terrainData.heightmapHeight;
		heightsMatrix = cenarioTerrain.terrainData.GetHeights (0, 0, xResolution,zResolution);
		heightsMatrix [0, 0] = 6000f;

		cenarioTerrain.terrainData.SetHeights (0,0,heightsMatrix);

		Vector3 V = cenarioTerrain.terrainData.treeInstances[0].position;
		Debug.Log (V.x);
		Debug.Log (V.y);
		Debug.Log (V.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
