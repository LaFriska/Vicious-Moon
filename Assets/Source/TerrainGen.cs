using System;
using Source.Blocks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source
{
    public class TerrainGen : MonoBehaviour
    {
        public Sprite desolateRock;
        public Sprite desolateDirt;
        public int desolateDirtHeight = 5;

        public Block DesolateRockBlock;
        public Block DesolateDirtBlock;

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

            DesolateRockBlock = new Block(1, desolateRock);
            DesolateDirtBlock = new Block(2, desolateDirt);

            GenerateNoiseTexture();
            GenerateTerrain();
        }

        public void GenerateTerrain()
        {
            for (int x = 0; x <= horizontalWorldSize - 1; x++)
            {
                float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier +
                               heightAddition;

                for (int y = 0; y <= height - 1; y++)
                {
                    if (y < height - desolateDirtHeight)
                    {
                        if (noiseTexture.GetPixel(x, y).r > caveSize || y >= height - caveDepth)
                            DesolateRockBlock.SetBlock(x, y);
                    }
                    else
                    {
                        DesolateDirtBlock.SetBlock(x, y);
                    }
                }
            }
        }

        public void GenerateNoiseTexture()
        {
            noiseTexture = new Texture2D(horizontalWorldSize, verticalWorldSize);

            for (int x = 0; x <= noiseTexture.width - 1; x++)
            {
                for (int y = 0; y <= noiseTexture.height - 1; y++)
                {
                    float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * terrainFreq);
                    noiseTexture.SetPixel(x, y, new Color(v, v, v));
                }
            }

            noiseTexture.Apply();
        }
    }
}