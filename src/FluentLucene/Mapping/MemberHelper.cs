using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Helper and extension methods for <see cref="IMember"/>
    /// </summary>
    internal static class MemberHelper
    {
        /// <summary>
        /// Transforms an expression into a member
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate represented by the expression</typeparam>
        /// <param name="expression">The expression</param>
        /// <returns>The member representing the expression</returns>
        /// <exception cref="ArgumentException">If the expression is not a valid member expression</exception>
        public static IMember ToMember<TDelegate>(this Expression<TDelegate> expression)
        {
            // Get the member expression
            var memberExpression = expression.ToMemberExpression();

            // Create a member using the expression
            return memberExpression.ToMember();
        }

        /// <summary>
        /// Transforms a member expression into a member
        /// </summary>
        /// <param name="expression">The member expression</param>
        /// <returns>The member representing the expression</returns>
        /// <exception cref="ArgumentException">If the member expression is not supported to become a member</exception>
        public static IMember ToMember(this MemberExpression expression)
        {
            // If the member is a property, create a member and return it
            if (expression.Member is PropertyInfo)
                return new PropertyMember((PropertyInfo) expression.Member);

            // If the member is a field, create a member and return in
            if (expression.Member is FieldInfo)
                return new FieldMember((FieldInfo) expression.Member);

            // Otherwise, the member is not supported
            throw new ArgumentException("Only properties and fields are supported for mapping", "expression");
        }

        /// <summary>
        /// Gets the member expression in the expression
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate represented by the expression</typeparam>
        /// <param name="expression">The expression</param>
        /// <returns>The member expression</returns>
        public static MemberExpression ToMemberExpression<TDelegate>(this Expression<TDelegate> expression)
        {
            // Get the body of the expression
            var body = expression.Body;

            // Unwrap any convert expression
            body = body.UnwrapConvert();

            // If the expression is a member expression
            if (body is MemberExpression)
            {
                // Cast the expression and return it
                return (MemberExpression) body;
            }

            // Otherwise, throw an exception. The expression is not a member expression
            throw new ArgumentException("The expression doesn't represent a member expression", "expression");
        }

        /// <summary>
        /// Unwraps convert operation potentially applied to the expression
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>The expression, with convertion expressions removed</returns>
        private static Expression UnwrapConvert(this Expression expression)
        {
            // If the expression is a convertion expression
            if (expression.NodeType == ExpressionType.Convert)
            {
                // Cast the expression as an unary expression;
                var unaryExpression = (UnaryExpression) expression;

                // Un wrap it and call this method recursively
                return UnwrapConvert(unaryExpression.Operand);
            }

            // Otherwise, return the expression itself
            return expression;
        }
    }
}