using System;
using System.Linq.Expressions;
using System.Text;
using SwissKnife.Diagnostics.Contracts;
using SwissKnife.IdentifierConversion;

namespace SwissKnife
{
    /// <summary>
    /// Contains methods for converting identifier expressions into their string representations.
    /// For example, the identifier expression 'x => x.Property.SubProperty' will be converted into 'Property.SubProperty' and
    /// identifier expression '() => Property' into 'Property'.
    /// </summary>
    public static class Identifier // TODO-IG: Implement the class fully! This is just a temporary version needed for internal projects!
    {
        public static string ToString<T>(Expression<Func<T, object>> identifierExpression) // TODO-IG: Explain in the implementation why this method, if we already have an input of the type Expression<Func<T, Something>> implicite cast to Func<T, object> not possible.
        {
            return ToString<T, object>(identifierExpression, Option<ConversionOptions>.Some(new ConversionOptions()));
        }

        public static string ToString<T, TResult>(Expression<Func<T, TResult>> identifierExpression)
        {
            return ToString(identifierExpression, Option<ConversionOptions>.Some(new ConversionOptions()));
        }

        public static string ToString<T>(Expression<Func<T, object>> identifierExpression, Option<ConversionOptions> identifierOptions)
        {
            return ToString<T, object>(identifierExpression, identifierOptions);
        }

        /// <summary>
        /// Converts <paramref name="identifierExpression"/> into <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">Type whose member identifier has to be converted into <see cref="string"/>.</typeparam>
        /// <param name="identifierExpression"><see cref="Expression"/> that access members of the type <typeparamref name="T"/>.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="identifierExpression"/> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException">If the <paramref name="identifierExpression"/> is not a valid identifier expression. For example: 'someInstance.IdentifierAsString(x => 0)'.</exception>
        public static string ToString<T, TResult>(Expression<Func<T, TResult>> identifierExpression, Option<ConversionOptions> identifierOptions)
        {
            Argument.IsNotNull(identifierExpression, "identifierExpression");

            StringBuilder sb = new StringBuilder();
            bool expressionCanBeConverted = ConvertExpression(sb, identifierExpression.Body, identifierOptions.ValueOr(new ConversionOptions()));
            Argument.IsValid(expressionCanBeConverted, string.Format("The expression is not a valid identifier expression. The expression was: '{0}'.", identifierExpression), "identifierExpression");
            return sb.ToString();
        }

        public static string ToString(Expression<Func<object>> identifierExpression)
        {
            return ToString(identifierExpression, Option<ConversionOptions>.Some(new ConversionOptions()));
        }

        public static string ToString<TResult>(Expression<Func<TResult>> identifierExpression)
        {
            return ToString(identifierExpression, Option<ConversionOptions>.Some(new ConversionOptions()));
        }

        public static string ToString<TResult>(Expression<Func<TResult>> identifierExpression, Option<ConversionOptions> identifierOptions) // TODO-IG: Remove duplicated code.
        {
            Argument.IsNotNull(identifierExpression, "identifierExpression");

            StringBuilder sb = new StringBuilder();
            bool expressionCanBeConverted = ConvertExpression(sb, identifierExpression.Body, identifierOptions.ValueOr(new ConversionOptions()));
            Argument.IsValid(expressionCanBeConverted, string.Format("The expression is not a valid identifier expression. The expression was: '{0}'.", identifierExpression), "identifierExpression");
            return sb.ToString();
        }

        private static bool ConvertExpression(StringBuilder output, Expression expression, ConversionOptions conversionOptions) // TODO-IG: Write comments on all of the methods!
        {
            System.Diagnostics.Debug.Assert(output != null);
            System.Diagnostics.Debug.Assert(expression != null);
            System.Diagnostics.Debug.Assert(conversionOptions != null);

            if (IsUnaryConversion(expression))
                return ConvertUnaryConversion(output, (UnaryExpression)expression, conversionOptions);
            if (IsMemberAccess(expression))
                return ConvertMemberAccess(output, (MemberExpression)expression, conversionOptions);
            if (IsArrayIndex(expression))
                return ConvertArrayIndex(output, (BinaryExpression)expression, conversionOptions);
            if (IsIndexerGetter(expression))
                return ConvertIndexerGetter(output, (MethodCallExpression)expression, conversionOptions);
            if (IsLambdaInputParameter(expression))
                return true;
            return false;
        }

        private static bool IsLambdaInputParameter(Expression expression) // TODO-IG: Write comment. "x => x.A" "() => A"
        {
            return expression.NodeType == ExpressionType.Parameter || expression.NodeType == ExpressionType.Constant;
        }

        private static bool IsUnaryConversion(Expression expression)
        {
            return expression is UnaryExpression && expression.NodeType == ExpressionType.Convert;
        }

        private static bool ConvertUnaryConversion(StringBuilder output, UnaryExpression unaryConvertExpression, ConversionOptions conversionOptions)
        {
            return ConvertExpression(output, unaryConvertExpression.Operand, conversionOptions);
        }

        private static bool IsMemberAccess(Expression expression)
        {
            return expression.NodeType == ExpressionType.MemberAccess;
        }

        private static bool ConvertMemberAccess(StringBuilder output, MemberExpression memberAccessExpression, ConversionOptions conversionOptions)
        {
            output.Insert(0, memberAccessExpression.Member.Name);

            // If there is no containing object stop the recursion.
            // E.g. in the expression "x => x.A.B" the "A" is the containing object of the "B".
            // E.g in the expression "() => String.Empty" there is no containing object for "Empty".
            if (memberAccessExpression.Expression == null)
            {
                switch (conversionOptions.StaticMemberConversion)
                {
                    case StaticMemberConversion.MemberNameOnly: // Ignore it. The member name is already rendered.
                        break;
                    case StaticMemberConversion.ParentTypeName: // Convert to "ParentType.Member".
                        output.Insert(0, conversionOptions.Separator);
                        output.Insert(0, memberAccessExpression.Member.DeclaringType.Name);
                        break;
                    case StaticMemberConversion.ParentTypeFullName: // Convert to "Namespace.ParentType.Member".
                        output.Insert(0, conversionOptions.Separator);
                        output.Insert(0, memberAccessExpression.Member.DeclaringType.FullName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return true;
            }

            // Otherwise, if there is a containing object. E.g. in the expression "x => x.A.B" the "A" is the containing object of the "B".

            if (!IsLambdaInputParameter(memberAccessExpression.Expression)) // If it is not e.g. "x" in the following expression "x => x.A.B".
                output.Insert(0, conversionOptions.Separator);

            return ConvertExpression(output, memberAccessExpression.Expression, conversionOptions);
        }

        private static bool IsArrayIndex(Expression expression)
        {
            return expression.NodeType == ExpressionType.ArrayIndex;
        }

        private static bool ConvertArrayIndex(StringBuilder output, BinaryExpression arrayIndexExpression, ConversionOptions conversionOptions)
        {
            output.Insert(0, ']');
            output.Insert(0, EvaluateExpression(arrayIndexExpression.Right));
            output.Insert(0, '[');

            return ConvertExpression(output, arrayIndexExpression.Left, conversionOptions);
        }

        private static bool IsIndexerGetter(Expression expression)
        {
            if (expression.NodeType != ExpressionType.Call) return false;
            if (!(expression is MethodCallExpression)) return false;
            return ((MethodCallExpression)expression).Method.Name == "get_Item";
        }

        private static bool ConvertIndexerGetter(StringBuilder output, MethodCallExpression indexerGetterExpression, ConversionOptions conversionOptions)
        {
            output.Insert(0, ']');
            output.Insert(0, EvaluateExpression(indexerGetterExpression.Arguments[0]));
            output.Insert(0, '[');

            return ConvertExpression(output, indexerGetterExpression.Object, conversionOptions);
        }

        private static object EvaluateExpression(Expression expression) // TODO-IG: Error handling. What about expression using explicit or implicit (captured variables) parameters? We want to evalute only something that returns integer values? Or maybe strings if the indexer is a string. In that case we probably want something like ["myString"].
        {
            return Expression.Lambda(expression).Compile().DynamicInvoke();
        }
    }
}
