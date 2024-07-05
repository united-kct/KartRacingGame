using MasterMemory;
using MessagePack;

namespace Common.MasterData
{
    [MemoryTable("friction"), MessagePackObject(true)]
    public sealed class Friction
    {
        [PrimaryKey] public string Id { get; private set; }
        [SecondaryKey(0)] public string TagName { get; private set; }
        public float FrictionalAcceleration { get; private set; }

        public Friction(string id, string tagName, float frictionalAcceleration)
        {
            Id = id;
            TagName = tagName;
            FrictionalAcceleration = frictionalAcceleration;
        }
    }
}