﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.Interfaces;
using Udemy.TodoAppNTier.Entities;

namespace Udemy.TodoAppNTier.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T: BaseEntity
	{
		private readonly TodoContext _context;

		public Repository(TodoContext context)
		{
			_context = context;
		}

		public async Task Create(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public async Task<List<T>> GetAll()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false)
		{
			var query = asNoTracking
				? _context.Set<T>().AsNoTracking()
				: _context.Set<T>();

			return await query.SingleOrDefaultAsync(filter);
		}

		public async Task<T> GetById(object id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public IQueryable<T> GetQuery()
		{
			return _context.Set<T>().AsQueryable();
		}

		public void Remove(object id)
		{
			var deletedEntity = _context.Set<T>().Find(id);
			_context.Set<T>().Remove(deletedEntity);
		}

		public void Update(T entity)
		{
			var updateEntity = _context.Set<T>().Find(entity.Id);
			_context.Entry(updateEntity).CurrentValues.SetValues(entity);
		}
	}
}
