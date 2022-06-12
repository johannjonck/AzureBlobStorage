using Application.Dtos;
using Application.Logic.Document.Requests;
using Application.Logic.Document.Responses;
using AutoMapper;
using Persistance;

namespace Application.Logic.Document.Handlers
{
    public class ListDocumentHandler : HandlerBase<ListDocumentRequest, HandlerResult<ListDocumentResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public ListDocumentHandler(IDbContext context, IMapper mapper)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override HandlerResult<ListDocumentResponse> HandleRequest(ListDocumentRequest request, CancellationToken cancellationToken, HandlerResult<ListDocumentResponse> result)
        {
            var document = _dbContext.Document.ToList();

            result.Entity = new ListDocumentResponse
            {
                DocumentList = _mapper.Map<List<DocumentDto>>(document)
            };

            return result;
        }
    }
}
