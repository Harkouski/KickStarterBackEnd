using BLL.Interfaces;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class HangFireService : IHangFireService
    {
        public EmailService _emailService;

        public HangFireService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void SendEmailIfCreateProject(string email)
        {
            BackgroundJob.Schedule(
               () => _emailService.SendEmailAsync(email, "You successfuly create project", "You successfuly create project"), TimeSpan.FromSeconds(30));
        }

        public void SendBeforeEndAWeekProject(string email, string projectName, DateTime dateTime)
        {
            BackgroundJob.Schedule(
               () => _emailService.SendEmailAsync(email, "KickStarter", $"Your project {projectName} is ending in less than 1 week "), dateTime.AddDays(-7));
        }

        public void SendBeforeEndAHourProject(string email, string projectName, DateTime dateTime)
        {
            BackgroundJob.Schedule(
               () => _emailService.SendEmailAsync(email, "KickStarter", $"Your project {projectName} is ending in less than 1 day "), dateTime.AddDays(-1));
        }
        public void SendBeforeEndADayProject(string email, string projectName, DateTime dateTime)
        {
            BackgroundJob.Schedule(
               () => _emailService.SendEmailAsync(email, "KickStarter", $"Your project {projectName} is ending in less than 1 hour "), dateTime.AddHours(-1));
        }
    }
}
