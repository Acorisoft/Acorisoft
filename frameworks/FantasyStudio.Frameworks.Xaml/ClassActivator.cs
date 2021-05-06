
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Acorisoft.Frameworks
{
    public static class ClassActivator
    {
        /// <summary>
        /// ����ָ�����͵�ʵ����
        /// </summary>
        /// <typeparam name="T">ָ��Ҫ������ʵ�����͡�</typeparam>
        /// <returns>����һ���´�����ʵ�����͡�</returns>
        public static T CreateInstance<T>() => TypeCache<T>.Activator();

        /// <summary>
        /// ����Lambda���͡�
        /// </summary>
        /// <typeparam name="T">��Ҫ������ʵ�����͡�</typeparam>
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
            // Ϊʲôʹ��TypeInfo������Type
            // 
            //  https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo?view=net-5.0
            //
            // ͨ������ٷ���վ�Ķ������Ƿ���TypeInfo��Ҫ����Microsoft Store�������档
            Debug.Assert(typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()));
            //
            // �ҵ����캯���зǾ�̬���캯�������޲����Ĺ��캯����
            var constructor = type.GetTypeInfo()
                                  .DeclaredConstructors
                                  .Where(x => !x.IsStatic && !x.GetParameters().Any())
                                  .FirstOrDefault();
            //
            // ʹ��Lambda���ʽʵ�ִ���ָ�����͵�ʵ����
            return Expression.Lambda<Func<T>>(Expression.New(constructor), null).Compile();
        }
    }

}
