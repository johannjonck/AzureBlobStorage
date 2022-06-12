using Application.Dtos;
using Application.Logic.Document.Requests;
using Application.Logic.Document.Responses;
using AutoMapper;
using Persistance;

namespace Application.Logic.Document.Handlers
{
    public class AddDocumentHandler : HandlerBase<AddDocumentRequest, HandlerResult<AddDocumentResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddDocumentHandler(IDbContext context, IMapper mapper)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override HandlerResult<AddDocumentResponse> HandleRequest(AddDocumentRequest request, CancellationToken cancellationToken, HandlerResult<AddDocumentResponse> result)
        {
            var document = new Domain.Entities.Document
            {
                FileName = request.FileName,
                FileFullName = request.FileFullName,
                FileSize = request.FileSize,
                DateModified = DateTime.Now
            };

            _dbContext.Document.Add(document);
            _dbContext.SaveChanges();

            result.Entity = new AddDocumentResponse
            {
                Document = _mapper.Map<DocumentDto>(document)
            };

            return result;
        }

    }
}
