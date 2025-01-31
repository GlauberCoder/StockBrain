using System.Linq.Expressions;
using System.Reflection;

namespace StockBrain.Utils;

public static class ExpressionsExtensions
{
	public static PropertyInfo GetPropertyInfo<TEntity, TType>(this Expression<Func<TEntity, TType>> expression)
	{
		PropertyInfo propertyInfo = null;

		if (expression.Body is UnaryExpression)
		{
			var UnExp = (UnaryExpression)expression.Body;
			if (UnExp.Operand is MemberExpression)
				propertyInfo = (PropertyInfo)((MemberExpression)UnExp.Operand).Member;
		}
		else if (expression.Body is MemberExpression)
		{
			propertyInfo = (PropertyInfo)((MemberExpression)expression.Body).Member;
		}
		return propertyInfo;
	}
}
