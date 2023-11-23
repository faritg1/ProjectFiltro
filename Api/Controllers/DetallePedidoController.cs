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
    public class DetallePedidoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DetallePedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
    {
        var con = await _unitOfWork.DetallesPedidos.GetAllAsync();

        return _mapper.Map<List<DetallePedidoDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetallePedidoDto>> Get(int IdPedido, string IdProducto){
        var con = await _unitOfWork.DetallesPedidos.GetByIdAsync(IdPedido,IdProducto);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<DetallePedidoDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> Post(DetallePedidoDto DetallePedidoDto){
        var con = _mapper.Map<DetallePedido>(DetallePedidoDto);
        _unitOfWork.DetallesPedidos.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        DetallePedidoDto.IdProducto = con.IdProducto;
        DetallePedidoDto.IdPedido = con.IdPedido;
        return CreatedAtAction(nameof(Post), new {IdProducto = DetallePedidoDto.IdProducto, IdPedido = DetallePedidoDto.IdPedido}, DetallePedidoDto);
    }
    }
}