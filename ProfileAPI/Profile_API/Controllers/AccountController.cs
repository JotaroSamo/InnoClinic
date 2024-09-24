using Microsoft.AspNetCore.Mvc;
using Profile_API.Contract.Request;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Domain.Models;

namespace Profile_API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<ActionResult> Create([FromBody] CreateAccountRequest createAccountRequest)
        {
            if (createAccountRequest == null)
            {
                return BadRequest();
            }

            var account = new Account()
            {
                UserId = createAccountRequest.UserId,
                Email = createAccountRequest.Email,
                Password = createAccountRequest.Password,
                PhoneNumber = createAccountRequest.PhoneNumber,
                IsEmailVerified = false,
                CreatedBy = "Owner",

            };
            if (!TryValidateModel(account))
            {
                return BadRequest();
            }
            account = await _accountService.CreateAccountAsync(account);
            return Ok(account);

        }
    }
}
