using Application.Dtos;
using Application.Logic.Document.Responses;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Assemblers
{
    public class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            this.CreateMap<Document, DocumentDto>();
            this.CreateMap<DocumentDto, Document>();
        }
    }
}
