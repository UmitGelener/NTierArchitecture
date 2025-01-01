using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Extensions;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.Business.ValidationRules;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.UnitofWork;
using Udemy.TodoAppNTier.Dtos.Interfaces;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Domains;
using Udemy.ToDoAppNTier.Common.ResponseObject;

namespace Udemy.TodoAppNTier.Business.Services
{
	public class WorkService : IWorkService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<WorkCreateDto> _createvalidator;
		private readonly IValidator<WorkUpdateDto> _updatevalidator;

		public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createvalidator, IValidator<WorkUpdateDto> updatevalidator)
		{
			_uow = uow;
			_mapper = mapper;
			_createvalidator = createvalidator;
			_updatevalidator = updatevalidator;
		}

		public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
		{

			var validationResult = _createvalidator.Validate(dto);
			if (validationResult.IsValid)
			{
				await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
				await _uow.SaveChangesAsync();
				return new Response<WorkCreateDto>(ResponseType.Success, dto);
			}
			else
			{
				return new Response<WorkCreateDto>(ResponseType.ValidationError, dto, validationResult.ConvertToCustomValidationError());
			}
		}

		public async Task<IResponse<List<WorkListDto>>> GetAll()
		{
			var data = _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
			return new Response<List<WorkListDto>>(ResponseType.Success, data);
			
		}

		public async Task<IResponse<IDto>> GetById<IDto>(int id)
		{
			var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
			if (data == null)
				return new Response<IDto>(ResponseType.NotFound, $"{id} değeri bulunamadı.");
			return new Response<IDto>(ResponseType.Success, data);
		}

		public async Task<IResponse> Remove(int id)
		{
			var entity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
			if (entity != null)
			{
				_uow.GetRepository<Work>().Remove(entity);
				await _uow.SaveChangesAsync();
				return new Response(ResponseType.Success);
			}
			return new Response(ResponseType.NotFound, $"{id} değeri bulunamadı.");
		}

		public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
		{
			var result = _updatevalidator.Validate(dto);
			if (result.IsValid)
			{
				var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
				if(updatedEntity != null)
				{
					_uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto), updatedEntity);
					await _uow.SaveChangesAsync();
					return new Response<WorkUpdateDto>(ResponseType.Success, dto);
				}
				return new Response<WorkUpdateDto>(ResponseType.NotFound, $"{dto.Id} değeri bulunamadı.");
			} 
			else
			{
				return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
			}
		}
	}
}
