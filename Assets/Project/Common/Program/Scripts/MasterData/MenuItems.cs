#nullable enable

using UnityEditor;

namespace Common.MasterData
{
    public static class MenuItems
    {
        // dotnet��MasterMemory.Generator��MessagePack.Generator���C���X�g�[�����āA
        // input �� output�̃p�X�����������m�F����K�v������
        // ���s��A��ʑJ�ڂ����܂Ȃ��Ɣ��f����Ȃ��̂Œ���
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