using AutoMapper;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;
using PowerPlant.Api.Entities;

namespace PowerPlant.Api.Mapping;

public class EnergyBlockProfile : Profile
{
    public EnergyBlockProfile()
    {
        CreateMap<EnergyBlock, EnergyBlockResponse>();
        
        CreateMap<CreateEnergyBlockRequest, EnergyBlock>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.StationId, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.Station, o => o.Ignore());
        
        CreateMap<UpdateEnergyBlockRequest, EnergyBlock>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.StationId, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.Station, o => o.Ignore());
    }
}