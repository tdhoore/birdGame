using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour {

    public GameObject[] terrainTiles;

    public GameObject player;

    public GameObject tileHolder;
    public int numbOfStartTiles = 0;
    public float tileSize = 0;
    private float starterZ = 0;

    void Awake() {
        //gen first set of tiles
        GenStarterTerrain();
    }

    void Update() {
        GenNewTerrain();
    }

    void GenNewTerrain() {
        if(player.transform.position.z > starterZ + (float)(tileSize)) {
            //set new startPos
            starterZ += (float)(tileSize);

            //remove old terrain
            Destroy(tileHolder.transform.GetChild(0).gameObject);
            Destroy(tileHolder.transform.GetChild(1).gameObject);

            //create new terain
            PlaceTiles(starterZ + ((numbOfStartTiles - 1) * tileSize), Get2Tiles());
        }
    }

    void GenStarterTerrain() {
        starterZ = (float)(-tileSize * 1.5);

        for (int i = 0; i < numbOfStartTiles; i++) {
            PlaceTiles(starterZ + (i * tileSize), Get2Tiles());
        }
    }

    void PlaceTiles(float z, List<GameObject> tiles) {
        GameObject tile1 = Instantiate(tiles[0], new Vector3(-tileSize / 2, 0, z), Quaternion.identity);
        GameObject tile2 = Instantiate(tiles[1], new Vector3(tileSize / 2, 0, z), Quaternion.Euler(0, 180, 0));

        //place in holder
        if (tileHolder != null) {
            tile1.transform.parent = tileHolder.transform;
            tile2.transform.parent = tileHolder.transform;
        }
    }

    List<GameObject> Get2Tiles() {
        List<GameObject> tiles = new List<GameObject> {
            terrainTiles[(int)UnityEngine.Random.Range(0, terrainTiles.Length)],
            terrainTiles[(int)UnityEngine.Random.Range(0, terrainTiles.Length)]
        };

        return tiles;
    }
}
