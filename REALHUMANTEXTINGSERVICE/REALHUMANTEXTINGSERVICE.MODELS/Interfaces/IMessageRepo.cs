using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.MODELS.Interfaces
{
	public interface IMessageRepo
	{
		List<RawMessage> GetAllMessages();
		RawMessage GetMessage(int messageId);
		void AddMessage(RawMessage toAdd);
	}
}
