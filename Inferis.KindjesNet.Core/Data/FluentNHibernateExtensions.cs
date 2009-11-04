using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;

namespace Inferis.KindjesNet.Core.Data
{
    public static class FluentNHibernateExtensions
    {
        public static T AddMany<T>(this SetupConventionFinder<T> finder, IEnumerable<IConvention> conventions)
        {
            return finder.Add(conventions.ToArray());
        }
    }
}
