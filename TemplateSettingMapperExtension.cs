using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class TemplateSettingMapperExtension
{
    public static TemplateSetting ToEntity(this TemplateSettingDto dto) => new()
    {
        Id = dto.Id,
        WaterHardness = dto.WaterHardness,
        WaterLevel = dto.WaterLevel,
        ChlorLevel = dto.ChlorLevel,
        MixingRatio = dto.MixingRatio,
        Drumspeed = dto.Drumspeed,
        RotationTime = dto.RotationTime
    };

    public static TemplateSettingDto ToDto(this TemplateSetting entity) => new(entity.Id,
                                                                                entity.WaterHardness,
                                                                                entity.WaterLevel,
                                                                                entity.ChlorLevel,
                                                                                entity.MixingRatio,
                                                                                entity.RotationTime,
                                                                                entity.Drumspeed);

    public static void Map(this TemplateSettingDto dto, TemplateSetting entity)
    {
        entity.WaterHardness = dto.WaterHardness;
        entity.WaterLevel = dto.WaterLevel;
        entity.ChlorLevel = dto.ChlorLevel;
        entity.MixingRatio = dto.MixingRatio;
        entity.RotationTime = dto.RotationTime;
        entity.Drumspeed = dto.Drumspeed;
    }

}
