using Application.Dtos;

namespace Application.Logic.Document.Responses
{
    public class ListDocumentResponse //TODO: Use a generic response -> combine Add, Get and List ???
    {
        public List<DocumentDto> DocumentList { get; set; }
    }
}
