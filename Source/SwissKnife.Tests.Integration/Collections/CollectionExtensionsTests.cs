using System.Linq;
using NUnit.Framework;
using SwissKnife.Collections;
using SwissKnife.Tests.Integration.EntityFramework;

namespace SwissKnife.Tests.Integration.Collections
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class CollectionExtensionsTests
    {
        #region Split<T>
        /// <summary>
        /// The original implementation of the <see cref="CollectionExtensions.Split{T}"/> method
        /// was not "Enterprise Framework friendly".
        /// Because of the execution of the original query as a sub-query of that same query,
        /// Entity Framework was throwing the following exception when <see cref="CollectionExtensions.Split{T}"/>
        /// was called directly on the Entity Framework query object:
        /// "There is already an open DataReader associated with this Command which must be closed first."
        /// </summary>
        [Test]
        public void Split_splits_enumerables_coming_from_Entity_Framework_query_objects()
        {
            var dbContext = new EntityFrameworkTestDatabaseContext();

            for (int i = 0; i < 10; i++)
                dbContext.TestEntities.Add(new TestEntity());

            dbContext.SaveChanges();

            IQueryable<TestEntity> query = dbContext.Set<TestEntity>();

            // Note that we are actually not testing here if the Split() method
            // works properly. We have unit tests to check that.
            // In this case, we are only testing that we can iterate over the result.
            foreach (var testEntity in query.Split(1))
                // THe entity variable is intentionally not used - we are not interested in its content.
                // ReSharper disable UnusedVariable
                foreach (var entity in testEntity) { }
                // ReSharper restore UnusedVariable            
        }
        #endregion
    }

    // ReSharper restore InconsistentNaming
}
