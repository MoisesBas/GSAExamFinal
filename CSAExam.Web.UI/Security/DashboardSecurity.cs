using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAExam.Web.UI.Security
{
    
    public class DashboardAuthorization : IDashboardAuthorizationFilter
    {       
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
