﻿using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.DeviceManager.DTO.Device.Statistics;
using Netmon.Models.Component.Interface;

namespace Netmon.DeviceManager.Controllers.Device.Statistics;

[ApiController]
[Route("device/{id}/statistics/interface")]
public class DeviceInterfaceStatisticsController : BaseController
{
    private readonly IInterfaceReadRepository _interfaceReadRepository;
    
    public DeviceInterfaceStatisticsController(IInterfaceReadRepository interfaceReadRepository)
    {
        _interfaceReadRepository = interfaceReadRepository;
    }   
    
    [HttpGet("inout")]
    public async Task<IActionResult> GetInterfaceStatisticsAsync(Guid id, DateTime fromDate, DateTime toDate)
    {
        List<IInterface> interfaces = (await _interfaceReadRepository.GetByDeviceIdWithMetrics(id, fromDate, toDate)).Select(dbo => dbo.ToInterface()).ToList();
        DeviceInterfacesStatisticsDTO dto = DeviceInterfacesStatisticsDTO.FromInterfaces(interfaces);
        return Ok(dto);
    }
}