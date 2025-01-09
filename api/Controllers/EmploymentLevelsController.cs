using api.DTO.EmploymentLevels;
using api.Errors;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
	[Route("api/employment_levels")]
	[ApiController]
	public class EmploymentLevelsController : ControllerBase
	{
		
		private readonly EmploymentLevelsRepository _employmentLevelsRepository;

		public EmploymentLevelsController(EmploymentLevelsRepository employmentLevelsRepository)
		{
			_employmentLevelsRepository = employmentLevelsRepository;
		}

		// GET: api/<EmploymentLevelsController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<EmploymentLevel>>> Get([FromQuery] FilterEmploymentLevelDTO? filter)
		{
			IEnumerable<EmploymentLevel> levels;

			if (filter != null)
			{
				levels = await _employmentLevelsRepository.GetManyAsync(filter);
			}else
			{
				levels = await _employmentLevelsRepository.GetAllAsync();
			}


			var results = levels.Select(l => new EmploymentLevelDTO
			{
				Id = l.Id,
				Name = l.Name,
			});

			return Ok(results);
		}

		// GET api/<EmploymentLevelsController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			try
			{
				var level = await _employmentLevelsRepository.GetByIdAsync(id);
				var result = new EmploymentLevelDTO
				{
					Id = id,
					Name = level.Name,
				};

				return Ok(result);
			}
			catch (InvalidOperationException)
			{
				return NotFound(EmploymentLevelErrors.NotFound);
			}
		}

		// POST api/<EmploymentLevelsController>
		[HttpPost]
		public async Task<ActionResult<int>> Post([FromBody] CreateEmploymentLevelDTO data)
		{
			var levelId = await _employmentLevelsRepository.CreateAsync(new EmploymentLevel
			{
				Name = data.Name,
			});

			return Ok(levelId);
		}

		// PUT api/<EmploymentLevelsController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] EditEmploymentLevelDTO data)
		{
			try
			{
				await _employmentLevelsRepository.UpdateByIdAsync(id, new()
				{
					Name = data.Name,
				});

				return Ok();
			}
			catch (InvalidOperationException)
			{
				return NotFound(EmploymentLevelErrors.NotFound);
			}

		}

		// DELETE api/<EmploymentLevelsController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				await _employmentLevelsRepository.DeleteByIdAsync(id);

				return NoContent();
			}
			catch (InvalidOperationException)
			{
				return NotFound(EmploymentLevelErrors.NotFound);
			}
		}
	}
}
