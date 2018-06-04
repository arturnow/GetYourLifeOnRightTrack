using System;
using System.Linq;
using System.Web.Http;
using WasterDAL.Model;
using WasterLOB;
using WasterWebAPI.Filters;
using WasterWebAPI.Handlers;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{
    [HeaderAuthorizationFilter]
    public class MessageApiController : ApiController
    {
        private readonly IMessageService _service;

        public MessageApiController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public string GetMessageTemplate()
        {
            return "<div id='message_title'><div class='on-the-right-track-message-close'></div><h3>{{title}}</h3>" +
                   "<div id='message_content'>" + 
                   "<a href='{{url}}' target='_blank'> "+
                   "<img src='{{imageUrl}}' alt='Image to message' width='60' height='60' /></a>{{content}}</div><div style='clear:both;' />";//.Replace("\\\"", );
        }

        [HttpPost]
        public MessageViewModel[] GetMessage()
        {
            var result = _service.GetAll().Select(MapMessageToMessageViewModel).ToArray();

#if DEBUG
            if (result.Length < 1)
            {
                result = new MessageViewModel[]
                {
                    new MessageViewModel
                    {
                        Title = "Test Title 1",
                        RequireConfirmation = true,
                        Content = "Test content 1",
                        ImageUrl = "http://bi.gazeta.pl/im/3/7499/z7499823Q.jpg",
                        Url = "http://bi.gazeta.pl/im/3/7499/z7499823Q.jpg",
                        Category = "Ad",
                        DueDateTime =  DateTime.Now.AddMinutes(2)
                    }, new MessageViewModel
                    {                        
                        Title = "Test Title 2",
                        RequireConfirmation = false,
                        Content = "Test content 2",
                        ImageUrl = "http://bi.gazeta.pl/im/3/7499/z7499823Q.jpg",
                        Url = "http://bi.gazeta.pl/im/3/7499/z7499823Q.jpg",
                        Category = "Event",
                        DueDateTime =  DateTime.Now.AddMinutes(2)
                        
                    }, new MessageViewModel
                    {
                        Title = "Test Title 3",
                        RequireConfirmation = true,
                        Content = "Test content 3",
                        ImageUrl = "http://bi.gazeta.pl/im/3/7499/z7499823Q.jpg",
                        Url = "http://bi.gazeta.pl/im/3/7499/z7499823Q.jpg",
                        Category = "Track",
                        DueDateTime =  DateTime.Now.AddMinutes(2)
                    }, 
                };
            }
#endif
            return result;
        }

        public Guid CreateMessage(MessageViewModel model)
        {
            if(ModelState.IsValid)
            {
                    return _service.Create(MapMessageViewModelToMessage(model));
            }
            //TODO: zrobić obslugę wyjątków
            throw new ApplicationException("Model invalid");
        }

        public bool ConfirmMessageDelivery(Guid id)
        {
            return true;
        }

        private MessageViewModel MapMessageToMessageViewModel(Message message)
        {
            return new MessageViewModel
            {
                Category = message.Type,
                Content = message.Content,
                DueDateTime = message.DueDate,
                RequireConfirmation = message.RequireConfirmation,
                Title = message.Title,
                Url = message.Url
            };
        }

        private Message MapMessageViewModelToMessage(MessageViewModel model)
        {
            return new Message
            {
                Type = model.Category,
                Content = model.Content,
                DueDate = model.DueDateTime,
                RequireConfirmation = model.RequireConfirmation,
                Title = model.Title,
                Url = model.Url
            };
        }
    }
}
