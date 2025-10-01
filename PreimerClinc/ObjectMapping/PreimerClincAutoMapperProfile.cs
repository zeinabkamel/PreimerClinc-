using AutoMapper;
using PreimerClinc.Entities.Books;
using PreimerClinc.Services.Dtos.Books;

namespace PreimerClinc.ObjectMapping;

public class PreimerClincAutoMapperProfile : Profile
{
    public PreimerClincAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<BookDto, CreateUpdateBookDto>();
        /* Create your AutoMapper object mappings here */
    }
}