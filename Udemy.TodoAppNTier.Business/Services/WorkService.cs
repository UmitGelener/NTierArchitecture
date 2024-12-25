using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.Business.ValidationRules;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.UnitofWork;
using Udemy.TodoAppNTier.Dtos.Interfaces;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Domains;

namespace Udemy.TodoAppNTier.Business.Services
{
	public class WorkService : IWorkService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<WorkCreateDto> _createvalidator;
		private readonly IValidator<WorkUpdateDto> _updatevalidator;
		//private readonly IValidator<WorkListDto> _listvalidator;
		public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createvalidator, IValidator<WorkUpdateDto> updatevalidator)
		{
			_uow = uow;
			_mapper = mapper;
			_createvalidator = createvalidator;
			_updatevalidator = updatevalidator;
		}

		public async Task Create(WorkCreateDto dto)
		{
			if (_createvalidator.Validate(dto).IsValid)
			{
				await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
				await _uow.SaveChangesAsync();
			}
		}

		public async Task<List<WorkListDto>> GetAll()
		{
			return _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
		}

		public async Task<IDto> GetById<IDto>(int id)
		{
			var data = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
			return _mapper.Map<IDto>(data);
		}

		public async Task Remove(int id)
		{
			_uow.GetRepository<Work>().Remove(id);
			await _uow.SaveChangesAsync();
		}

		public async Task Update(WorkUpdateDto dto)
		{
			if (_updatevalidator.Validate(dto).IsValid)
			{
				_uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto));
				await _uow.SaveChangesAsync();
			}
		}
	}
}
