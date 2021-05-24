using LiteDB;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSet : ICompositionSet, ICompositionSetDatabase
    {
        public CompositionSet(string path)
        {
            Path = path;
        }
        
        public void Dispose()
        {
            MainDatabase?.Dispose();
        }
        
        public string Name { get; set; }
        public string Path { get; }

        /// <summary>
        /// 获取当前项目的数据库地址。
        /// </summary>
        /// <remarks>
        /// 切换上下文时请保证该属性能够正确的释放。
        /// </remarks>
        public LiteDatabase MainDatabase { get; internal set; }
    }
}