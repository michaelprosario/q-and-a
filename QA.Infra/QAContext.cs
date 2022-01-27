using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QA.Core.Entities;

namespace QA.Infra
{
    public class QAContext : IdentityDbContext
    {
        public QAContext(DbContextOptions<QAContext> options) : base(options)
        {
        }

   }
}