// <auto-generated />
#pragma warning disable CS0105
using Common.MasterData;
using MasterMemory.Validation;
using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;
using Generated.MasterData.Tables;

namespace Generated.MasterData
{
   public sealed class DatabaseBuilder : DatabaseBuilderBase
   {
        public DatabaseBuilder() : this(null) { }
        public DatabaseBuilder(MessagePack.IFormatterResolver resolver) : base(resolver) { }

        public DatabaseBuilder Append(System.Collections.Generic.IEnumerable<Friction> dataSource)
        {
            AppendCore(dataSource, x => x.Id, System.StringComparer.Ordinal);
            return this;
        }

    }
}