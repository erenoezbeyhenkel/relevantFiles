namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

public sealed record TemplateSettingDto(long Id,
                                        decimal WaterHardness,
                                        decimal WaterLevel,
                                        decimal ChlorLevel,
                                        string MixingRatio,
                                        int? RotationTime,
                                        int? Drumspeed);