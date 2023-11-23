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
    public class GamaProductoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GamaProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GamaProductoDto>>> Get()
    {
        var con = await _unitOfWork.GamasProductos.GetAllAsync();

        return _mapper.Map<List<GamaProductoDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GamaProductoDto>> Get(string id){
        var con = await _unitOfWork.GamasProductos.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<GamaProductoDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GamaProductoDto>> Post(GamaProductoDto GamaProductoDto){
        var con = _mapper.Map<GamaProducto>(GamaProductoDto);
        _unitOfWork.GamasProductos.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        GamaProductoDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = GamaProductoDto.Id}, GamaProductoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GamaProductoDto>> Put(string id, [FromBody]GamaProductoDto GamaProductoDto){
        if(GamaProductoDto.Id == null){
            GamaProductoDto.Id = id;
        }

        if(GamaProductoDto.Id != id){
            return BadRequest();
        }

        if(GamaProductoDto == null){
            return NotFound();
        }
        var con = _mapper.Map<GamaProducto>(GamaProductoDto);
        _unitOfWork.GamasProductos.Update(con);
        await _unitOfWork.SaveAsync();
        return GamaProductoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id){
        var con = await _unitOfWork.GamasProductos.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.GamasProductos.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}