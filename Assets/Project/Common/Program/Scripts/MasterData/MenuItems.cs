#nullable enable

using System.Collections;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Common.MasterData
{
    public static class MenuItems
    {
        // dotnetにMasterMemory.GeneratorとMessagePack.Generatorをインストールして、
        // inputとoutputのパスが正しいか確認する必要がある
        [MenuItem("Tools/MasterData/GenerateCode")]
        private static void GenerateCode()
        {
            CodeGenerator.ExecuteMasterMemoryCodeGenerator();
            CodeGenerator.ExecuteMessagePackCodeGenerator();
        }
    }
}