﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.Interfaces;
using Udemy.TodoAppNTier.DataAccess.Repositories;
using Udemy.TodoAppNTier.Entities;

namespace Udemy.TodoAppNTier.DataAccess.UnitofWork
{
	public class Uow : IUow
	{
		private readonly TodoContext _context;

		public Uow(TodoContext context)
		{
			_context = context;
		}

		public IRepository<T> GetRepository<T>() where T : BaseEntity
		{
			return new Repository<T>(_context);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
