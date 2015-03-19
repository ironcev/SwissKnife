using NUnit.Framework;

namespace SwissKnife.Tests.Integration.EntityFramework
{
    public abstract class BaseEntityFrameworkTests
    {
        protected EntityFrameworkTestDatabaseContext DbContext { get; private set; }

        [SetUp]
        public void SetUp()
        {
            DbContext = new EntityFrameworkTestDatabaseContext();

            DbContext.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", Identifier.ToString<EntityFrameworkTestDatabaseContext>(x => x.TestEntities)));
        }
    }
}
