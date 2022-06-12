using Application.Dtos;

namespace Application.Logic.Document.Responses
{
    public class GetDocumentResponse //TODO: Use a generic response -> combine Add, Get and List ???
    {
        public DocumentDto Document { get; set; }

    }
}
