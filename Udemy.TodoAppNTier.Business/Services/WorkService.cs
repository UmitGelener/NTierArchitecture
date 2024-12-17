using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.UnitofWork;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Domains;

namespace Udemy.TodoAppNTier.Business.Services
{
	public class WorkService : IWorkService
	{
		private readonly IUow _uow;

		public WorkService(IUow uow)
		{
			_uow = uow;
		}

		public async Task Create(WorkCreateDto dto)
		{
			await _uow.GetRepository<Work>().Create(new()
			{
				Definition = dto.Definition,
				IsCompleted = dto.IsCompleted
			});

			await _uow.SaveChangesAsync();
		}

		public async Task<List<WorkListDto>> GetAll()
		{
			var list = await _uow.GetRepository<Work>().GetAll();

			var workList = new List<WorkListDto>();

			if(list != null && list.Count > 0)
			{
				foreach (var work in list)
				{
					workList.Add(new()
					{
						Id = work.Id,
						Definition = work.Definition,
						IsCompleted = work.IsCompleted
					});
				}
			}
			return workList;
		}

		public async Task<WorkListDto> GetById(object id)
		{
			var data = await _uow.GetRepository<Work>().GetById(id);
			return new()
			{
				Id = data.Id,
				Definition = data.Definition,
				IsCompleted = data.IsCompleted
			};
		}

		public async Task Remove(object id)
		{
			var deleteEntity = await _uow.GetRepository<Work>().GetById(id);

			_uow.GetRepository<Work>().Remove(deleteEntity);
			await _uow.SaveChangesAsync();
		}

		public async Task Update(WorkUpdateDto dto)
		{
			_uow.GetRepository<Work>().Update(new()
			{
				Id = dto.Id,
				Definition = dto.Definition,
				IsCompleted = dto.IsCompleted
			});

			await _uow.SaveChangesAsync();
		}
	}
}
