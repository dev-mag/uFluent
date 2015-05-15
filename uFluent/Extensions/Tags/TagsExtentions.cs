using uFluent.Extensions.Tags.Enums;

namespace uFluent.Extensions.Tags
{
    public static class TagsExtentions
    {
        public static DataType SetTagDataTypePreValues(this DataType dataType, string tagGroup, StorageType storageType)
        {
            dataType.AddPreValue(tagGroup, 1, "group")
                .AddPreValue(storageType.ToString(), 1, "storageType")
                .Save();

            return dataType;
        }
    }
}
