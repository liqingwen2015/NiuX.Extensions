using System.Linq.Expressions;

namespace System.Linq;

/// <summary>
/// 表达式
/// </summary>
/// <remarks>常规的方式不能直接拼接表达式</remarks>
public static class NiuXExpressionExtensions
{


    /// <summary>
    /// AndAlso
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="leftExpression"></param>
    /// <param name="rightExpression"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> leftExpression,
        Expression<Func<T, bool>> rightExpression)
    {
        return leftExpression.Merge(rightExpression, Expression.AndAlso);
    }


    /// <summary>
    /// Or
    /// </summary>
    /// <returns></returns>
    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> leftExpression,
        Expression<Func<T, bool>> rightExpression)
    {
        return leftExpression.Merge(rightExpression, Expression.OrElse);
    }

    /// <summary>
    /// Not，取反
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        return expression == null
            ? null
            : Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters[0]);
    }

    /// <summary>
    /// 合并
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="leftExpression"></param>
    /// <param name="rightExpression"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    private static Expression<Func<T, bool>> Merge<T>(this Expression<Func<T, bool>> leftExpression,
        Expression<Func<T, bool>> rightExpression, Func<Expression, Expression, Expression> func)
    {
        if (leftExpression == null)
        {
            return rightExpression;
        }

        if (rightExpression == null)
        {
            return leftExpression;
        }

        var newParameter = Expression.Parameter(typeof(T), "x");
        var visitor = new ParameterReplaceExpressionVisitor(newParameter);

        var left = visitor.ReplaceParameter(leftExpression.Body);
        var right = visitor.ReplaceParameter(rightExpression.Body);
        var body = func(left, right);

        return Expression.Lambda<Func<T, bool>>(body, newParameter);
    }

    /// <summary>
    /// 参数替换表达式访问器
    /// </summary>
    private class ParameterReplaceExpressionVisitor : ExpressionVisitor
    {
        public ParameterReplaceExpressionVisitor(ParameterExpression parameterExpression)
        {
            NewParameterExpression = parameterExpression;
        }

        private ParameterExpression NewParameterExpression { get; }

        /// <summary>
        /// 替换参数
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Expression ReplaceParameter(Expression exp)
        {
            return Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return NewParameterExpression;
        }
    }
}