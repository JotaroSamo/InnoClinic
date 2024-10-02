using FluentValidation;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<Account> _accountValidator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, IValidator<Account> accountValidator, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _accountValidator = accountValidator;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] CreateAccountRequest createAccountRequest)
        {
            if (createAccountRequest == null)
            {
                _logger.LogWarning("CreateAccountRequest is null.");
                return BadRequest("Invalid request.");
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

            var validationResult = await _accountValidator.ValidateAsync(account);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for account creation: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var createAccount = await _accountService.CreateAccountAsync(account);
            if (createAccount.IsFailure)
            {
                _logger.LogError("Failed to create account: {Error}", createAccount.Error);
                return BadRequest(createAccount.Error);
            }

            _logger.LogInformation("Account created successfully: {AccountId}", createAccount.Value.Id);
            return Ok(createAccount.Value);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account.IsFailure)
            {
                _logger.LogWarning("Account with ID {Id} not found: {Error}", id, account.Error);
                return NotFound(account.Error);
            }

            _logger.LogInformation("Retrieved account with ID {Id}", id);
            return Ok(account.Value);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            if (accounts.IsFailure)
            {
                _logger.LogError("Failed to retrieve accounts: {Error}", accounts.Error);
                return BadRequest(accounts.Error);
            }

            _logger.LogInformation("Retrieved all accounts");
            return Ok(accounts.Value);
        }

        [HttpGet("verify-email")]
        public async Task<ActionResult> VerifyEmail(Guid userId)
        {
            var account = await _accountService.GetAccountByIdAsync(userId);
            if (account.IsFailure)
            {
                _logger.LogWarning("Account with user ID {UserId} not found: {Error}", userId, account.Error);
                return NotFound(account.Error);
            }

            if (account.Value.IsEmailVerified)
            {
                _logger.LogWarning("Email for user ID {UserId} is already verified.", userId);
                return BadRequest("Email уже подтвержден.");
            }

            var verifyAccount = await _accountService.VerificateEmail(userId, account.Value.Email);
            if (verifyAccount.IsFailure)
            {
                _logger.LogError("Failed to verify email for user ID {UserId}: {Error}", userId, verifyAccount.Error);
                return BadRequest(verifyAccount.Error);
            }

            _logger.LogInformation("Email verified successfully for user ID {UserId}", userId);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateAccountRequest updateAccountRequest)
        {
            if (updateAccountRequest == null)
            {
                _logger.LogWarning("UpdateAccountRequest is null.");
                return BadRequest("Invalid request.");
            }

            var account = new Account()
            {
                Id = updateAccountRequest.Id,
                UserId = updateAccountRequest.UserId,
                Email = updateAccountRequest.Email,
                Password = updateAccountRequest.Password,
                PhoneNumber = updateAccountRequest.PhoneNumber,
            };

            var validationResult = await _accountValidator.ValidateAsync(account);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for account update: {Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var updateAccount = await _accountService.UpdateAccountAsync(account.Id, account);
            if (updateAccount.IsFailure)
            {
                _logger.LogError("Failed to update account: {Error}", updateAccount.Error);
                return BadRequest(updateAccount.Error);
            }

            _logger.LogInformation("Account updated successfully: {AccountId}", updateAccount.Value.Id);
            return Ok(updateAccount.Value);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to delete account with ID {Id}: {Error}", id, result.Error);
                return NotFound(result.Error);
            }

            _logger.LogInformation("Account with ID {Id} deleted successfully", id);
            return Ok();
        }
    }

}
