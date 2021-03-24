namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="IMapData"/>
    /// </summary>
    public interface IMapData
    {
        /// <summary>
        /// 获取当前 <see cref="MapData"/> 的绘制画刷。
        /// </summary>
        Brush Brush { get; }

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