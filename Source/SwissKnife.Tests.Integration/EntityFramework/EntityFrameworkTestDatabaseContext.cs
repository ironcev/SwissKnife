using System.Data.Entity;

namespace SwissKnife.Tests.Integration.EntityFramework
{
    public class EntityFrameworkTestDatabaseContext : DbContext
    {
        public IDbSet<TestEntity> TestEntities { get; set; } 

        public EntityFrameworkTestDatabaseContext() : base("EntityFrameworkIntegrationTests")
        {            
        }
    }
}
