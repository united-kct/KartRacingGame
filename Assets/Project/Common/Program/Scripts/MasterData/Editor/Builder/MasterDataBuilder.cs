using Generated.MasterData;
using MasterMemory;
using MasterMemory.Meta;
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
            MasterDataUtils.InitializeMasterMemory();

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