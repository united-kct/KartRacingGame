using Generated.MasterData;
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
            builder.Append(new Friction[]
            {
                new(id: "1", tagName: "GroundRoad", frictionalAcceleration: 2.78f),
                new(id: "2", tagName: "GroundOffroad", frictionalAcceleration: 4.17f)
            });

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