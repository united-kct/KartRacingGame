#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Common.MasterMemory
{
    public static class MenuItems
    {
        private static readonly string BasePath = $"{Application.dataPath}/Project/Common/Program/Scripts";
        private static readonly string InputDirectory = $"{BasePath}/Schema";
        private static readonly string OutputDirectory = $"{BasePath}/Generated/MasterData";
        private const string Namespace = "MasterData";

        // dotnet��MasterMemory.Generator��MessagePack.Generator���C���X�g�[�����āA
        // input��output�̃p�X�����������m�F����K�v������
        [MenuItem("Tools/MasterData/GenerateCode")]
        private static void GenerateCode()
        {
            ExecuteMasterMemoryCodeGenerator();
            ExecuteMessagePackCodeGenerator();
        }

        private static void ExecuteMasterMemoryCodeGenerator()
        {
            UnityEngine.Debug.Log($"{nameof(ExecuteMasterMemoryCodeGenerator)} : start");

            ProcessStartInfo psi = new()
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                FileName = "dotnet-mmgen",
                Arguments = $@"-i ""{InputDirectory}"" -o ""{OutputDirectory}"" -c -n ""{Namespace}""",
            };

            //UnityEngine.Debug.Log($@"-i ""{InputPath}"" -o ""{OutputPath}"" -c -n ""{Namespace}""");

            Process? p = Process.Start(psi);

            // true �� p.Exited���g��
            p.EnableRaisingEvents = true;
            // �v���Z�X���I�������Ƃ�
            p.Exited += (object sender, System.EventArgs e) =>
            {
                UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
                UnityEngine.Debug.Log($"{nameof(ExecuteMasterMemoryCodeGenerator)} : end");
                // ���\�[�X�����
                p.Dispose();
                p = null;
            };
        }

        private static void ExecuteMessagePackCodeGenerator()
        {
            UnityEngine.Debug.Log($"{nameof(ExecuteMessagePackCodeGenerator)} : start");

            ProcessStartInfo psi = new()
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                FileName = "mpc",
                Arguments = $@"-i ""{InputDirectory}"" -o ""{OutputDirectory}"" -n ""{Namespace}""",
            };

            //UnityEngine.Debug.Log($@"-i ""{InputPath}"" -o ""{OutputPath}"" -n ""{Namespace}""");

            Process? p = Process.Start(psi);

            // true �� p.Exited���g��
            p.EnableRaisingEvents = true;
            // �v���Z�X���I�������Ƃ�
            p.Exited += (object sender, System.EventArgs e) =>
            {
                UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
                UnityEngine.Debug.Log($"{nameof(ExecuteMessagePackCodeGenerator)} : end");
                // ���\�[�X�����
                p.Dispose();
                p = null;
            };
        }
    }
}