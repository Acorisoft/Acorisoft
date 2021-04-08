using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Acorisoft.Morisa.Core
{
    public static class ClassActivator
    {
        /// <summary>
        /// 创建指定类型的实例。
        /// </summary>
        /// <typeparam name="T">指定要创建的实例类型。</typeparam>
        /// <returns>返回一个新创建的实例类型。</returns>
        public static T CreateInstance<T>() => TypeCache<T>.Activator();

        /// <summary>
        /// 缓存Lambda类型。
        /// </summary>
        /// <typeparam name="T">需要创建的实例类型。</typeparam>
        private class TypeCache<T>
        {
            public static readonly Func<T> Activator = ClassActivator<T>.CreateInstance(typeof(T));
        }
    }

    static class ClassActivator<T>
    {
        public static Func<T> CreateInstance(Type type)
        {
            //
            // 为什么使用TypeInfo而不是Type
            // 
            //  https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo?view=net-5.0
            //
            // 通过浏览官方网站的定义我们发现TypeInfo主要用于Microsoft Store程序里面。

            Debug.Assert(typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()));

            //
            // 找到构造函数中非静态构造函数并且无参数的构造函数。
            var constructor = type.GetTypeInfo()
                                  .DeclaredConstructors
                                  .Where(x => !x.IsStatic && !x.GetParameters().Any())
                                  .FirstOrDefault();

            //
            // 使用Lambda表达式实现创建指定类型的实例。
            return Expression.Lambda<Func<T>>(Expression.New(constructor), null).Compile();
        }
    }
}
