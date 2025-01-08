using api.Data;
using api.DTO.Employees;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
	[Route("api/employees")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{

		private readonly EmployeesRepository _employeesRepository;

		public EmployeesController(EmployeesRepository employeesRepository) => _employeesRepository = employeesRepository;

		// GET: api/<EmployeesController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> Get([FromQuery] FilterEmployeeDTO? filter)
		{
			try
			{
				if (filter != null)
				{
					return Ok(await _employeesRepository.GetMany(filter));
				}

				return Ok(await _employeesRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// GET api/<EmployeesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetById(string id)
		{
			try
			{
				var employee = await _employeesRepository.GetByIdAsync(id);
				var result = new EmployeeDetailsDTO();

				result.FromEntity(employee);

				return Ok(result);
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}

		// POST api/<EmployeesController>
		[HttpPost]
		public async Task<ActionResult<string>> Post([FromBody] CreateEmployeeDTO data)
		{
			var employeeId = await _employeesRepository.CreateAsync(new Employee
			{
				NIK = data.NIK,
				Name = data.Name,
				Gender = data.Gender,
				Birthday = data.Birthday,
				BirthCity = data.BirthCity,
				UnitId = data.UnitId,
				EmploymentLevelId = data.EmploymentLevelId,
			});

			return Ok(employeeId);
		}

		// PUT api/<EmployeesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(string id, [FromBody] EditEmployeeDTO data)
		{
			try
			{
				await _employeesRepository.UpdateByIdAsync(id, new Employee
				{
					NIK = data.NIK,
					Name = data.Name,
					BirthCity = data.BirthCity,
					Birthday = data.Birthday,
					EmploymentLevelId = data.EmploymentLevelId,
					UnitId = data.UnitId,
					Gender = data.Gender,
				});

				return Ok();
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}

		// DELETE api/<EmployeesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{
			try
			{
				await _employeesRepository.DeleteByIdAsync(id);
				return NoContent();
			}
			catch (InvalidOperationException e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
