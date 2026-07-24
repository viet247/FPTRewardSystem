using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FPTRewardSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // Duong dan se la: api/v1/wallet
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        [Authorize]
        [HttpGet("ballance")]
        public async Task<IActionResult> GetWalletBalance()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId == null)
            {
                throw new NotFoundException($"Không có User");
            }
            var result = await _walletService.GetWalletByUserIdAsync(currentUserId);
            return Ok(result);
        }
    }
}
