using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    /// <summary>
    /// <see cref="TerrainLayer"/> 表示一个地形图层。
    /// </summary>
    public class TerrainLayer : MapLayer, ITerrainLayer
    {
        private ITerrainData[] _Datas;
        private int _Width;
        private int _Height;

        public TerrainLayer()
        {
        }

        internal override void OnAttachMapDocument(MapDocument document)
        {
            //
            // 初始化当前地图文档。
            _Datas = new ITerrainData[_Width * _Height];
        }

        public void SetData(ITerrainData data)
        {
            if(data != null)
            {
                var x = data.UnitX;
                var y = data.UnitY;

                if(x < 0 || x > _Width)
                {
                    throw new InvalidOperationException(nameof(data));
                }

                if (y < 0 || y > _Height)
                {
                    throw new InvalidOperationException(nameof(data));
                }

                _Datas[x + y * _Width] = data;
            }
        }

        public ITerrainData GetData(int x ,int y)
        {
            if (x < 0 || x > _Width)
            {
                throw new InvalidOperationException(nameof(x));
            }

            if (y < 0 || y > _Height)
            {
                throw new InvalidOperationException(nameof(y));
            }

            return _Datas[x + y * _Width];
        }

        public ITerrainData GetData(double x, double y)
        {
            if (x < 0 || x > _Width)
            {
                throw new InvalidOperationException(nameof(x));
            }

            if (y < 0 || y > _Height)
            {
                throw new InvalidOperationException(nameof(y));
            }

            return GetData((int)(x / 40),(int)(y / 40));
        }

        public override bool HitTest(int x, int y)
        {
            if (x > -1 && x < _Width && y > -1 && y < _Height)
            {
                return _Datas[y * _Width + x] != null;
            }

            return false;
        }

        public override bool HitTest(double x, double y)
        {
            return HitTest((int)(x / 40d), (int)(y / 40));
        }

        public IEnumerator<ITerrainData> GetEnumerator()
        {
            for(int y =0;y < _Height; y++)
            {
                for (int x = 0; x < _Width; x++)
                {
                    var data = _Datas[x + y * _Width];
                    if (data != null)
                    {
                        yield return data;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int y = 0; y < _Height; y++)
            {
                for (int x = 0; x < _Width; x++)
                {
                    var data = _Datas[x + y * _Width];
                    if (data != null)
                    {
                        yield return data;
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前 <see cref="TerrainLayer"/> 的绘制横坐标。
        /// </summary>
        public int Width
        {
            get => _Width;
            set => _Width = value;
        }

        /// <summary>
        /// 获取当前 <see cref="TerrainLayer"/> 的绘制纵坐标。
        /// </summary>
        public int Height
        {
            get => _Height;
            set => _Height = value;
        }

        public override sealed string ToString()
        {
            return $"w:{_Width},h:{_Height}";
        }
    }
}
