using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generated.MasterData.Resolvers;
using Generated.MasterData;
using MessagePack.Resolvers;
using MessagePack;

namespace Common.MasterData
{
    public static class MasterDataUtils
    {
        public static void InitializeMasterMemory()
        {
            IFormatterResolver messagePackResolvers = CompositeResolver.Create(
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );
            MessagePackSerializerOptions options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
            MessagePackSerializer.DefaultOptions = options;
        }
    }
}
