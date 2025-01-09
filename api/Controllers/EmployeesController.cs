using api.Data;
using api.DTO;
using api.DTO.Employees;
using api.Errors;
using api.Models;
using api.Repositories;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

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
			GetManyResultDTO<Employee> result = new() { Entities = [], TotalCount = 0 };

			result.TotalCount = await _employeesRepository.CountAysnc();
			if (filter != null)
			{
				result.Entities = await _employeesRepository.GetManyAsync(filter);
				return Ok(result);
			}

			result.Entities = await _employeesRepository.GetAllAsync();
			return Ok(result);
		}

		// GET api/<EmployeesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetById(string id)
		{
			try
			{
				var employee = await _employeesRepository.GetByIdAsync(id);
				var result = new EmployeeDetailsDTO().FromEntity(employee);

				return Ok(result);
			}
			catch (InvalidOperationException)
			{
				return NotFound(EmployeeErrors.NotFound);
			}
		}

		// POST api/<EmployeesController>
		[HttpPost]
		public async Task<ActionResult<string>> Post([FromBody] CreateEmployeeDTO data)
		{
			try
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
			catch (ReferenceConstraintException e)
			{
				var field = e.ConstraintProperties[0];

				if (field == "UnitId") return BadRequest(EmployeeErrors.WorkUnitNotFound);
				if (field == "EmploymentLevelId") return BadRequest(EmployeeErrors.EmploymentLevelNotFound);

				return BadRequest();
			}
			catch(UniqueConstraintException)
			{
				return BadRequest(EmployeeErrors.DuplicateNIK);
			}
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
			catch (InvalidOperationException)
			{
				return NotFound(EmployeeErrors.NotFound);
			}
			catch (ReferenceConstraintException e) {
				var field = e.ConstraintProperties[0];

				if (field == "UnitId") return BadRequest(EmployeeErrors.WorkUnitNotFound);
				if (field == "EmploymentLevelId") return BadRequest(EmployeeErrors.EmploymentLevelNotFound);

				return BadRequest();
			}
			catch (UniqueConstraintException)
			{
				return BadRequest(EmployeeErrors.DuplicateNIK);
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
			catch (InvalidOperationException)
			{
				return NotFound(EmployeeErrors.NotFound);
			}
		}
	}
}
