using api.DTO.WorkHistories;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
	[Route("api/work_histories")]
	[ApiController]
	public class EmployeesWorkHistoriesController : ControllerBase
	{

		private readonly EmployeesWorkHistoriesRepository _employeesWorkHistoriesRepository;

		public EmployeesWorkHistoriesController(EmployeesWorkHistoriesRepository employeesWorkHistoriesRepository)
		{
			_employeesWorkHistoriesRepository = employeesWorkHistoriesRepository;
		}

		// GET: api/<EmployeesWorkHistoriesController>
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var histories = await _employeesWorkHistoriesRepository.GetAllAsync();
			var results = histories.Select(h => new EmployeeWorkHistoryDTO {
				Id = h.Id,
				Status = h.Status,
				Company = h.Company,
				StartDate = DateTime.Now,
				EndDate = DateTime.Now
			});

			return Ok(results);
		}

		// GET api/<EmployeesWorkHistoriesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{
			try
			{
				var history = await _employeesWorkHistoriesRepository.GetByIdAsync(id);
				var result = new EmployeeWorkHistoryDTO
				{
					Id = history.Id,
					Status = history.Status,
					Company = history.Company,
					StartDate = history.StartDate,
					EndDate = history.EndDate,
				};

				return Ok(result);
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}

		// POST api/<EmployeesWorkHistoriesController>
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateWorkHistoryDTO data)
		{
			var historyId = await _employeesWorkHistoriesRepository.CreateAsync(new() 
			{
				EmployeeId = data.EmployeeId,
				Company = data.Company,
				StartDate = data.StartDate,
				EndDate = data.EndDate,
				Status = data.Status,
			});

			return Ok(historyId);
		}

		// PUT api/<EmployeesWorkHistoriesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<EmployeesWorkHistoriesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{

			try
			{
				await _employeesWorkHistoriesRepository.DeleteByIdAsync(id);
				return NoContent();
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}

		}
	}
}
