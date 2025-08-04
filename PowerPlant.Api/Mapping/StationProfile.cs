using AutoMapper;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;
using PowerPlant.Api.Entities;

namespace PowerPlant.Api.Mapping;

public class StationProfile : Profile
{
    public StationProfile()
    {
        CreateMap<Station, StationResponse>()
            .ForCtorParam(nameof(StationResponse.BlocksCount), o => o.MapFrom(s => s.EnergyBlocks.Count));
        
        CreateMap<CreateStationRequest, Station>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.EnergyBlocks, o => o.Ignore());
        
        CreateMap<UpdateStationRequest, Station>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.EnergyBlocks, o => o.Ignore());
    }
}