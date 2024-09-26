using Microsoft.AspNetCore.Mvc;
using Profile_API.Application.Service;
using Profile_API.Contract.Request.Create;
using Profile_API.Contract.Request.Update;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Domain.Models;

namespace Profile_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Create")]
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
            var createAccount = await _accountService.CreateAccountAsync(account);
            if (createAccount.IsFailure)
            {
                return BadRequest(createAccount.Error);
            }

            return Ok(createAccount.Value);

        }
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            return Ok(account);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts.Value);
        }
        [HttpGet("verify-email")]
        public async Task<ActionResult> VerifyEmail(Guid userId)
        {
            var account = await _accountService.GetAccountByIdAsync(userId);
            if (account.IsFailure)
            {
                return NotFound(account.Error);
            }

            if (account.Value.IsEmailVerified)
            {
                return BadRequest("Email уже подтвержден.");
            }

           var verivicateAccount =  await _accountService.VerificateEmail(userId, account.Value.Email);
            if (verivicateAccount.IsFailure)
            {
                return BadRequest(verivicateAccount.Error);
            }

            return Ok();
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateAccountRequest updateAccountRequest)
        {
            if (updateAccountRequest == null)
            {
                return BadRequest();
            }

            var account = new Account()
            {
                Id = updateAccountRequest.Id,
                UserId = updateAccountRequest.UserId,
                Email = updateAccountRequest.Email,
                Password = updateAccountRequest.Password,
                PhoneNumber = updateAccountRequest.PhoneNumber,

            };
            if (!TryValidateModel(account))
            {
                return BadRequest();
            }
          var  updateAccount = await _accountService.UpdateAccountAsync(account.Id,account);
            if (updateAccount.IsFailure)
            {
                return BadRequest(updateAccount.Error);
            }
            return Ok(updateAccount.Value);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _accountService.DeleteAccountAsync(id);
            return Ok();
        }

    }
}
