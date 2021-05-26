namespace Acorisoft.Studio.Documents
{
    public abstract class DataPropertyManager<TProperty> : MetadataManager, IDataPropertyManager where TProperty : IDataProperty
    {
        /// <summary>
        /// 设置属性。完成基础的序列化操作。
        /// </summary>
        /// <param name="property">需要序列化的属性。</param>
        public IDataProperty SetProperty(IDataProperty property)
        {
            //
            // 使得 TProperty 能够
            return SetObject<IDataProperty>(property);
        }

        public TProperty GetProperty<TProperty>() where TProperty : IDataProperty
        {
            return GetObject<TProperty>();
        }
    }
}