using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TerrainGen : MonoBehaviour
{

    public Sprite tile;
    
    //Size of the world.
    public int horizontalWorldSize = 100;
    public int verticalWorldSize = 100;
    //Noise frequency changes how frequent hills are generated.
    [FormerlySerializedAs("noiseFreq")] public float terrainFreq = 0.07F;
    //Height multiplier determines how tall hills are.
    public float heightMultiplier = 4f;
    //How deep the world is.
    public int heightAddition = 25;
    //Seed
    public float seed = 0;
    //How often caves generate
    public float caveFreq = 0.5F;
    //Size of the caves
    public float caveSize = 0.7F;
    
    
    
    public Texture2D noiseTexture;

    public void Start()
    {
        seed = UnityEngine.Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x <= horizontalWorldSize - 1; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;
            
            for (int y = 0; y <= height - 1; y++)
            {
                if (noiseTexture.GetPixel(x, y).r > caveSize) generateTile(x,y);
            }
        }
    }

    private void generateTile(int x, int y)
    {
        GameObject newTile = new GameObject(name = "tile");
        newTile.transform.parent = this.transform;
        newTile.transform.localScale = new Vector3(3.125F, 3.125F, 3.125F);
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = tile;
        newTile.transform.position = new Vector2(x + 0.5F, y + 0.5F);
    }

    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(horizontalWorldSize, verticalWorldSize);

        for (int x = 0; x <= noiseTexture.width - 1; x++)
        {
            for (int y = 0; y <= noiseTexture.height - 1; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * terrainFreq);
                noiseTexture.SetPixel(x,y,new Color(v, v, v));
            }
        }
        
        noiseTexture.Apply();
    }
}
