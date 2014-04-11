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
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

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

        /// <remarks>
        /// Covers issue #4.
        /// </remarks>
        [Test]
        public void TextBoxFor_AttributeDefined_RendersAttribute()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null, accesskey => "a").ToString()
                        .Contains(@" accesskey=""a"" "));
        }

        /// <remarks>
        /// Covers issue #4.
        /// </remarks>
        [Test]
        public void TextBoxFor_DataAttributeDefined_ReturnsRenderedDataAttribute()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null, dataCustom => "custom data").ToString()
                        .Contains(@" data-custom=""custom data"" "));
        }

        /// <remarks>
        /// Covers issue #4.
        /// </remarks>
        [Test]
        public void TextBoxFor_CamelCaseAttributeDefined_ReturnsRenderedAttribute()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null, camelCaseAttributeName => 101).ToString()
                        .Contains(@" camel-case-attribute-name=""101"" "));
        }

        [Test]
        public void TextBoxFor_PascalCaseAttributeDefined_ReturnsRenderedAttribute()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null, PascalCaseAttributeName => 101).ToString()
                        .Contains(@" pascal-case-attribute-name=""101"" "));
        }

        [Test]
        public void TextBoxFor_AttributeNameHasSingleUpperCaseCharacter_ReturnsRenderedAttribute()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null, A => 101).ToString()
                        .Contains(@" a=""101"" "));
        }

        [Test]
        public void TextBoxFor_AttributeNameHasSingleLoweCaseCharacter_ReturnsRenderedAttribute()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null, a => 101).ToString()
                        .Contains(@" a=""101"" "));
        }

        [Test]
        public void TextBoxFor_AttributeNameHasOnlyCapitalCharacter_ReturnsRenderedAttribute()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            Assert.That(htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null, CAPITALS => 101).ToString()
                        .Contains(@" c-a-p-i-t-a-l-s=""101"" "));
        }

        [Test]
        public void TextBoxFor_SeveralAttributesDefined_ReturnsRenderedAttributes()
        {
            HtmlHelper htmlHelper = MvcTestHelper.GetHtmlHelper();

            var result = htmlHelper.TextBoxFor<TestClass>(x => x.ValueTypeProperty, null,
                                                          accesskey => "a",
                                                          dataCustom => "custom data",
                                                          camelCaseAttributeName => 1,
                                                          PascalCaseAttributeName => 1.2,
                                                          A => 1.23,
                                                          CAPITALS => 101).ToString();

            var expectedAttributes = new[]
            {
                @" accesskey=""a"" ",
                @" data-custom=""custom data"" ",
                @" camel-case-attribute-name=""1"" ",
                @" pascal-case-attribute-name=""1.2"" ",
                @" a=""1.23"" ",
                @" c-a-p-i-t-a-l-s=""101"" "
            };


            foreach (var expectedAttribute in expectedAttributes)
            {
                Assert.That(result.Contains(expectedAttribute));
            }
        }
    }
    // ReSharper restore InconsistentNaming
}
