﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DbWpfApp.Services
{
    public class DISource : MarkupExtension
    {
        public static Func<Type, object, string, object> Resolver { get; set; }
        public Type Type { get; set; }
        public object Key { get; set; }
        public string Name { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider) => Resolver?.Invoke(Type, Key, Name);
    }
}
