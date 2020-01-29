using GSAExam.Infrastructure.Utility.Scheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSAExam.Infrastructure.DropBox
{
    
    public class DropBoxScheduleTask : ScheduledProcessor
    {

        private readonly IDropBoxService _dropBoxService;
        
        private readonly DropBoxSettings _settings;
        public DropBoxScheduleTask( IDropBoxService dropBoxService, IOptions<DropBoxSettings> settings, IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _dropBoxService = dropBoxService;
            _settings = settings.Value;
       
        }

        protected override string Schedule => "*/2 * * * *";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            await _dropBoxService.Process(_settings.PathUrl);         
        }
    }
}
