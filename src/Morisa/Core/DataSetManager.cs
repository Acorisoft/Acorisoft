using Acorisoft.Morisa.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="DataSetManager{TDataSet, TProperty}"/>
    /// </summary>
    /// <typeparam name="TDataSet"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public abstract class DataSetManager<TDataSet, TProperty> : DataSetManager<TDataSet>, IDataSetManager<TDataSet, TProperty>
        where TDataSet : DataSet<TProperty>, IDataSet<TProperty>
        where TProperty : DataProperty, IDataProperty
    {

        /// <summary>
        /// 在派生类中完成后续的数据集加载操作。
        /// </summary>
        /// <param name="context">指定的要加载的上下文。该参数不能为空，并且保证不为空。</param>
        protected abstract void ProcessLoading(ILoadContext context);
        

        /// <summary>
        /// 更新当前活跃的创作集属性。
        /// </summary>
        /// <param name="property">指定要更新的创作集属性，要求不能为空。</param>
        public void Update(TProperty property)
        {

        }

        /// <summary>
        /// 切换到指定的创作集上下文。
        /// </summary>
        /// <param name="context">指定要切换的上下文，要求不能为空。</param>
        public void Switch(IActivatingContext<TDataSet, TProperty> context)
        {

        }

        /// <summary>
        /// 使用指定的加载上下文加载一个新的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        /// <param name="context">指定的加载上下文，要求不能为空。</param>
        public void Load(ILoadContext context)
        {
            if(context == null)
            {
                throw new InvalidOperationException(SR.DataSetManager_Load_Context_Null);
            }

            if (string.IsNullOrEmpty(context.FileName))
            {
                throw new InvalidOperationException(SR.DataSetManager_Load_FileName_Null);
            }

            if (!File.Exists(context.FileName))
            {
                throw new InvalidOperationException(SR.DataSetManager_Load_File_NotExists);
            }

            //
            // 在派生类中完成后续的数据集加载操作。
            ProcessLoading(context);
        }

        /// <summary>
        /// 使用指定的保存上下文创建一个新的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        /// <param name="context">指定的保存上下文，要求不能为空。</param>
        public void Generate(ISaveContext<TProperty> context)
        {

        }

        /// <summary>
        /// 关闭当前正在打开的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        public void Close()
        {

        }

        ///// <summary>
        ///// 获取当前正在活动的创作集 <see cref="ICompositionSetContext"/> 实例。
        ///// </summary>
        //public IActivatingContext<TDataSet, TProperty> Activating { get; }

        ///// <summary>
        ///// 获取当前所有已经打开的创作集 <see cref="ICompositionSetContext"/> 实例。
        ///// </summary>
        //public abstract ReadOnlyObservableCollection<IActivatingContext<TDataSet, TProperty>> Opening { get; }
    }
}
