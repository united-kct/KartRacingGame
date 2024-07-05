using Generated.MasterData;
using Generated.MasterData.Resolvers;
using MessagePack;
using MessagePack.Resolvers;

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