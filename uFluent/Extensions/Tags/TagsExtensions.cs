using uFluent.Extensions.Tags.Enums;

namespace uFluent.Extensions.Tags
{
    public static class TagsExtensions
    {
        public static IDataType SetTagDataTypePreValues(this IDataType dataType, string tagGroup, StorageType storageType)
        {
            dataType.AddPreValue(tagGroup, 1, "group")
                .AddPreValue(storageType.ToString(), 1, "storageType")
                .Save();

            return dataType;
        }
    }
}
