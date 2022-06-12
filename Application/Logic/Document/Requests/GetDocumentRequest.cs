using Application.Logic.Document.Responses;
using MediatR;

namespace Application.Logic.Document.Requests
{
    public class GetDocumentRequest : IRequest<HandlerResult<AddDocumentResponse>>
    {
        public string FileFullName { get; set; }

    }
}