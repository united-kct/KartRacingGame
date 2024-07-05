#nullable enable

using UnityEditor;

namespace Common.MasterData
{
    public static class MenuItems
    {
        // dotnetにMasterMemory.GeneratorとMessagePack.Generatorをインストールして、
        // input と outputのパスが正しいか確認する必要がある
        // 実行後、画面遷移を挟まないと反映されないので注意
        [MenuItem("Tools/MasterData/GenerateCode")]
        private static void GenerateCode()
        {
            CodeGenerator.ExecuteMasterMemoryCodeGenerator();
            CodeGenerator.ExecuteMessagePackCodeGenerator();
        }

        [MenuItem("Tools/MasterData/BuildMasterData")]
        private static void BuildMasterData()
        {
            MasterDataBuilder.Build();
        }
    }
}