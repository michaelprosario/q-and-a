using Microsoft.EntityFrameworkCore;
using QA.Core.Entities;

namespace QA.Infra
{
    public class QAContext : DbContext
    {
        public QAContext(DbContextOptions<QAContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<EntityView> EntityView { get; set; }
        public DbSet<EntityVote> EntityVote { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }

    }
}