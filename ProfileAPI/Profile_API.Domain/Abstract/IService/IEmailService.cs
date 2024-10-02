namespace Profile_API.Domain.Abstract.IService
{
    public interface IEmailService
    {
        Task SendCredentialsToEmail(string email);
        Task SendConfirmationLink(string email, Guid accountId);
    }
}