using Application.Logic.Document.Responses;
using MediatR;

namespace Application.Logic.Document.Requests
{
    public class AddDocumentRequest : IRequest<HandlerResult<AddDocumentResponse>>
    {
        public string FileName { get; set; }

        public string FileFullName { get; set; }

        public long FileSize { get; set; }

        public DateTime DateModified { get; set; }

    }
}