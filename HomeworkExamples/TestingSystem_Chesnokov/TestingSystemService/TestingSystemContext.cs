using System.Data.Entity;

namespace TestingSystemService
{
    public class TestingSystemContext : DbContext
    {
        public class InitializeContext : DropCreateDatabaseIfModelChanges<TestingSystemContext>
        {
            protected override void Seed(TestingSystemContext context)
            {

            }
        }

        public TestingSystemContext() : base("TestingSystemDataBaseBCS")
        {
            Database.SetInitializer(new InitializeContext());
        }

        public DbSet<Test> Tests { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<TestResult> Results { get; set; }

        public DbSet<QuestionWithAnswer> QuestionWithAnswers { get; set; }
    }
}