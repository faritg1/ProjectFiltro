using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class OficinaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OficinaDto>>> Get()
    {
        var con = await _unitOfWork.Oficinas.GetAllAsync();

        return _mapper.Map<List<OficinaDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OficinaDto>> Get(string id){
        var con = await _unitOfWork.Oficinas.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<OficinaDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> Post(OficinaDto OficinaDto){
        var con = _mapper.Map<Oficina>(OficinaDto);
        _unitOfWork.Oficinas.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        OficinaDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = OficinaDto.Id}, OficinaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OficinaDto>> Put(string id, [FromBody]OficinaDto OficinaDto){
        if(OficinaDto.Id == null){
            OficinaDto.Id = id;
        }

        if(OficinaDto.Id != id){
            return BadRequest();
        }

        if(OficinaDto == null){
            return NotFound();
        }
        var con = _mapper.Map<Oficina>(OficinaDto);
        _unitOfWork.Oficinas.Update(con);
        await _unitOfWork.SaveAsync();
        return OficinaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id){
        var con = await _unitOfWork.Oficinas.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.Oficinas.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}
