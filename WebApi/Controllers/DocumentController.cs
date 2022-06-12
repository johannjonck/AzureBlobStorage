using Application.Dtos;
using Application.Logic.Document.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebApi;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public DocumentController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> ListDocument()
        {
            try
            {
                var result = await _mediator.Send(new ListDocumentRequest());

                return new JsonResult(result.Entity.DocumentList)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception e)
            {
                return BadRequest("Exception when calling Document: " + e.Message);
            }
        }

        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadDocument(AddDocumentRequest request)
        {
            try
            {
                var documentHelper = new DocumentHelper(_configuration);

                if (await documentHelper.UploadBlob(request.FileFullName))
                {
                    var result = await _mediator.Send(new AddDocumentRequest()
                    {
                        FileName = request.FileName,
                        FileFullName = request.FileFullName,
                        FileSize = request.FileSize,
                        DateModified = DateTime.Now
                    });

                    return new JsonResult(result.Entity)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                else
                {
                    return BadRequest("Colud not upload blob document");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Exception when calling Document: " + e.Message);
            }
        }

        [HttpPost]
        [Route("download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DownloadDocument(GetDocumentRequest request)
        {
            try
            {
                var documentHelper = new DocumentHelper(_configuration);
                var originalBlobFile = await documentHelper.DownloadBlobFromStorageAccount(request.FileFullName);
                var content = new StreamContent(originalBlobFile);

                content.Headers.ContentType = new MediaTypeHeaderValue("text/plain"); //Get from .json
                content.Headers.ContentLength = originalBlobFile.GetBuffer().Length;

                string content2 = new StreamReader(originalBlobFile).ReadToEnd();

                return Ok(content2);
            }
            catch (Exception e)
            {
                return BadRequest("Exception when calling Document: " + e.Message);
            }
        }
    }
}
