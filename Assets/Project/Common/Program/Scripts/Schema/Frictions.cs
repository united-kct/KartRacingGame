using MasterMemory;
using MessagePack;

namespace Common.Schema
{
    [MemoryTable("frictions"), MessagePackObject(true)]
    public sealed class Frictions
    {
        public Frictions(string id, string tagName, float frictionalAcceleration)
        {
            Id = id;
            TagName = tagName;
            FrictionalAcceleration = frictionalAcceleration;
        }

        [PrimaryKey] public string Id { get; }
        [SecondaryKey(0)] public string TagName { get; }
        public float FrictionalAcceleration { get; }
    }
}