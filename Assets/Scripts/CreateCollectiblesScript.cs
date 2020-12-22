using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCollectiblesScript : MonoBehaviour
{
    public int numberOfPositiveCollectibles = 5;
    public int numberOfNegativeCollectibles = 5;

    public Transform positiveCollectiblePrefab;
    public Transform negativeCollectiblePrefab;

    private Terrain terrain;
    private TerrainData terrain_data;
    private int heightmap_width;
    private int heightmap_height;
    private float[,] heightmap_data;

    Vector3 randomPosition(){
        int x, y, z, xTerrain, zTerrain;
        x = (int)Random.Range(0,terrain_data.size.x);
        z = (int)Random.Range(0,terrain_data.size.z);
        //xTerrain = (int)((x + heightmap_width) % heightmap_width);
        //zTerrain = (int)((z + heightmap_height) % heightmap_height);
        y = (int)terrain_data.GetHeight(z,x);
        y = (int)terrain.SampleHeight(new Vector3(x,y,z));
        return new Vector3(x,y+1,z);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!terrain)
            terrain = Terrain.activeTerrain;
        
        terrain_data = terrain.terrainData;

        heightmap_width = terrain_data.heightmapResolution;
        heightmap_height = terrain_data.heightmapResolution;
        heightmap_data = terrain_data.GetHeights(0, 0, heightmap_width, heightmap_height);



        for(int i=0 ; i< numberOfPositiveCollectibles ; i++){
            Instantiate(positiveCollectiblePrefab, randomPosition(), Quaternion.identity);
        }
        for(int i=0 ; i< numberOfNegativeCollectibles ; i++){
            Instantiate(negativeCollectiblePrefab, randomPosition(), Quaternion.identity);        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
