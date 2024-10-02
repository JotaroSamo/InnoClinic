namespace Profile_API.Consumer
{
    using Global.Dto;
    using MassTransit;
    using Profile_API.Application.Service;
    using Profile_API.Domain.Models;

    public class AccountCreatedConsumer : IConsumer<AccountDto>
    {
        private readonly IAccountService _accountService;

        public AccountCreatedConsumer(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Consume(ConsumeContext<AccountDto> context)
        {
            var message = context.Message;

            var account = new Account()
            {
                UserId = message.UserId,
                Email = message.Email,
                Password = message.Password,
                IsEmailVerified = false,
                CreatedBy = "Owner",
            };

            // Создание аккаунта
            await _accountService.CreateAccountAsync(account);
        }
    }

}
