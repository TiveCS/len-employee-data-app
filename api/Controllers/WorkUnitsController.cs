using api.DTO.WorkUnits;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
	[Route("api/units")]
	[ApiController]
	public class WorkUnitsController : ControllerBase
	{

		private readonly WorkUnitsRepository _workUnitsRepository;

		public WorkUnitsController(WorkUnitsRepository workUnitsRepository)
		{
			_workUnitsRepository = workUnitsRepository;
		}

		// GET: api/<WorkUnitsController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<WorkUnitDTO>>> Get()
		{
			var units = await _workUnitsRepository.GetAllAsync();

			var results = units.Select(u => new WorkUnitDTO 
			{
				Id = u.Id,
				Name = u.Name,
			});

			return Ok(results);
		}

		// GET api/<WorkUnitsController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<WorkUnitDTO>> Get(int id)
		{
			try
			{
				var unit = await _workUnitsRepository.GetByIdAsync(id);
				var result = new WorkUnitDTO { Id = id, Name = unit.Name };

				return Ok(result);
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}

		// POST api/<WorkUnitsController>
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateWorkUnitDTO data)
		{
			var entity = new WorkUnit { Name = data.Name };
			
			await _workUnitsRepository.CreateAsync(entity);

			return Ok(entity.Id);
		}

		// PUT api/<WorkUnitsController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] EditWorkUnitDTO data)
		{
			try
			{
				await _workUnitsRepository.UpdateByIdAsync(id, new() 
				{
					Name = data.Name,	
				});

				return Ok();
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}

		// DELETE api/<WorkUnitsController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				await _workUnitsRepository.DeleteByIdAsync(id);

				return NoContent();
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
