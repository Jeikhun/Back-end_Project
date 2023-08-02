using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Back_end_Project.Services.Interfaces
{
    public interface IMailService
    {
		void SendEmail(Message message);

	}
}
