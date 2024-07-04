using Common.MasterData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SetupMessagePackResolver()
        {
            MasterDataUtils.InitializeMasterMemory();
        }
    }
}