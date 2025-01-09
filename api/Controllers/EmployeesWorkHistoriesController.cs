using api.DTO.WorkHistories;
using api.Errors;
using api.Models;
using api.Repositories;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<ActionResult<IEnumerable<EmployeeWorkHistory>>> Get([FromQuery] FilterWorkHistoryDTO? filter)
		{
			IEnumerable<EmployeeWorkHistory> histories;

			if (filter != null)
			{
				histories = await _employeesWorkHistoriesRepository.GetManyAsync(filter);
			}else
			{
				histories = await _employeesWorkHistoriesRepository.GetAllAsync();
			}


			var results = histories.Select(h => new EmployeeWorkHistoryDTO().FromEntity(h));

			return Ok(results);
		}

		// GET api/<EmployeesWorkHistoriesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<EmployeeWorkHistory>> Get(string id)
		{
			try
			{
				var history = await _employeesWorkHistoriesRepository.GetByIdAsync(id);
				var result = new EmployeeWorkHistoryDTO().FromEntity(history);

				return Ok(result);
			}
			catch (InvalidOperationException)
			{
				return NotFound(EmployeeWorkHistoryErrors.NotFound);
			}
		}

		// POST api/<EmployeesWorkHistoriesController>
		[HttpPost]
		public async Task<ActionResult<string>> Post([FromBody] CreateWorkHistoryDTO data)
		{
			try
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
			catch (ReferenceConstraintException)
			{
				return BadRequest(EmployeeWorkHistoryErrors.EmployeeNotFound);
			}
		}

		// PUT api/<EmployeesWorkHistoriesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(string id, [FromBody] EditWorkHistoryDTO data)
		{
			try
			{
				await _employeesWorkHistoriesRepository.UpdateByIdAsync(id, new EmployeeWorkHistory()
				{
					Company = data.Company,
					StartDate = data.StartDate,
					EndDate = data.EndDate,
					Status = data.Status,
				});

				return Ok();
			}
			catch (InvalidOperationException)
			{
				return NotFound(EmployeeWorkHistoryErrors.NotFound);
			}
			catch (ReferenceConstraintException)
			{
				return BadRequest(EmployeeWorkHistoryErrors.EmployeeNotFound);
			}
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
			catch (InvalidOperationException)
			{
				return NotFound(EmployeeWorkHistoryErrors.NotFound);
			}

		}
	}
}
