using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public interface IDataPropertyManager<TProperty> : IMetadataManager where TProperty : IDataProperty
    {
        TProperty SetProperty(TProperty property);
        TProperty GetProperty();
    }
}