using Unity.VisualScripting;
using UnityEngine;

namespace Source.Blocks
{
    public class Block
    {

        public GameObject parent;

        private int blockID;
        private Sprite sprite;

        public Block(int blockID, Sprite sprite)
        {
            this.blockID = blockID;
            this.sprite = sprite;
            parent = new GameObject(sprite.name + "_parent");
        }

        public void SetBlock(float x, float y)
        {
            GameObject newTile = new GameObject("tile");
            newTile.transform.parent = parent.transform;
            //newTile.transform.localScale = new Vector3(6.25F, 6.25F, 6.25F);
            newTile.AddComponent<SpriteRenderer>();
            newTile.GetComponent<SpriteRenderer>().sprite = sprite;
            newTile.AddComponent<Variables>();
            newTile.GetComponent<Variables>().declarations.Set("blockID", blockID);
            newTile.name = sprite.name;
            newTile.transform.position = new Vector2(x, y);
        }

        //Getters

        public int getBlockID()
        {
            return blockID;
        }
    }
}