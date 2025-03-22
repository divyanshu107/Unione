using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Unione.Models
{
	public class AppDBContext: DbContext
	{
		public DbSet<UserModel> Users { get; set; }
		public DbSet<PhotoModel> Photos { get; set; }

		public AppDBContext(DbContextOptions<AppDBContext> options)
		: base(options)
		{
		}
	}
}
