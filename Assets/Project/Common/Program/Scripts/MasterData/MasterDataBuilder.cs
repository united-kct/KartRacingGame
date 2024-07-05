using Generated.MasterData;
using MasterMemory;
using MasterMemory.Meta;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Common.MasterData
{
    public static class MasterDataBuilder
    {
        public static void Build()
        {
            MasterDataUtils.InitializeMasterMemory();

            DatabaseBuilder builder = new();
            MetaDatabase metaDataBase = MemoryDatabase.GetMetaDatabase();
            MetaTable table = metaDataBase.GetTableInfo("friction");
            string csvPath = $"{Application.dataPath}/Project/Common/Program/MasterData/friction.csv";
            builder.AppendDynamic(table.DataType, CsvSerializer.Deserialize(csvPath, table));

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