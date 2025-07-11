﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BrokersController : Controller
{
	IBrokers Brokers { get; }

	public BrokersController(IBrokers brokers)
	{
		Brokers = brokers;
	}

	[HttpGet]
	public IEnumerable<Broker> Get() => Brokers.All();
}
