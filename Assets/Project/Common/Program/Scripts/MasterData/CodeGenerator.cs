#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Common.MasterData
{
    public static class CodeGenerator
    {
        private static readonly string BasePath = $"{Application.dataPath}/Project/Common/Program/Scripts";
        private static readonly string InputDirectory = $"{BasePath}/MasterData/Schema";
        private static readonly string OutputDirectory = $"{BasePath}/Generated/MasterData";
        private const string Namespace = "Generated.MasterData";

        public static void ExecuteMasterMemoryCodeGenerator()
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

            // true で p.Exitedを使う
            p.EnableRaisingEvents = true;
            // プロセスが終了したとき
            p.Exited += (object sender, System.EventArgs e) =>
            {
                UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
                UnityEngine.Debug.Log($"{nameof(ExecuteMasterMemoryCodeGenerator)} : end");
                // リソースを解放
                p.Dispose();
                p = null;
            };
        }

        public static void ExecuteMessagePackCodeGenerator()
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

            // true で p.Exitedを使う
            p.EnableRaisingEvents = true;
            // プロセスが終了したとき
            p.Exited += (object sender, System.EventArgs e) =>
            {
                UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
                UnityEngine.Debug.Log($"{nameof(ExecuteMessagePackCodeGenerator)} : end");
                // リソースを解放
                p.Dispose();
                p = null;
            };
        }
    }
}