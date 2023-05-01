namespace WebApiPractice.Services
{
    public class CloudMailService : IMailService
    {
        // this is an example of a service that can send an email on some errors/logs
        // this is just an example, it is not actually sending an email, it is just logging to the console
        // Once writeen it will need to be added to the container in Program.cs


        // these have been replaced with the constructor below and initialised as empty strings
        // private string _mailTo = "admin@mycompany.com";
        // private string _mailFrom = "noreply@mycompany.com";

        private readonly string _mailTo = String.Empty;
        private readonly string _mailFrom = String.Empty;

        // new constructor to hit values in the appsettings or program file
        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            // send mail - output to console window
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with CloudMailService");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }


    }
}
