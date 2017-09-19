#if NET461
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Rhyous.StringLibrary
{
    public static class PropertyNameLambdaExtensions
    {
        /// <summary>
        /// Creates a lambda out of a property name that when called should return the property value.
        /// </summary>
        /// <typeparam name="T">The type of teh object the lambda will run against.</typeparam>
        /// <typeparam name="TResult">The value type that the lambda should return when processed.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>An Expression that should return the property value when called.</returns>
        public static Expression<Func<T, TResult>> ToLambda<T, TResult>(this string propertyName)
        {
            var param = Expression.Parameter(typeof(T));
            var body = Expression.PropertyOrField(param, propertyName);
            return Expression.Lambda<Func<T, TResult>>(body, param);
        }


        /// <summary>
        /// Creates a lambda out of a property name and a value that should return true or false whether the property value equals the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the object the lambda will run against.</typeparam>
        /// <typeparam name="TInput">The type of the specified input value.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value to compare.</param>
        /// <param name="methodName">The method. It can be a method or it can be a any OData filter operator. The default is eq. Any valid method could be used. For example, if TInput is string, StartsWith or EndsWith, etc., would be options.</param>
        /// <returns>An Expression that when called should return true or false whether the property value equals the specified value.</returns>
        public static Expression<Func<T, bool>> ToLambda<T, TInput>(this string propertyName, TInput value, string methodName = "eq", bool not = false)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "e");
            Expression left = Expression.Property(parameter, propertyName);
            Expression right = Expression.Constant(value);
            Expression method = null;
            Func<Expression, Expression, Expression> func;
            if (ExpressionMethodDictionary.Instance.TryGetValue(methodName, out func))
            {
                method = func.Invoke(left, right);
            }
            else
            {
                var methodInfo = typeof(TInput).GetMethod(methodName, new[] { typeof(TInput) });
                method = Expression.Call(left, methodInfo, right);
            }
            return Expression.Lambda<Func<T, bool>>(not? Expression.Not(method) : method, parameter);
        }

        /// <summary>
        /// Allows for calling ToLambda{T, TInput} when the TInput type is only known at runtime.
        /// </summary>
        /// <typeparam name="T">The type of the object the lambda will run against.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="Type">The type of the object the lambda will run against.</param>
        /// <param name="parameters">The parameters to pass into the ToLambda{T, TInput} method as an object array.</param>
        /// <returns>An Expression that when called should return true or false whether the property value equals the specified value.</returns>
        public static Expression<Func<T, bool>> ToLambda<T>(this string propertyName, Type type, params object[] parameters)
        {
            var lambdaMethod = typeof(PropertyNameLambdaExtensions).GetMethods().Where(m => m.Name == "ToLambda" && m.IsGenericMethod && m.GetParameters().Length == 4 && m.GetParameters()[1].ParameterType.IsGenericParameter).FirstOrDefault();
            lambdaMethod = lambdaMethod.MakeGenericMethod(typeof(T), type);
            var newParams = new object[4];
            newParams[0] = propertyName;
            parameters.CopyTo(newParams, 1);
            var missingParams = 3 - parameters.Length;
            while (missingParams > 0)
                newParams[newParams.Length - missingParams--] = Type.Missing;
            var expression = lambdaMethod.Invoke(null, newParams);
            return expression as Expression<Func<T, bool>>;
        }

        /// <summary>
        /// Creates a lambda out of a property name and a list of values that should return true or false whether the property value is contained in the specified value list.
        /// </summary>
        /// <typeparam name="T">The type of the object the lambda will run against.</typeparam>
        /// <typeparam name="TInput">The type of the specified List of input values.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="values">A list of values.</param>
        /// <param name="methodName">Contains is the default. Any valid method could be used. For example, if TInput is string, StartsWith or EndsWith, etc., would be options.</param>
        /// <returns>An Expression that when called should return true or false whether the property value is contained in the list of specified value.</returns>
        public static Expression<Func<T, bool>> ToLambda<T, TInput>(this string propertyName, List<TInput> values, string methodName = "Contains")
        {
            var methodInfo = typeof(List<TInput>).GetMethod(methodName, new Type[] { typeof(TInput) });
            var list = Expression.Constant(values);
            var param = Expression.Parameter(typeof(T), "e");
            var value = Expression.Property(param, propertyName);
            var body = Expression.Call(list, methodInfo, value);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
#endif