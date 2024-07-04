#nullable enable

using System.Collections;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Common.MasterData
{
    public static class MenuItems
    {
        // dotnet��MasterMemory.Generator��MessagePack.Generator���C���X�g�[�����āA
        // input��output�̃p�X�����������m�F����K�v������
        // ���s��A��ʑJ�ڂ����܂Ȃ��Ɣ��f����Ȃ��̂Œ���
        [MenuItem("Tools/MasterData/GenerateCode")]
        private static void GenerateCode()
        {
            CodeGenerator.ExecuteMasterMemoryCodeGenerator();
            CodeGenerator.ExecuteMessagePackCodeGenerator();
        }
    }
}