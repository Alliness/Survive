using System.Collections.Generic;
using Game.Scripts.Building;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class TileManager : MonoBehaviour
    {
        [HideInInspector] public static TileManager instance;

        public GameObject tileBlock;
        public GameObject[,] blocks;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            blocks = new GameObject[Constants.gridMaxX, Constants.gridMaxY];


            //create grid array
            for (int x = 0; x < Constants.gridMaxX; x++)
            {
                for (int y = 0; y < Constants.gridMaxY; y++)
                {
                    GameObject currentBlock = Instantiate(tileBlock,
                            new Vector3(x * Constants.tileSizeX, y * Constants.tileSizeY, transform.position.z),
                            Quaternion.identity);
                    currentBlock.transform.SetParent(transform);
                    blocks[x, y] = currentBlock;
                }
            }            
        }

        public bool isFree(int x, int y)
        {
            return !blocks[x, y].GetComponent<TileBlock>().Occupied;
        }


        public List<GameObject> getAvailableToBuildArea(Enums.RoomSize roomSize)
        {
            List<GameObject> freeBlocks = new List<GameObject>();

            for (int y = 0; y < Constants.gridMaxY; y++)
            {
                for (int x = 0; x < Constants.gridMaxX; x++)
                {
                    var currentBlock = blocks[x, y];
                    var currentTile = currentBlock.GetComponent<TileBlock>();

                    if (currentTile.Occupied)
                    {
                        bool left = xIsValid(x - 1);
                        bool right = xIsValid(x + 1);
                        bool down = yIsFalid(y - 1);
                        bool up = yIsFalid(y + 1);

                        //if target block is elevator,  and we want to build elevator - validate possibility to build on up/down sector
                        if (currentTile.isElevator && roomSize == Enums.RoomSize.ROOM_1)
                        {
                            if (down && isFree(x, y - 1))
                            {
                                freeBlocks.Add(blocks[x, y - 1]);
                            }
                            if (up && isFree(x, y + 1))
                            {
                                freeBlocks.Add(blocks[x, y + 1]);
                            }
                        }

                        if (right)
                        {
                            int nextX = x + 1;
                            var nextBlock = blocks[nextX, y];
                            var nextTile = nextBlock.GetComponent<TileBlock>();

                            if (!nextTile.Occupied && validateRightSectorFit(nextX, y, roomSize))
                            {
                                freeBlocks.Add(nextBlock);
                            }
                        }
                        if (left)
                        {
                            int prevX = x - 1;
                            var prevTile = blocks[prevX, y].GetComponent<TileBlock>();
                            if (!prevTile.Occupied && validateLeftSectorFit(prevX, y, roomSize))
                            {
                                freeBlocks.Add(blocks[x - (int) roomSize, y]);
                            }
                        }
                    }
                }
            }
            return freeBlocks;
        }


        /**
     * validate right side sectors (object can fit to left0
     */
        bool validateRightSectorFit(int startX, int y, Enums.RoomSize roomSize)
        {
            for (int i = 0; i < (int) roomSize; i++)
            {
                if (!xIsValid(startX + i))
                {
                    return false;
                }
                var block = blocks[startX + i, y];
                if (block.GetComponent<TileBlock>().Occupied)
                {
                    return false;
                }
            }
            return true;
        }


        /**
     * validate left side free sectors (object can fit to right)
     */
        bool validateLeftSectorFit(int prevX, int y, Enums.RoomSize roomSize)
        {
            for (int i = 0; i < (int) roomSize; i++)
            {
                if (!xIsValid(prevX - i))
                {
                    return false;
                }
                var block = blocks[prevX - i, y];
                if (block.GetComponent<TileBlock>().Occupied)
                {
                    return false;
                }
            }
            return true;
        }

        /**
     * (min) <= (x) <= (max)
     */
        bool xIsValid(int x)
        {
            return x >= 0 && x < Constants.gridMaxX;
        }

        /**
     * (down) < y > (up)
     */
        bool yIsFalid(int y)
        {
            return y >= 0 && y < Constants.gridMaxY;
        }

    }
}