using UnityEngine;

namespace Source.Blocks
{
    public class Block
    {

        public int blockID;
        
        public Block(int blockID, Sprite sprite)
        {
            this.blockID = blockID;
        }
        
        //Getters

        public int getBlockID()
        {
            return blockID;
        }
    }
}