﻿using EntityLayer.DTOs.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.API.User.Abstract;
using System.Security.Claims;

namespace JWT_TokenBasedAuthentication.Controllers
{
	[Route("api/TransactionServices")]
	[Authorize]
	[ApiController]
	public class TransactionController(ITransactionService service) : ControllerBase
	{
		[HttpPost("CreateTransaction")]
		public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDTO model)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is null || model is null) return BadRequest("Invalid Request!");

			var result = await service.CreateTransactionAsync(userId, model);
			if (result.Flag == false) return BadRequest(result.Message);

			return Ok(result);
		}
	}
}
