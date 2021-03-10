using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft
{
    public interface ILiteDatabaseContext
    {
        public bool Result { get; }
        public ILiteDatabase Database { get; }
    }

    public interface IFieldContext<T> : ILiteDatabaseContext
    {
        ILiteDatabaseContext Context { get; }
    }

    public interface IFieldContext<TElement, TDBElement> : ILiteDatabaseContext
    {
        ILiteDatabaseContext Context { get; }
    }
    public interface ICollectionContext<TElement, TList> : ILiteDatabaseContext where TList : IEnumerable<TElement>
    {
        ILiteDatabaseContext Context { get; }
    }

    public static class LiteDBMixins
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        protected class LiteDatabaseContext : ILiteDatabaseContext
        {
            public LiteDatabaseContext(ILiteDatabase database , bool result)
            {
                Result = result;
                Database = database;

            }
            public bool Result { get; }
            public ILiteDatabase Database { get; }
        }

        protected class FieldDatabaseContext<TElement> : IFieldContext<TElement>
        {
            public FieldDatabaseContext(ILiteDatabaseContext context , Func<ILiteDatabaseContext , TElement> factory)
            {
                Result = context.Result;
                Database = context.Database;
                Context = context;
                Factory = factory;
            }

            public ILiteDatabaseContext Context { get; }
            public bool Result { get; }
            public ILiteDatabase Database { get; }
            public string FieldId { get; internal set; }
            public string CollectionName { get; internal set; }
            public Func<ILiteDatabaseContext , TElement> Factory { get; }
            public ILiteCollection<TElement> DBCollection { get; internal set; }
        }

        protected class FieldDatabaseContext<TElement, TDBElement> : IFieldContext<TElement , TDBElement>
        {
            public FieldDatabaseContext(ILiteDatabaseContext context , Func<ILiteDatabaseContext , TElement> factory)
            {
                Result = context.Result;
                Database = context.Database;
                Context = context;
                Factory = factory;
            }

            public ILiteDatabaseContext Context { get; }
            public bool Result { get; }
            public ILiteDatabase Database { get; }
            public string FieldId { get; internal set; }
            public string CollectionName { get; internal set; }
            public Func<ILiteDatabaseContext , TElement> Factory { get; }
            public ILiteCollection<TDBElement> DBCollection { get; internal set; }
        }

        protected class CollectionDatabaseContext<TElement, TList> : ICollectionContext<TElement , TList> where TList : IEnumerable<TElement>
        {
            public CollectionDatabaseContext(ILiteDatabaseContext context , Func<ILiteCollection<TElement> , TList> factory , Func<ILiteCollection<TElement> , TList> construct)
            {
                Result = context.Result;
                Database = context.Database;
                Context = context;
                Factory = factory;
                Construct = construct;
            }

            public ILiteDatabaseContext Context { get; }
            public bool Result { get; }
            public ILiteDatabase Database { get; }
            public string FieldId { get; internal set; }
            public string CollectionName { get; internal set; }
            public Func<ILiteCollection<TElement> , TList> Factory { get; internal set; }
            public Func<ILiteCollection<TElement> , TList> Construct { get; internal set; }
            public ILiteCollection<TElement> DBCollection { get; internal set; }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Exists
        //
        //-------------------------------------------------------------------------------------------------
        public static ILiteDatabaseContext Exists(this ILiteDatabase database , string collectionName)
        {
            if (database == null)
            {
                throw new InvalidOperationException();
            }

            return new LiteDatabaseContext(database , database.CollectionExists(collectionName));
        }

        public static ILiteDatabaseContext Exists(this ILiteDatabase database , Func<ILiteDatabase , bool> predicate)
        {
            if (database == null || predicate == null)
            {
                throw new InvalidOperationException();
            }

            return new LiteDatabaseContext(database , predicate(database));
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  ToFactory
        //
        //-------------------------------------------------------------------------------------------------
        public static IFieldContext<T> ToFactory<T>(this ILiteDatabaseContext context , Func<ILiteDatabaseContext , T> initDelegate)
        {
            if (context == null || initDelegate == null)
            {
                throw new InvalidOperationException();
            }
            return new FieldDatabaseContext<T>(context , initDelegate);
        }

        public static IFieldContext<T , TDBElement> ToFactory<T, TDBElement>(this ILiteDatabaseContext context , Func<ILiteDatabaseContext , T> initDelegate)
        {
            if (context == null || initDelegate == null)
            {
                throw new InvalidOperationException();
            }
            return new FieldDatabaseContext<T , TDBElement>(context , initDelegate);
        }

        public static ICollectionContext<TElement , TList> ToFactory<TElement, TList>(this ILiteDatabaseContext context , Func<ILiteCollection<TElement> , TList> initDelegate , Func<ILiteCollection<TElement> , TList> constructDelegate) where TList : IEnumerable<TElement>
        {
            if (context == null || initDelegate == null || constructDelegate == null)
            {
                throw new InvalidOperationException();
            }
            return new CollectionDatabaseContext<TElement , TList>(context , initDelegate , constructDelegate);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  ToField
        //
        //-------------------------------------------------------------------------------------------------
        public static IFieldContext<T> ToField<T>(this IFieldContext<T> context , ref T backEnd)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }

            if (context is FieldDatabaseContext<T> fdContext)
            {
                fdContext.DBCollection = context.Database.GetCollection<T>(fdContext.CollectionName);

                if (fdContext.Result)
                {
                    backEnd = fdContext.DBCollection.FindById(fdContext.FieldId);
                }
                else
                {
                    backEnd = fdContext.Factory(context);
                    fdContext.DBCollection.Upsert(backEnd);
                }
            }

            return context;
        }

        public static IFieldContext<T , TDBElement> ToField<T, TDBElement>(this IFieldContext<T , TDBElement> context , ref T backEnd) where TDBElement : BsonValue
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }

            if (context is FieldDatabaseContext<T , TDBElement> fdContext)
            {
                fdContext.DBCollection = context.Database.GetCollection<TDBElement>(fdContext.CollectionName);

                if (fdContext.Result)
                {
                    var document = fdContext.DBCollection.FindById(fdContext.FieldId);
                    backEnd = BsonMapper.Global.Deserialize<T>(document);
                }
                else
                {
                    backEnd = fdContext.Factory(context);
                    fdContext.DBCollection.Upsert(fdContext.FieldId , BsonMapper.Global.Serialize(backEnd) as TDBElement);
                }
            }

            return context;
        }

        public static ICollectionContext<TElement , TList> ToCollection<TElement, TList>(this ICollectionContext<TElement , TList> context , ref TList backEnd) where TList : IEnumerable<TElement>
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }

            if (context is CollectionDatabaseContext<TElement , TList> cdContext)
            {
                cdContext.DBCollection = context.Database.GetCollection<TElement>(cdContext.CollectionName);

                if (cdContext.Result)
                {
                    backEnd = cdContext.Factory(cdContext.DBCollection);
                }
                else
                {
                    backEnd = cdContext.Factory(cdContext.DBCollection);
                    cdContext.DBCollection.Upsert(backEnd);
                }
            }

            return context;
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  WithCollectionName
        //
        //-------------------------------------------------------------------------------------------------
        public static IFieldContext<T> WithCollectionName<T>(this IFieldContext<T> context , string collectionName)
        {
            if (context == null || string.IsNullOrEmpty(collectionName))
            {
                throw new InvalidOperationException();
            }

            if (context is FieldDatabaseContext<T> fdContext)
            {
                fdContext.CollectionName = collectionName;
            }

            return context;
        }

        public static IFieldContext<T , TDBElement> WithCollectionName<T, TDBElement>(this IFieldContext<T , TDBElement> context , string collectionName) where TDBElement : BsonValue
        {
            if (context == null || string.IsNullOrEmpty(collectionName))
            {
                throw new InvalidOperationException();
            }

            if (context is FieldDatabaseContext<T , TDBElement> fdContext)
            {
                fdContext.CollectionName = collectionName;
            }

            return context;
        }

        public static ICollectionContext<TElement , TList> WithCollectionName<TElement, TList>(this ICollectionContext<TElement , TList> context , string collectionName) where TList : IEnumerable<TElement>
        {
            if (context == null || string.IsNullOrEmpty(collectionName))
            {
                throw new InvalidOperationException();
            }


            if (context is CollectionDatabaseContext<TElement , TList> cdContext)
            {
                cdContext.CollectionName = collectionName;
            }

            return context;
        }


        public static IFieldContext<T> WithFieldName<T>(this IFieldContext<T> context , string fieldName)
        {
            if (context == null || string.IsNullOrEmpty(fieldName))
            {
                throw new InvalidOperationException();
            }

            if (context is FieldDatabaseContext<T> fdContext)
            {
                fdContext.FieldId = fieldName;
            }

            return context;
        }

        public static IFieldContext<T , TDBElement> WithFieldName<T, TDBElement>(this IFieldContext<T , TDBElement> context , string fieldName) where TDBElement : BsonValue
        {
            if (context == null || string.IsNullOrEmpty(fieldName))
            {
                throw new InvalidOperationException();
            }

            if (context is FieldDatabaseContext<T , TDBElement> fdContext)
            {
                fdContext.FieldId = fieldName;
            }

            return context;
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  ToLiteCollection
        //
        //-------------------------------------------------------------------------------------------------
        public static ILiteDatabaseContext ToLiteCollection<TElement, TList>(this ICollectionContext<TElement , TList> context , ref ILiteCollection<TElement> collection) where TList : IEnumerable<TElement>
        {
            if (context is CollectionDatabaseContext<TElement , TList> cdContext)
            {
                collection = cdContext.DBCollection;
            }

            return context.Context;
        }

        public static ILiteDatabaseContext ToLiteCollection<TElement, TDBElement>(this IFieldContext<TElement , TDBElement> context , ref ILiteCollection<TDBElement> collection)
        {
            if (context is FieldDatabaseContext<TElement , TDBElement> cdContext)
            {
                collection = cdContext.DBCollection;
            }

            return context.Context;
        }

        public static ILiteDatabaseContext ToLiteCollection<TElement>(this IFieldContext<TElement> context , ref ILiteCollection<TElement> collection)
        {
            if (context is FieldDatabaseContext<TElement> fdContext)
            {
                collection = fdContext.DBCollection;
            }

            return context.Context;
        }
        public static T FindOne<T>(this ILiteCollection<BsonDocument> collection , BsonValue Id)
        {
            var expression = Query.EQ("_id" , Id);
            var document = collection.FindOne(expression).AsDocument;
            return BsonMapper.Global.Deserialize<T>(document);
        }
    }
}
