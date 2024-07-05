using MasterMemory;
using MasterMemory.Validation;
using MessagePack;

namespace Common.MasterData
{
    [MemoryTable("friction"), MessagePackObject(true)]
    public sealed class Friction : IValidatable<Friction>
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

        void IValidatable<Friction>.Validate(IValidator<Friction> validator)
        {
            if (TagName == "")
            {
                validator.Fail("TagNameは入力必須です");
            }

            if (validator.CallOnce())
            {
                ValidatableSet<Friction> frictions = validator.GetTableSet();
                frictions.Unique(friction => friction.TagName);
            }
        }
    }
}