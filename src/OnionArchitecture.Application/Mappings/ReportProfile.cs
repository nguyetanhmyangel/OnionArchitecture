using AutoMapper;
using OnionArchitecture.Application.Features.Reports.Commands.Create;
using OnionArchitecture.Application.Features.Reports.Queries.Get;
using OnionArchitecture.Application.Features.Reports.Queries.GetById;
using OnionArchitecture.Application.Features.Reports.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<CreateReportCommand, Report>().ReverseMap();
            CreateMap<GetReportByIdResponse, Report>().ReverseMap();
            CreateMap<GetReportResponse, Report>().ReverseMap();
            CreateMap<GetPageReportResponse, Report>().ReverseMap();
        }
    }
}