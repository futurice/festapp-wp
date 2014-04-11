using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FestApp.Utils
{
    public static class PropertySupport
    {
        public static T ExtractPropertyValue<T>(this object obj, Expression<Func<T>> propertyExpression)
        {
            MemberExpression body = propertyExpression.Body as MemberExpression;
            PropertyInfo member = body.Member as PropertyInfo;
            return (T)member.GetValue(obj, null);
        }

        public static void SetPropertyValue<T>(this object obj, Expression<Func<T>> propertyExpression, T newValue)
        {
            MemberExpression body = propertyExpression.Body as MemberExpression;
            PropertyInfo member = body.Member as PropertyInfo;
            member.SetValue(obj, newValue, null);
        }

        public static string ExtractPropertyName<T>(this Expression<Func<T>> propertyExpression)
        {
            MemberExpression body = propertyExpression.Body as MemberExpression;
            PropertyInfo member = body.Member as PropertyInfo;
            return body.Member.Name;
        }
    }
}