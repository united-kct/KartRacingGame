#nullable enable

using System;
using System.Collections.Generic;
using System.IO;

namespace Common.MasterData
{
    public class TinyCSVReader : IDisposable
    {
        // \tはタブのこと
        private static readonly char[] Trim = { ' ', '\t' };

        private readonly StreamReader _reader;

        public IReadOnlyList<string> Header { get; }

        // 初期化時に最初の行をヘッダとして読み取っている
        public TinyCSVReader(StreamReader reader)
        {
            _reader = reader;
            {
                // csvの最初の行を取得
                string line = reader.ReadLine() ?? throw new InvalidOperationException("ヘッダーが空です");

                // 最初の行をカンマ(",")で区切って配列 header に格納する
                int index = 0;
                List<string> header = new();
                while (index < line.Length)
                {
                    string s = GetValue(line, ref index);
                    if (s.Length == 0)
                    {
                        break;
                    }
                    header.Add(s);
                }

                Header = header;
            }
        }

        // 一つの行をカンマ区切りの値でTrimに指定された文字を削除して返す
        private string GetValue(string line, ref int i)
        {
            char[] temp = new char[line.Length - i];
            int j = 0;
            for (; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    i++;
                    break;
                }
                temp[j++] = line[i];
            }
            return new string(temp, 0, j).Trim(Trim);
        }

        // stream を閉じてリソースを解放
        public void Dispose()
        {
            _reader.Dispose();
        }

        // 次の行を取得するために使う
        public string[]? ReadValues()
        {
            // 次の行の値を取得
            string line = _reader.ReadLine();
            if (line == null || string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            // カンマ区切りで values に格納
            string[] values = new string[Header.Count];
            // 何文字目まで読んだかを保持
            int lineIndex = 0;
            for (int i = 0; i < values.Length; i++)
            {
                string s = GetValue(line, ref lineIndex);
                values[i++] = s;
            }

            return values;
        }

        // 次の行をヘッダ付きで取得するために使う
        public Dictionary<string, string>? ReadValuesWithHeader()
        {
            string[]? values = ReadValues();
            if (values == null)
            {
                return null;
            }

            Dictionary<string, string> dict = new();
            for (int i = 0; i < values.Length; i++)
            {
                dict.Add(Header[i], values[i]);
            }

            return dict;
        }
    }
}