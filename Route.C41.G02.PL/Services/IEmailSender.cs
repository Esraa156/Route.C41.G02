using System.Globalization;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading.Tasks;
namespace Route.C41.G02.PL.Services
{
	public interface IEmailSender
	{

			Task SendAsync(StringInfo from, StringInfo recipients, string subject, string body);
		}
	}

