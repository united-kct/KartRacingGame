using MasterMemory;
using MessagePack;

namespace Common.Schema
{
    [MemoryTable("friction"), MessagePackObject(true)]
    public sealed class Friction
    {
        [PrimaryKey] public string Id { get; }
        [SecondaryKey(0)] public string TagName { get; }
        public float FrictionalAcceleration { get; }

        public Friction(string id, string tagName, float frictionalAcceleration)
        {
            Id = id;
            TagName = tagName;
            FrictionalAcceleration = frictionalAcceleration;
        }
    }
}