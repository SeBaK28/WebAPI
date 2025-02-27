using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.UserData;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace api.Controller
{
    [Route("api/userdata")]
    [ApiController]
    public class UserDataController : ControllerBase
    {

        private readonly IUserDataRepository _userDataRepo;

        public UserDataController(IUserDataRepository userDataRepo)
        {
            _userDataRepo = userDataRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = await _userDataRepo.GetAllAsync(query);

            var userDto = users.Select(s => s.ToStockDto());

            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userDataRepo.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToStockDto());
        }


        // [HttpGet("{Name}")]
        // public async Task<IActionResult> GetByName([FromRoute] string Name)
        // {
        //     var userName = await _userDataRepo.GetByNameAsync(Name);

        //     if (userName == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(userName.ToStockDto());
        // }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDataRequestDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDataModel = userDto.ToStockFromCreateDto();

            await _userDataRepo.CreateAsync(userDataModel);
            return CreatedAtAction(nameof(GetById), new { id = userDataModel.Id }, userDataModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDataRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDataModel = await _userDataRepo.UpdateAsync(id, updateDto);

            if (userDataModel == null)
            {
                return NotFound();
            }

            return Ok(userDataModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDataModel = await _userDataRepo.DeleteAsync(id);

            if (userDataModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}