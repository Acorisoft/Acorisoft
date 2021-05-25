using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public interface IDataPropertyManager<out TProperty> : IMetadataManager where TProperty : IDataProperty
    {
        IDataProperty SetProperty(IDataProperty property);
        TProperty GetProperty();
    }
}