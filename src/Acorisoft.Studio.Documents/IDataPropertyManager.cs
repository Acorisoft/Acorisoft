using LiteDB;

namespace Acorisoft.Studio.Documents
{
    public interface IDataPropertyManager : IMetadataManager
    {
        IDataProperty SetProperty(IDataProperty property);
        TProperty GetProperty<TProperty>()  where TProperty : IDataProperty;
    }
}