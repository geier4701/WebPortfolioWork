using REALHUMANTEXTINGSERVICE.DATA;
using REALHUMANTEXTINGSERVICE.MODELS;
using REALHUMANTEXTINGSERVICE.MODELS.Interfaces;
using REALHUMANTEXTINGSERVICE.MODELS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.BLL
{
	public class MessagingManager
	{
		private IReservationRepo _reservationRepo;
		private IMessageRepo _messageRepo;

		public MessagingManager(IReservationRepo resRepo, IMessageRepo mesRepo)
		{
			_reservationRepo = resRepo;
			_messageRepo = mesRepo;
		}

		public AllCompaniesResponse GetAllCompanies()
		{
			var toReturn = new AllCompaniesResponse();

			try
			{
				toReturn.AllCompanies = _reservationRepo.GetAllCompanies().ToList();
				toReturn.Success = true;
			}
			catch
			{
				toReturn.Success = false;
				toReturn.Message = "An error occurred retrieving company list";
			}

			return toReturn;
		}

		public AllGuestsResponse GetAllGuests()
		{
			var toReturn = new AllGuestsResponse();

			try
			{
				toReturn.AllGuests = _reservationRepo.GetAllGuests().ToList();
				toReturn.Success = true;
			}
			catch
			{
				toReturn.Success = false;
				toReturn.Message = "An error occurred retrieving guest list";
			}

			return toReturn;
		}

		public AllMessagesResponse GetAllMessages()
		{
			var toReturn = new AllMessagesResponse();

			try
			{
				toReturn.AllMessages = _messageRepo.GetAllMessages().ToList();
				toReturn.Success = true;
			}
			catch
			{
				toReturn.Success = false;
				toReturn.Message = "An error occurred retrieving message list";
			}

			return toReturn;
		}

		public SendMessageResponse SendMessage(string enteredCompanyId, string enteredGuestId, string enteredMessageId)
		{
			var response = new SendMessageResponse();
			int companyId = -1;
			int guestId = -1;
			int messageId = -1;
			try
			{
				companyId = int.Parse(enteredCompanyId);
				guestId = int.Parse(enteredGuestId);
				messageId = int.Parse(enteredMessageId);
			}
			catch
			{
				response.Success = false;
				response.Message = "You must enter a number!";
				return response;
			}

			var company = _reservationRepo.GetCompany(companyId);
			var guest = _reservationRepo.GetGuest(guestId);
			var message = _messageRepo.GetMessage(messageId);

			if (company == null || guest == null || message == null)
			{
				response.Success = false;
				response.Message = "An ID you entered was incorrect.";
				return response;
			}

			response.FullTextMessage = TextParser.ToOutput(message, company, guest);
			response.Success = true;

			return response;
		}

		public Response AddMessage(RawMessage toSave)
		{
			var response = new Response();

			try
			{
				_messageRepo.AddMessage(toSave);
				response.Success = true;
			}
			catch
			{
				response.Success = false;
				response.Message = "Message failed to save";
			}

			return response;
		}
	}

	public static class TextParser
	{
		public static string ToOutput(RawMessage message, Company company, Guest guest)
		{
			if (message.text.Contains("{timeGreeting}"))
			{
				var currentTime = new DateTime();
				currentTime = DateTime.Now;
				var greeting = new Greeting();

				if (currentTime.Hour >= 6 && currentTime.Hour < 11)
				{
					greeting.Salutation = "GOOD DAY TO YOU, PARTNER";
				}
				else if (currentTime.Hour >= 11 && currentTime.Hour < 17)
				{
					greeting.Salutation = "GOOD DAY TO YOU, PARTNER";
				}
				else if (currentTime.Hour >= 17 && currentTime.Hour < 11)
				{
					greeting.Salutation = "GOOD EVENING, COMRADE";
				}
				else
				{
					greeting.Salutation = "YOU SHOULD BE SLEEPING";
				}

				message.text = message.text.Replace("{timeGreeting}", greeting.Salutation);
			}

			if (message.text.Contains("{guestName}"))
			{
				message.text = message.text.Replace("{guestName}", ($"{guest.firstName} {guest.lastName}"));
			}

			if (message.text.Contains("{company}"))
			{
				message.text = message.text.Replace("{company}", company.company);
			}

			if (message.text.Contains("{roomNumber}"))
			{
				message.text = message.text.Replace("{roomNumber}", guest.reservation.roomNumber.ToString());
			}

			if (message.text.Contains("{startTimeStamp}"))
			{
				message.text = message.text.Replace("{startTimeStamp}", DateConverter.ToDateTime(guest.reservation.startTimeStamp).ToString());
			}

			if (message.text.Contains("{endTimeStamp}"))
			{
				message.text = message.text.Replace("{endTimeStamp}", DateConverter.ToDateTime(guest.reservation.endTimeStamp).ToString());
			}

			if (message.text.Contains("{company}"))
			{
				message.text = message.text.Replace("{company}", company.company);
			}

			if (message.text.Contains("{city}"))
			{
				message.text = message.text.Replace("{city}", company.city);
			}

			if (message.text.Contains("{timezone}"))
			{
				message.text = message.text.Replace("{timezone}", company.timezone);
			}

			return message.text;
		}
	}

	public static class DateConverter
	{
		public static DateTime ToDateTime(int timeStamp)
		{
			var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

			return origin.AddSeconds(timeStamp);
		}
	}
}
