﻿using System;

namespace Acorisoft.Extensions.Platforms.Services
{
    /// <summary>
    /// <see cref="ServiceProvider"/> 类型用于提供一个不依赖具体容器的服务提供者实现。
    /// </summary>
    public static class ServiceProvider
    {
        public static void SetServiceProvider(IServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public static object? GetService(Type serviceType)
        {
            return Provider?.GetService(serviceType);
        }

        private static IServiceProvider Provider { get; set; }
    }
} 