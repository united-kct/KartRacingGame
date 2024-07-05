// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace Generated.MasterData.Formatters.Common.MasterData
{
    public sealed class FrictionFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Common.MasterData.Friction>
    {
        // Id
        private static global::System.ReadOnlySpan<byte> GetSpan_Id() => new byte[1 + 2] { 162, 73, 100 };
        // TagName
        private static global::System.ReadOnlySpan<byte> GetSpan_TagName() => new byte[1 + 7] { 167, 84, 97, 103, 78, 97, 109, 101 };
        // FrictionalAcceleration
        private static global::System.ReadOnlySpan<byte> GetSpan_FrictionalAcceleration() => new byte[1 + 22] { 182, 70, 114, 105, 99, 116, 105, 111, 110, 97, 108, 65, 99, 99, 101, 108, 101, 114, 97, 116, 105, 111, 110 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::Common.MasterData.Friction value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            var formatterResolver = options.Resolver;
            writer.WriteMapHeader(3);
            writer.WriteRaw(GetSpan_Id());
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<string>(formatterResolver).Serialize(ref writer, value.Id, options);
            writer.WriteRaw(GetSpan_TagName());
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<string>(formatterResolver).Serialize(ref writer, value.TagName, options);
            writer.WriteRaw(GetSpan_FrictionalAcceleration());
            writer.Write(value.FrictionalAcceleration);
        }

        public global::Common.MasterData.Friction Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            var formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __Id__ = default(string);
            var __TagName__ = default(string);
            var __FrictionalAcceleration__ = default(float);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 2:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 25673UL) { goto FAIL; }

                        __Id__ = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<string>(formatterResolver).Deserialize(ref reader, options);
                        continue;
                    case 7:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 28549237342429524UL) { goto FAIL; }

                        __TagName__ = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<string>(formatterResolver).Deserialize(ref reader, options);
                        continue;
                    case 22:
                        if (!global::System.MemoryExtensions.SequenceEqual(stringKey, GetSpan_FrictionalAcceleration().Slice(1))) { goto FAIL; }

                        __FrictionalAcceleration__ = reader.ReadSingle();
                        continue;

                }
            }

            var ____result = new global::Common.MasterData.Friction(__Id__, __TagName__, __FrictionalAcceleration__);
            reader.Depth--;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name