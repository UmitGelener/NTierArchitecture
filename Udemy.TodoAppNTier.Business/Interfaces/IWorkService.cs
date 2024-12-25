﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTier.Business.Interfaces
{
	public interface IWorkService
	{
		Task<List<WorkListDto>> GetAll();

		Task Create(WorkCreateDto dto);

		Task<IDto> GetById<IDto>(int id);

		Task Remove(int id);

		Task Update(WorkUpdateDto dto);
	}
}
