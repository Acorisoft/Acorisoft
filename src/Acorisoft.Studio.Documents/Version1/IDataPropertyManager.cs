using LiteDB;

namespace Acorisoft.Studio
{
    public interface IDataPropertyManager : IMetadataManager
    {
        IDataProperty SetProperty(IDataProperty property);
        TProperty GetProperty<TProperty>()  where TProperty : IDataProperty;
    }
}