
namespace uFluent.Extensions.Tags
{
    public static class TagsExtensions
    {
        public static DataType SetTagGroup(this DataType dataType, string tagGroup)
        {
            dataType.AddPreValue(tagGroup, 0, "group").Save();

            return dataType;
        }
    }
}
