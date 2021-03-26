namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="IMapData"/>
    /// </summary>
    public interface IMapData
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 
        /// </summary>
        public int RefId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BrushMode Mode { get; set; }

        /// <summary>
        /// 获取当前 <see cref="MapData"/> 的绘制横坐标。
        /// </summary>
        double X { get; }

        /// <summary>
        /// 获取当前 <see cref="MapData"/> 的绘制纵坐标。
        /// </summary>
        double Y { get; }
    }
}