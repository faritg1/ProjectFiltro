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
    public class PagoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PagoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
    {
        var con = await _unitOfWork.Pagos.GetAllAsync();

        return _mapper.Map<List<PagoDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagoDto>> Get(int id){
        var con = await _unitOfWork.Pagos.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<PagoDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> Post(PagoDto PagoDto){
        var con = _mapper.Map<Pago>(PagoDto);
        _unitOfWork.Pagos.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        PagoDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = PagoDto.Id}, PagoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagoDto>> Put(int id, [FromBody]PagoDto PagoDto){
        if(PagoDto.Id == 0){
            PagoDto.Id = id;
        }

        if(PagoDto.Id != id){
            return BadRequest();
        }

        if(PagoDto == null){
            return NotFound();
        }
        var con = _mapper.Map<Pago>(PagoDto);
        _unitOfWork.Pagos.Update(con);
        await _unitOfWork.SaveAsync();
        return PagoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var con = await _unitOfWork.Pagos.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.Pagos.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}

