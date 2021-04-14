﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using MediatR;
using Acorisoft.Morisa;
using Acorisoft.Morisa.Composition;

namespace Acorisoft.Morisa
{
    public static class MorisaSystem
    {
        public static IContainer UseMorisa(this IContainer container)
        {
            //
            // 创建ServiceFactory代理
            container.RegisterDelegate<ServiceFactory>(f => f.Resolve);

            //
            // 创建 ICompositionSetMediator 中介者
            container.RegisterInstance<ICompositionSetMediator>(new CompositionSetMediator(container.Resolve<ServiceFactory>()));

            //
            // 注册所有System

            return container;
        }
    }
}
