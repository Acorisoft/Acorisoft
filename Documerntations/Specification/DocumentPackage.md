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
    - DPK_Preference
    - DPK_Profile
- DPK_FileIndex
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
    Guid Id { get; set }

    bool IsDeleted { get; set; }
    
    bool IsInLocalDisk { get; set; }
    
    string Path { get; set;}

    [BsonRef("DPK_FileIndexTag")]
    ICollection<string> Tags { get; set; }
}

```

## 文档索引

## 文档

## 元数据

### 文档集属性元数据

#### 元数据位置

文档集属性元数据位于`\DPK_Metadata\DPK_Property`

#### 元数据模型

``` C#
public interface IDocumentPackageProperty
{
    string Authors { get; set; }
    string Summary { get; set; }    
    string Topic { get; set; }
}

```