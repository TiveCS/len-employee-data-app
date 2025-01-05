using api.DTO.EmploymentLevels;
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
		public async Task<ActionResult<IEnumerable<EmploymentLevel>>> Get()
		{
			var levels = await _employmentLevelsRepository.GetAllAsync();
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
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}

		// POST api/<EmploymentLevelsController>
		[HttpPost]
		public async Task<int> Post([FromBody] CreateEmploymentLevelDTO data)
		{
			return await _employmentLevelsRepository.CreateAsync(new EmploymentLevel
			{
				Name = data.Name,
			});
		}

		// PUT api/<EmploymentLevelsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
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
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
