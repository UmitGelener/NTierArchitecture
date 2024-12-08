using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Entities.Domains;

namespace Udemy.TodoAppNTier.DataAccess.Configurations
{
	public class WorkConfiguration : IEntityTypeConfiguration<Work>
	{
		public void Configure(EntityTypeBuilder<Work> builder)
		{
			builder.Property(x => x.Id);

			builder.Property(x => x.Definition).HasMaxLength(300);
			builder.Property(x => x.Definition).IsRequired();

			builder.Property(x => x.IsCompleted).IsRequired(true);

		}
	}
}
