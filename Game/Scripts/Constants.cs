using System;
using UnityEngine;

namespace Game.Scripts
{
    public class Constants
    {
        public static readonly int gridMaxX = 12;
        public static readonly int gridMaxY = 8;

        public static readonly int tileSizeX = 2;
        public static readonly int tileSizeY = 3;
        public static readonly int tileSizeZ = 3;

        public static readonly int gridStartPosX = 0;
        public static readonly int gridStartPosY = 0;
        public static readonly Vector3 TileSize = new Vector3(tileSizeX, tileSizeY, tileSizeZ);


        public class Dir
        {
            public static readonly String GAME_DATA = Application.dataPath + "/Game/Data/";
            public static readonly String STREAMING_ASSETS = Application.streamingAssetsPath;
        }
    }
}