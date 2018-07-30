using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Model;

namespace TheCodingVine.Tests
{
	[SetUpFixture]
	internal sealed class SetUpFixture
	{
		static string testConnection = "TheCodingVineTest";

		internal static TheCodingVineDbContext TestContext = new TheCodingVineDbContext(testConnection);

		[OneTimeSetUp]
		public void Initialize()
		{
			Database.SetInitializer(new DropCreateDatabaseAlwaysAndSeed());
			TestContext.Database.Initialize(false);


		}
	}
}
