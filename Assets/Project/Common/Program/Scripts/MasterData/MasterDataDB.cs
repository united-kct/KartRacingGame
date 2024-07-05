using Generated.MasterData;
using UnityEditor;
using UnityEngine;

namespace Common.MasterData
{
    public static class MasterDataDB
    {
        public static readonly MemoryDatabase DB = new(
            AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Project/Common/Program/MasterData/master_data.bytes").bytes
            );
    }
}