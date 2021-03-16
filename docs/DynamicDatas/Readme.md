# DynamicData的使用经验

## SourceList

介绍我在使用SourceList过程中的经验。

### Transform方法

在`SourceList<T>`  类型中，我们如果想要像普通的LINQ一样让一个集合类型变成另一个集合类型，我们使用Select并不能获得我们想要的效果。这时候我们应该使用`Transform` 方法。

``` C#
public class StringAdapter{
    public string Text { get; set;}
}

var editableCollection = new SourceList<string>();
editableCollection.Connect()
    			  .Transform(x => new StringAdapter{Text = x })
    			  .Bind(out bindableCollection);
//
// 在未调用Transform方法之前，Bind方法最终会返回的是一个ReadOnlyObservableCollection<string>集合
// 但是在调用Transform方法之后，Bind方法最终会返回的是一个ReadOnlyObservableCollection<StringAdapter>集合
```

### Page

在`SourceList<T>`  类型中，我们可以很方便的使用`Page`方法来实现分页效果。分页的实现需要依赖以下几个类型：

* `ISubject<IPageRequest>`
* `IPageRequest`

分页的基本模型是:

* PageSize 每一页的内容大小
* Page 当前的页码（必须从1开始）

修改PageSize或者Page时将消息推送给`ISubject<IPageRequest>`即可完成自动分页。

> 调用的时候只能使用OnNext，不能使用OnCompleted

### Sort

目前还没研究出来

### Group

目前还没研究出来

### Filter

过滤目前使用的是Func<T,bool>委托，可以通过自定义过滤器来实现。但是目前并不能实现动态过滤的效果。