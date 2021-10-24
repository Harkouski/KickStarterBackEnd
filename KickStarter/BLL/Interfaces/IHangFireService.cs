using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHangFireService
    {
        void SendEmailIfCreateProject(string email);

        void SendBeforeEndAWeekProject(string email, string projectName, DateTime dateTime);

        void SendBeforeEndAHourProject(string email, string projectName, DateTime dateTime);

        void SendBeforeEndADayProject(string email, string projectName, DateTime dateTime);
    }
}
