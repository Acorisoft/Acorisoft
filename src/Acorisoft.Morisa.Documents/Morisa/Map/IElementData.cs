namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="IElementData"/>
    /// </summary>
    public interface IElementData : IUnitMapData
    {

        /// <summary>
        /// 获取当前 <see cref="ElementData"/> 的绘制大小。
        /// </summary>
        public int Size { get; }
    }
}