# 文档集

## 文档集后缀名

文档集的后缀名由下列部分组成：
前缀 `Mori-`和后缀 `DPK`以及版本号`v1`组成`Mori-DPKv1`

## 文档集的实际格式

实际上文档集由 `LiteDB` 提供存储功能。

## 文档集的抽象

``` C#

public interface DocumentPackage
{
    IDocumentPackageProfileProvider Profile { get; }
    IDocumentPackageMetadataManager Metadata { get; }
    IDocumentPackageFileIndexManager FileIndex { get; }
    IDocumentPackageDocumentIndexManager DocumentIndex { get; }
    IDocumentPackageDocumentManager Document { get; }
}

```

## 文档集的集合分类

``` YAML
collection :
- DPK_Metadata
    - DPK_Setting
    - DPK_Profile
- DPK_FileIndex
- DPK_FileIndexTag
- DPK_DocumentIndex
- DPK_Document
```

## 文件索引

### 文件索引与集合

文件索引功能由`ILiteCollection<FileIndex>`集合提供，使用的是集合名为`DPK_FileIndex` 的集合进行存储。

### 文件索引摘要

使用文件索引功能主要是因为我们想实现动态的索引文件内容，`LiteDB` 虽然为我们提供了实用的IFileStorage接口，但是她所提供的功能并不满足我们实时最终文件状态的需要，特别是枚举某一个类型的文件时，它的功能就非常的局限。改造文件存储是必然所趋。

### 文件索引模型

``` C#

public interface IFileIndex
{
    [BsonId]
    Guid Id { get; set; }

    bool IsDeleted { get; set; }
    
    bool IsInLocalDisk { get; set; }
    
    string Path { get; set;}

    [BsonRef("DPK_FileIndexTag")]
    ICollection<string> Tags { get; set; }
}

```

## 文档索引


### 文档索引与集合

文件索引功能由`ILiteCollection<DocumentIndex>`集合提供，使用的是集合名为`DPK_DocumentIndex` 的集合进行存储。

### 文档索引摘要

使用文档索引的理由是，文档作为一个需要加载到内存解析的大对象，我们在做文档列表读取的时候并不建议直接读取到内存中，因为这样会导致两个后果：
1. 反序列化时原始的文档也需要占用内存空间， 并且文档大小普遍偏大，一般都`>100KB`，甚至普遍大于`500KB`，这样子在文档列表的应用场景下会导致大量的不必要内存占用。
2. 反序列化是原始的文档读取与序列化都需要一定的时间开销，尽量减少时间开销

### 文档索引模型

``` C#

public interface IDocumentIndex
{
    [BsonId]
    Guid Id { get; set }
    string Name { get; set; }
    string Summary { get; set; }
    string Topic { get; set; }
    IFile Thumbnail { get; set; }
    IFile Cover { get; set; }
    ICollection<string> Tags { get; set; }
}

```


## 组合文档

组合文档表示的是一种由部件组合而成的文档。

## 文档模型

``` C#
public interface ICompositionDocument
{
    ICompositionDocumentPart Documents { get; }
    ICompositionMetadataPart Metadatas { get; }
}

```


## 元数据

元数据为全局唯一的数据，用于存储文档集特殊的数据。

### 文档集属性元数据

文档集属性元数据用于保存当前文档集的一些基础信息，比如：作者信息、所有者信息等等

#### 元数据位置

文档集属性元数据位于`\DPK_Metadata\DPK_Property`

#### 元数据模型

``` C#
public interface IDocumentPackageProperty
{
    string Authors { get; set; }
    string Summary { get; set; }    
    string Topic { get; set; }
    IFile Logo { get; set; }
}

```

#### 元数据属性

* `Owners` 表示该文档的所有者，数据参考如下:`Acoris;lindexi`
* `Authors` 表示该文档集的作者，数据参考如下:`Acoris;lindexi`
* `Summary` 表示该文档集的摘要。
* `Topic` 表示该文档集的主题。
* `Cover` 表示该文档集的封面。

### 文档集配置文件元数据

文档集配置文件元数据用于保存用户的配置信息与偏好信息。

#### 元数据位置

文档集属性元数据位于`\DPK_Metadata\DPK_Profile`

#### 元数据模型

``` C#
public interface IDocumentPackageProfile
{    
    bool IsFirstTime { get; set; }
}

```

* `IsFirstTime` 用于存储当前文档集是否为第一次创建，该值为true的时候需要执行第一次初始化行为。