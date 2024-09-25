namespace Profile_API.Consumer
{
    using Global;
    using MassTransit;
    using Profile_API.Application.Service;
    using Profile_API.Domain.Models;

    public class AccountCreatedConsumer : IConsumer<AccountCreated>
    {
        private readonly IAccountService _accountService;

        public AccountCreatedConsumer(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Consume(ConsumeContext<AccountCreated> context)
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
