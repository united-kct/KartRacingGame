using Generated.MasterData;
using UnityEngine;

namespace Common.MasterData
{
    public static class MasterDataDB
    {
        public static readonly MemoryDatabase DB = new(Resources.Load<TextAsset>("master-data").bytes);
    }
}