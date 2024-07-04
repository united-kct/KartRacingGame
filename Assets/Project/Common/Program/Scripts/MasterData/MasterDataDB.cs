using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generated.MasterData;

namespace Common.MasterData
{
    public static class MasterDataDB
    {
        public static readonly MemoryDatabase DB = new(Resources.Load<TextAsset>("master-data").bytes);
    }
}