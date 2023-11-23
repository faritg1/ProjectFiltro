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
    public class ProductoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var con = await _unitOfWork.Productos.GetAllAsync();

        return _mapper.Map<List<ProductoDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Get(string id){
        var con = await _unitOfWork.Productos.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<ProductoDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Post(ProductoDto ProductoDto){
        var con = _mapper.Map<Producto>(ProductoDto);
        _unitOfWork.Productos.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        ProductoDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = ProductoDto.Id}, ProductoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Put(string id, [FromBody]ProductoDto ProductoDto){
        if(ProductoDto.Id == null){
            ProductoDto.Id = id;
        }

        if(ProductoDto.Id != id){
            return BadRequest();
        }

        if(ProductoDto == null){
            return NotFound();
        }
        var con = _mapper.Map<Producto>(ProductoDto);
        _unitOfWork.Productos.Update(con);
        await _unitOfWork.SaveAsync();
        return ProductoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id){
        var con = await _unitOfWork.Productos.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.Productos.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}