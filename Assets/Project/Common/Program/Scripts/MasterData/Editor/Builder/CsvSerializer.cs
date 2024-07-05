#nullable enable

using MasterMemory.Meta;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Common.MasterData
{
    public static class CsvSerializer
    {
        public static List<object> Deserialize(MetaTable table)
        {
            List<object> records = new();
            string path = $"Assets/Project/Common/Program/MasterData/Csv/{table.TableName}.csv";
            TextAsset? textAsset = AssetDatabase.LoadAssetAtPath<TextAsset?>(path);
            string csv = textAsset != null ? textAsset.text : throw new FileNotFoundException($"テーブル:{table.TableName}の csvファイルが見つかりません");

            using (MemoryStream stream = new(Encoding.UTF8.GetBytes(csv)))
            using (StreamReader reader = new(stream))
            using (TinyCSVReader csvReader = new(reader))
            {
                while (true)
                {
                    Dictionary<string, string>? columnNameToValueMap = csvReader.ReadValuesWithHeader();
                    if (columnNameToValueMap == null)
                    {
                        break;
                    }

                    // 各プロパティの名前に一致するカラムを探して値を設定する（一致するカラムがないものはスキップ
                    object record = FormatterServices.GetUninitializedObject(table.DataType);
                    foreach (MetaProperty property in table.Properties)
                    {
                        string columnName = property.Name;
                        if (!columnNameToValueMap.TryGetValue(columnName, out string rawValue))
                            continue;

                        object? value = ParseValue(property.PropertyInfo.PropertyType, rawValue);
                        if (property.PropertyInfo.SetMethod == null)
                        {
                            throw new Exception(
                                $"Target property does not exists set method. If you use {{get;}}, please change to {{ get; private set; }}, Type: {property.PropertyInfo.DeclaringType} Prop: {property.PropertyInfo.Name}"
                                );
                        }

                        property.PropertyInfo.SetValue(record, value);
                    }

                    records.Add(record);
                }
                csvReader.Dispose();
            }

            return records;
        }

        // 型を変換する関数
        private static object? ParseValue(Type type, string rawValue)
        {
            if (type == typeof(string)) return rawValue;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrWhiteSpace(rawValue)) return null;
                return ParseValue(type.GenericTypeArguments[0], rawValue);
            }

            if (type.IsEnum)
            {
                object value = Enum.Parse(type, rawValue);
                return value;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    // True/False or 0,1
                    if (int.TryParse(rawValue, out int intBool)) return Convert.ToBoolean(intBool);
                    return bool.Parse(rawValue);

                case TypeCode.Char:
                    return char.Parse(rawValue);

                case TypeCode.SByte:
                    return sbyte.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.Byte:
                    return byte.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.Int16:
                    return short.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.UInt16:
                    return ushort.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.Int32:
                    return int.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.UInt32:
                    return uint.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.Int64:
                    return long.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.UInt64:
                    return ulong.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.Single:
                    return float.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.Double:
                    return double.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.Decimal:
                    return decimal.Parse(rawValue, CultureInfo.InvariantCulture);

                case TypeCode.DateTime:
                    return DateTime.Parse(rawValue, CultureInfo.InvariantCulture);

                default:
                    if (type == typeof(DateTimeOffset))
                        return DateTimeOffset.Parse(rawValue, CultureInfo.InvariantCulture);
                    if (type == typeof(TimeSpan))
                        return TimeSpan.Parse(rawValue, CultureInfo.InvariantCulture);
                    if (type == typeof(Guid)) return Guid.Parse(rawValue);

                    // or other your custom parsing.
                    throw new NotSupportedException();
            }
        }
    }
}