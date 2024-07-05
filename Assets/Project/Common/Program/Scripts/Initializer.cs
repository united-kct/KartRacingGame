using Common.MasterData;
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