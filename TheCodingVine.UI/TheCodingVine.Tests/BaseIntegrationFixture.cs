using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Model;

namespace TheCodingVine.Tests
{
	internal class BaseIntegrationTestFixture
	{
		protected TheCodingVineDbContext TestContext => SetUpFixture.TestContext;

		protected BaseIntegrationTestFixture()
		{

		}

		[TearDown]
		public void ResetChangeTracker()
		{
			IEnumerable<DbEntityEntry> changedEntriesCopy = TestContext.ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added ||
							e.State == EntityState.Modified ||
							e.State == EntityState.Deleted
							);
			foreach (DbEntityEntry entity in changedEntriesCopy)
			{
				TestContext.Entry(entity.Entity).State = EntityState.Detached;
			}
		}
	}
}
