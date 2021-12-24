using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlatformsController : ControllerBase
  {
    private readonly IPlatformRepo _repo;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      Console.WriteLine("--> GetPlatforms");
      var platoonItems = _repo.GetAllPlatforms();
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platoonItems));
    }

    [HttpGet("{id}", Name="GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
      Console.WriteLine("--> GetPlatformById");
      var platform = _repo.GetPlatformById(id);
      if (platform == null) {
        return NotFound();
      }
      return Ok(_mapper.Map<PlatformReadDto>(platform));
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platoformCreateDto)
    {
      Console.WriteLine("--> CreatePlatform");
      var platformModel = _mapper.Map<Platform>(platoformCreateDto);
      _repo.CreatePlatform(platformModel);
      _repo.SaveChanges();

      var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
      return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
  }
}