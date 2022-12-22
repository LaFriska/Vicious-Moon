using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TerrainGen : MonoBehaviour
{
    
    public Sprite desolateRock;
    public Sprite desolateDirt;
    public int desolateDirtHeight = 5;
    
    //Size of the world.
    public int horizontalWorldSize = 100;
    public int verticalWorldSize = 100;
    //Noise frequency changes how frequent hills are generated.
    public float terrainFreq = 0.07F;
    //Height multiplier determines how tall hills are.
    public float heightMultiplier = 4f;
    //How deep the world is.
    public int heightAddition = 25;
    //Seed
    public float seed = 0;
    
    //Caves
    public float caveFreq = 0.5F;
    public float caveSize = 0.7F;
    public int caveDepth = 20;
    
    
    
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
                if (y < height - desolateDirtHeight)
                {
                    if (noiseTexture.GetPixel(x, y).r > caveSize || y >= height - caveDepth) GenerateTile(x,y, desolateRock);
                }
                else
                {
                    GenerateTile(x,y, desolateDirt);
                }
            }
        }
    }

    private void GenerateTile(int x, int y, Sprite sprite)
    {
        GameObject newTile = new GameObject(name = "tile");
        newTile.transform.parent = this.transform;
        //newTile.transform.localScale = new Vector3(6.25F, 6.25F, 6.25F);
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = sprite;
        newTile.name = sprite.name;
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
