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
    public class PedidoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
    {
        var con = await _unitOfWork.Pedidos.GetAllAsync();

        return _mapper.Map<List<PedidoDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoDto>> Get(int id){
        var con = await _unitOfWork.Pedidos.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<PedidoDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Post(PedidoDto PedidoDto){
        var con = _mapper.Map<Pedido>(PedidoDto);
        _unitOfWork.Pedidos.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        PedidoDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = PedidoDto.Id}, PedidoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody]PedidoDto PedidoDto){
        if(PedidoDto.Id == 0){
            PedidoDto.Id = id;
        }

        if(PedidoDto.Id != id){
            return BadRequest();
        }

        if(PedidoDto == null){
            return NotFound();
        }
        var con = _mapper.Map<Pedido>(PedidoDto);
        _unitOfWork.Pedidos.Update(con);
        await _unitOfWork.SaveAsync();
        return PedidoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var con = await _unitOfWork.Pedidos.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.Pedidos.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
