﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Api;
using BrainstormSessions.Controllers;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Events;
using Serilog.Sinks.InMemory;
using Serilog;
using Xunit;

namespace BrainstormSessions.Test.UnitTests
{
    public class LoggingTests : IDisposable
    {
        private Serilog.Core.Logger serilog;
        private LoggerFactory loggerFactory;

        public LoggingTests()
        {
            serilog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.InMemory()
                .CreateLogger();
            loggerFactory = (LoggerFactory)new LoggerFactory().AddSerilog(serilog);
        }

        public void Dispose()
        {
            InMemorySink.Instance.Dispose();
        }
        [Fact]
        public async Task HomeController_Index_LogInfoMessages()
        {
            // Arrange
            ILogger<HomeController> logger = loggerFactory.CreateLogger<HomeController>();
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());
            var controller = new HomeController(mockRepo.Object, logger);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.True(InMemorySink.Instance.LogEvents.Any(l => l.Level == LogEventLevel.Information), "Expected Info messages in the logs");
        }

        [Fact]
        public async Task HomeController_IndexPost_LogWarningMessage_WhenModelStateIsInvalid()
        {
            // Arrange
            ILogger<HomeController> logger = loggerFactory.CreateLogger<HomeController>();
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());
            var controller = new HomeController(mockRepo.Object, logger);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new HomeController.NewSessionModel();

            // Act
            var result = await controller.Index(newSession);

            // Assert
            Assert.True(InMemorySink.Instance.LogEvents.Any(l => l.Level == LogEventLevel.Warning), "Expected Warn messages in the logs");
        }

        [Fact]
        public async Task IdeasController_CreateActionResult_LogErrorMessage_WhenModelStateIsInvalid()
        {
            // Arrange & Act
            ILogger<IdeasController> logger = loggerFactory.CreateLogger<IdeasController>();
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            var controller = new IdeasController(mockRepo.Object, logger);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.CreateActionResult(model: null);

            // Assert
            Assert.True(InMemorySink.Instance.LogEvents.Any(l => l.Level == LogEventLevel.Error), "Expected Error messages in the logs");
        }

        [Fact]
        public async Task SessionController_Index_LogDebugMessages()
        {
            // Arrange
            ILogger<SessionController> logger = loggerFactory.CreateLogger<SessionController>();
            int testSessionId = 1;
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(GetTestSessions().FirstOrDefault(
                    s => s.Id == testSessionId));
            var controller = new SessionController(mockRepo.Object, logger);

            // Act
            var result = await controller.Index(testSessionId);

            // Assert
            Assert.True(InMemorySink.Instance.LogEvents.Count(l => l.Level == LogEventLevel.Information) == 2, "Expected 2 Debug messages in the logs");
        }

        private List<BrainstormSession> GetTestSessions()
        {
            var sessions = new List<BrainstormSession>();
            sessions.Add(new BrainstormSession()
            {
                DateCreated = new DateTime(2016, 7, 2),
                Id = 1,
                Name = "Test One"
            });
            sessions.Add(new BrainstormSession()
            {
                DateCreated = new DateTime(2016, 7, 1),
                Id = 2,
                Name = "Test Two"
            });
            return sessions;
        }
    }
}
