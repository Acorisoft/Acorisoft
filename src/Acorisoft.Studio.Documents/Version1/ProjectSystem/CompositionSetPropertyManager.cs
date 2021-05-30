using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Acorisoft.Studio.ProjectSystem
{
    public class CompositionSetPropertyManager : DataPropertyManager, ICompositionSetPropertyManager
    {
        private readonly ICompositionSetFileManager _fileManager;
        private protected readonly Subject<ICompositionSetProperty> CurrentCompositionProperty;
        public CompositionSetPropertyManager(ICompositionSetFileManager fileManager)
        {
            _fileManager = fileManager;
            CurrentCompositionProperty = new Subject<ICompositionSetProperty>();
        }

        public async Task<ICompositionSetProperty> SetProperty(ICompositionSetProperty property)
        {
            //
            // TODO: File Operation
            if (property.Cover is not null)
            {
            }
            
            base.SetProperty(property);
            
            CurrentCompositionProperty.OnNext(property);
            
            return property;
        }

        public ICompositionSetProperty GetProperty() 
        {
            var property = base.GetProperty<CompositionSetProperty>();
            CurrentCompositionProperty.OnNext(property);
            return property;
        }


        /// <summary>
        /// 获取或设置当前 <see cref="ICompositionSet"/> 的属性。
        /// </summary>
        public IObservable<ICompositionSetProperty> Property => CurrentCompositionProperty;
    }
}