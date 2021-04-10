using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Views
{
    public static class NavigateContextExtensions
    {
        public static void Accept(this INavigateContext context)
        {

        }

        public static void Refuse(this INavigateContext context, IRoutableViewModel intendViewModel, INavigateParameter parameters)
        {

        }

        public static void Refuse(this INavigateContext context, Type intendViewModel, INavigateParameter parameters)
        {

        }

        public static void Refuse<TViewModel>(this INavigateContext context, INavigateParameter parameters)
        {

        }
    }
}
