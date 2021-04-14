using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="IDataSetManager{TDataSet, TProperty}"/>
    /// </summary>
    /// <typeparam name="TDataSet"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public interface IDataSetManager<TDataSet, TProperty> : IDataSetManager<TDataSet>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataProperty, IDataProperty
    {
        /// <summary>
        /// 更新当前活跃的创作集属性。
        /// </summary>
        /// <param name="property">指定要更新的创作集属性，要求不能为空。</param>
        void Update(TProperty property);

        /// <summary>
        /// 切换到指定的创作集上下文。
        /// </summary>
        /// <param name="context">指定要切换的上下文，要求不能为空。</param>
        void Switch(IActivatingContext<TDataSet, TProperty> context);

        /// <summary>
        /// 使用指定的加载上下文加载一个新的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        /// <param name="context">指定的加载上下文，要求不能为空。</param>
        void Load(ILoadContext context);

        /// <summary>
        /// 使用指定的保存上下文创建一个新的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        /// <param name="context">指定的保存上下文，要求不能为空。</param>
        void Generate(ISaveContext<TProperty> context);

        /// <summary>
        /// 关闭当前正在打开的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        void Close();

    }
}
