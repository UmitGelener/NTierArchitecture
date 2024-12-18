﻿using Microsoft.EntityFrameworkCore;
using Udemy.TodoAppNTier.DataAccess.Configurations;
using Udemy.TodoAppNTier.Entities.Domains;

namespace Udemy.TodoAppNTier.DataAccess.Contexts
{
	public class TodoContext : DbContext
	{
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new WorkConfiguration());
		}
		public DbSet<Work> Works { get; set; }
    }
}
