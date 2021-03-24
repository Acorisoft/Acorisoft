/** 
// 总结上一个版本的设计缺陷，主要是在于以下几点:
//
// 1)
// ViewModel - Service 的设计没有区分度
//
// 就拿EmotionViewModel里面的设计来说吧。

    public partial class EmotionViewModel : ViewModelBase
    {
        private readonly IEmotionMechanism _Mechanism;
        private readonly ICommand _AddOperator;
        private readonly ICommand _ClearOperator;
        private readonly ICommand _RemoveOperator;

        public EmotionViewModel()
        {
            _Mechanism = Locator.Current.GetService<IEmotionMechanism>();
            _AddOperator = ReactiveCommand.Create(async ()=>
            {
                var count = new Random().Next(0,12);
                var elements = new[]
                {
                    CompositionElementFactory.CreateMottoEmotion(),
                    CompositionElementFactory.CreateMottoEmotion("当爱情萌芽的时候，只需要悄悄地给点希望就可以了","新房昭之")
                };
                var element = elements[count % 2];
                element.Creation = DateTime.Now;
                _Mechanism.Add(element);
            });
            _RemoveOperator = ReactiveCommand.Create<IEmotionElement>(async x =>
            {
                if(x is IEmotionElement)
                {
                    _Mechanism.Remove(x);
                }
            });
            _ClearOperator = ReactiveCommand.Create(() =>
            {
                _Mechanism.Clear();
            });
        }

        public ICommand AddOperator => _AddOperator;
        public ICommand RemoveOperator => _RemoveOperator;
        public ICommand ClearOperator => _ClearOperator;
        public ReadOnlyObservableCollection<IEmotionElement> Collection => _Mechanism.Collection;
        public int Page
        {
            get => _Mechanism.Page;
            set => _Mechanism.Page = value;
        }
        public int PageSize
        {
            get => _Mechanism.PageSize;
            set => _Mechanism.PageSize = value;
        }
    
}

// 里面的逻辑大部分都是重复Mechanism中的设计，并且有大量的冗余设计，比如Page，PageSize，这些的设计过于冗余。
//
// 所以:
// 建议1:
//
// Mechanism 中不允许提供Page、PageSize的冗余设计
//
// 建议2:
// 
// Mechanism 将会改变自己的命名后缀，用来起到表示作用。目前在Manager,Service,Mechanism中选择(现在已经敲定了是Factory)。










//
// 命名规则
//
// 我们的App非常喜欢用XX集来命名一个概念，比如设定集，它表示的是一个世界观的所有设定与设定相关所需的资料的集合。它一般使用XXXSet来命名。
//
// 1) 设定集
//
// 设定集是我们用于指代一个世界观设定创作项目及创作这个世界观设定所需的所有资料的集合，就是设定集。我们使用CompositionSet来称呼设定集，因为Composition在英文中有
// 创作的意思，直译过来就是创作集合，非常符合我们对设定集的概念。
//
// Tags : CompositionSet
//
// 2) 数据集
// 
// 数据集是分布在设定集中的各个组件，每个数据集都维护了这个数据集自己的数据。
//
// 3) 创作管理器
//
// CompositionManager 用于管理一个设定集与各个数据集之间的关系。
//
// 4) 数据集工厂
//
// 我们还是采取数据集工厂的方式来对数据集进行编辑，也方便我们设计异步、跨线程操作。


//
// ViewModel - Factory - DataSet

**/