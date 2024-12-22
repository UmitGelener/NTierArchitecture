using AutoMapper;
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
		private readonly IMapper _mapper;

		public WorkService(IUow uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		public async Task Create(WorkCreateDto dto)
		{
			await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
			await _uow.SaveChangesAsync();
		}

		public async Task<List<WorkListDto>> GetAll()
		{
			return _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
		}

		public async Task<WorkListDto> GetById(int id)
		{
			var data = await _uow.GetRepository<Work>().GetByFilter(x=>x.Id == id);
			return _mapper.Map<WorkListDto>(data);
		}

		public async Task Remove(int id)
		{
			_uow.GetRepository<Work>().Remove(id);
			await _uow.SaveChangesAsync();
		}

		public async Task Update(WorkUpdateDto dto)
		{
			_uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto));

			await _uow.SaveChangesAsync();
		}
	}
}
