using Generated.MasterData;
using Generated.MasterData.Resolvers;
using MasterMemory;
using MasterMemory.Meta;
using MessagePack;
using MessagePack.Resolvers;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Common.MasterData
{
    public static class MasterDataBuilder
    {
        public static void Build()
        {
            IFormatterResolver messagePackResolvers = CompositeResolver.Create(
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );
            MessagePackSerializerOptions options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
            MessagePackSerializer.DefaultOptions = options;

            DatabaseBuilder builder = new();
            MetaDatabase metaDataBase = MemoryDatabase.GetMetaDatabase();
            MetaTable table = metaDataBase.GetTableInfo("friction");
            string csvPath = "Assets/Project/Common/Program/MasterData/friction.csv";
            List<object> data = CsvSerializer.Deserialize(csvPath, table);
            builder.AppendDynamic(table.DataType, data);

            byte[] binary = builder.Build();

            string path = $"{Application.dataPath}/Resources/master-data.bytes";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(path, binary);
            Debug.Log($"Write byte[] to: {path}");
            AssetDatabase.Refresh();
        }
    }
}