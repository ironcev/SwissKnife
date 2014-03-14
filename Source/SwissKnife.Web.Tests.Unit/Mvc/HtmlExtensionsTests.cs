using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NUnit.Framework;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class HtmlExtensionsTests
    {
        #region Experiments
        /// <remarks>
        /// Trying out <see cref="ExpressionHelper.GetExpressionText(LambdaExpression)"/> because of the issue #2.
        /// </remarks>
        [Test]
        [Ignore("Experiment")]
        public void ExpressionHelper_GetExpressionText_WithReferenceTypes()
        {
            Expression<Func<TestClass, object>> lambdaExpression = x => x.ReferenceTypeProperty;
            Assert.That(ExpressionHelper.GetExpressionText(lambdaExpression), Is.EqualTo("ReferenceTypeProperty"));
        }

        /// <remarks>
        /// Trying out <see cref="ExpressionHelper.GetExpressionText(LambdaExpression)"/> because of the issue #2.
        /// </remarks>
        [Test]
        [Ignore("Experiment")]
        public void ExpressionHelper_GetExpressionText_WithValueTypes()
        {
            Expression<Func<TestClass, object>> lambdaExpression = x => x.ValueTypeProperty;
            //Assert.That(ExpressionHelper.GetExpressionText(lambdaExpression), Is.EqualTo("ValueTypeProperty"));

            // The above line asserts with the following message:
            //   Expected string length 17 but was 0. Strings differ at index 0.
            //   Expected: "ValueTypeProperty"
            //   But was:  <string.Empty>
            // It looks like that ExpressionHelper.GetExpressionText() does not work with value types as expected.
            // It returns empty string as demonstrated below.

            Assert.That(ExpressionHelper.GetExpressionText(lambdaExpression), Is.Empty);
        }
        #endregion

        #region TextBoxFor<T>
        /// <remarks>
        /// Covers issue #2.
        /// </remarks>
        [Test]
        public void TextBoxFor_ExpressionRepresentValueTypeProperty_ReturnsInputTag()
        {
            HtmlHelper htmlHelper = TestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty).ToString(),
                        Is.EqualTo(@"<input id=""ValueTypeProperty"" name=""ValueTypeProperty"" type=""text"" value="""" />"));
        }
        #endregion

        // ReSharper disable ClassNeverInstantiated.Local
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        private class TestClass
        {
            public object ReferenceTypeProperty { get; set; }
            public int ValueTypeProperty { get; set; }
        }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
        // ReSharper restore ClassNeverInstantiated.Local
    }
    // ReSharper restore InconsistentNaming
}
