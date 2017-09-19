using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rhyous.StringLibrary
{
    public class ExpressionMethodDictionary : Dictionary<string,Func<Expression, Expression, Expression>>
    {
        #region Singleton

        private static readonly Lazy<ExpressionMethodDictionary> Lazy = new Lazy<ExpressionMethodDictionary>(() => new ExpressionMethodDictionary());

        public static ExpressionMethodDictionary Instance { get { return Lazy.Value; } }

        internal ExpressionMethodDictionary()
        {
            Add("eq", (a, b) => Expression.Equal(a, b));
            Add("ne", (a, b) => Expression.NotEqual(a, b));
            Add("gt", (a, b) => Expression.GreaterThan(a, b));
            Add("ge", (a, b) => Expression.GreaterThanOrEqual(a, b));
            Add("lt", (a, b) => Expression.LessThan(a, b));
            Add("le", (a, b) => Expression.LessThanOrEqual(a, b));
        }

        #endregion
    }
}
