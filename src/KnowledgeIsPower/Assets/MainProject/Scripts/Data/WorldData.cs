using System;

namespace MainProject.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public Vector3Data Position;
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}