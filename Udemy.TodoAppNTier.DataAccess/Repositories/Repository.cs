﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.Interfaces;

namespace Udemy.TodoAppNTier.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T: class , new()
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

		void IRepository<T>.Remove(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		void IRepository<T>.Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}
	}
}
