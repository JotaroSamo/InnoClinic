namespace Profile_API.Domain.Abstract.IService
{
    public interface IEmailService
    {
        Task SendVerificationEmail(string email, string verificationLink);
    }
}