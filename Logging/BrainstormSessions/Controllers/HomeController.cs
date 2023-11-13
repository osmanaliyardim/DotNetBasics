using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace BrainstormSessions.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBrainstormSessionRepository sessionRepository, ILogger<HomeController> logger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;

            //_logger = Log.Logger = new LoggerConfiguration()
            //    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            //    .WriteTo.Email(new EmailConnectionInfo
            //    {
            //        FromEmail = "from@example.com",
            //        ToEmail = "to@example.com",
            //        MailServer = "smtp.example.com",
            //        NetworkCredentials = new NetworkCredential
            //        {
            //            UserName = "username",
            //            Password = "password"
            //        },
            //        EnableSsl = true,
            //        Port = 587,
            //        EmailSubject = "Log event"
            //    }, restrictedToMinimumLevel: LogEventLevel.Debug, batchPostingLimit: 1)
            //    .CreateLogger();
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Session creation has been started!");

            var sessionList = await _sessionRepository.ListAsync();

            var model = sessionList.Select(session => new StormSessionViewModel()
            {
                Id = session.Id,
                DateCreated = session.DateCreated,
                Name = session.Name,
                IdeaCount = session.Ideas.Count
            });

            _logger.LogInformation("Session creation has been completed!");
            _logger.LogInformation($"{model.Select(x => x.Name)} successfully logged in");

            return View(model);
        }

        public class NewSessionModel
        {
            [Required]
            public string SessionName { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewSessionModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.ToString());

                return BadRequest(ModelState);
            }
            else
            {
                await _sessionRepository.AddAsync(new BrainstormSession()
                {
                    DateCreated = DateTimeOffset.Now,
                    Name = model.SessionName
                });
            }

            return RedirectToAction(actionName: nameof(Index));
        }
    }
}
