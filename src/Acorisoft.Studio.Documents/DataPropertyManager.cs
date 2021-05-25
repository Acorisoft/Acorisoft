namespace Acorisoft.Studio.Documents
{
    public abstract class DataPropertyManager<TProperty> : MetadataManager, IDataPropertyManager<TProperty> where TProperty : IDataProperty
    {
        /// <summary>
        /// 设置属性。完成基础的序列化操作。
        /// </summary>
        /// <param name="property">需要序列化的属性。</param>
        public virtual TProperty SetProperty(TProperty property)
        {
            //
            // 使得 TProperty 能够
            return SetObject<TProperty>(property);
        }

        public TProperty GetProperty()
        {
            return GetObject<TProperty>();
        }
    }
}