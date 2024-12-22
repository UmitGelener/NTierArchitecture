using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTier.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IWorkService _workService;
		private readonly IMapper _mapper;
		public HomeController(IWorkService workService, IMapper mapper)
		{
			_workService = workService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var workList = await _workService.GetAll();
			return View(workList);
		}

		public IActionResult Create()
		{
			return View(new WorkCreateDto());
		}
		[HttpPost]
		public async Task<IActionResult> Create(WorkCreateDto dto)
		{
			if (ModelState.IsValid)
			{
				await _workService.Create(dto);
				return RedirectToAction("Index");
			}
			return View(dto);
		}

		public async Task<IActionResult> Update(int id)
		{
			var listDto = await _workService.GetById(id);

			var updateDto = _mapper.Map<WorkUpdateDto>(listDto);

			return View(updateDto);
		}

		[HttpPost]
		public async Task<IActionResult> Update(WorkUpdateDto dto)
		{
			if (ModelState.IsValid)
			{
				await _workService.Update(dto);
				return RedirectToAction("Index");
			}
			return View(dto);
		}

		public async Task<IActionResult> Remove(int id)
		{
			await _workService.Remove(id);
			return RedirectToAction("Index");
		}

	}
}
