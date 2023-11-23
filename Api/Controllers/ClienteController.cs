using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
public class ClienteController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var con = await _unitOfWork.Clientes.GetAllAsync();

        return _mapper.Map<List<ClienteDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Get(int id){
        var con = await _unitOfWork.Clientes.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<ClienteDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Post(ClienteDto ClienteDto){
        var con = _mapper.Map<Cliente>(ClienteDto);
        _unitOfWork.Clientes.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        ClienteDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = ClienteDto.Id}, ClienteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody]ClienteDto ClienteDto){
        if(ClienteDto.Id == 0){
            ClienteDto.Id = id;
        }

        if(ClienteDto.Id != id){
            return BadRequest();
        }

        if(ClienteDto == null){
            return NotFound();
        }
        var con = _mapper.Map<Cliente>(ClienteDto);
        _unitOfWork.Clientes.Update(con);
        await _unitOfWork.SaveAsync();
        return ClienteDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var con = await _unitOfWork.Clientes.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.Clientes.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}