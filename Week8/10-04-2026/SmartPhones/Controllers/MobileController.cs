using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartPhones.models.entities;
using SmartPhones.models.dto;
using System.Globalization;

namespace SmartPhones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MobileController : ControllerBase
    {
        private static readonly List<Company> _items = new();

        [HttpGet]
        public ActionResult<IEnumerable<Company>> GetAll()
        {
            return Ok(_items);
        }

        [HttpGet("{id}")]
        public ActionResult<Company> GetById(Guid id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Company> Create([FromBody] CustomerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var company = new Company
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            _items.Add(company);

            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }
    }
}
