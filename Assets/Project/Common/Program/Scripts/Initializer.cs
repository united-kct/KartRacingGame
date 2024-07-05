using Generated.MasterData;
using Generated.MasterData.Resolvers;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

namespace Common
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SetupMessagePackResolver()
        {
            StaticCompositeResolver.Instance.Register(new[]{
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            });
            MessagePackSerializerOptions options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            MessagePackSerializer.DefaultOptions = options;
        }
    }
}