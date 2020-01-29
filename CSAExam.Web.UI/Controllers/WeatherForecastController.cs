using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GSAExam.Core.Common.Queries;
using GSAExam.Core.DomainModel.Student;
using GSAExam.Infrastructure.EWS;
using GSAExam.Infrastructure.Interface;
using GSAExam.Infrastructure.Model;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace CSAExam.Web.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IGSADbContext _context;
        private readonly IEmailService _emailService;
        [Obsolete]
        private readonly IHostingEnvironment _env;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private IMediator _mediator;

        [Obsolete]
        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IGSADbContext context,
            IEmailService emailService,
            IHostingEnvironment env,
            IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _emailService = emailService;
            _env = env;
            _mediator = mediator;
        }

        [HttpGet]
        [Obsolete]
        [ProducesResponseType(typeof(EntityResponseListModel<StudentReadModel>), 200)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            //_emailService.Connect("Test.Employee1@gsa-gp.com", "D6y_k_%=ZTh:!qfs");
            //var webRoot = _env.ContentRootPath + @"\Attachment";           
            //_emailService.DownloadAttachment(webRoot);     
            EntityResponseListModel<StudentReadModel> returnResponse = new EntityResponseListModel<StudentReadModel>();

            try
            {
                var query = new EntityListQuery<EntityResponseListModel<StudentReadModel>>( null);
                var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
                if (result.ReturnStatus == false)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }           
        }
    }
}
