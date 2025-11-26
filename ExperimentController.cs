using Hcb.Rnd.Pwn.Api.Controllers.Base;
using Hcb.Rnd.Pwn.Common.Constants;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.ChangeStatus;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Delete;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Template.AssignedTemplateGroup.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Template.AssignedTemplateGroup.Delete;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Template.Copy;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Template.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Update;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Notifications.ExperimentLoadingWashRun;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetAllExperimentTemplatesSummary;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetAllExperimentTemplateSummariesByUser;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetById;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetExperimentsByProductDeveloperGroup;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetExperimentsForAllProductDeveloperGroups;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetExperimentSummaryById;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetExperimentTemplateSummaryById;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetExperimentTypesByProductDeveloperGroup;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetPillingImagesById;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetTemplateById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hcb.Rnd.Pwn.Api.Controllers.Experiments.Experiment;

/// <summary>
///  Includes experiment features
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
[Route("api/v1.0/experiment/")]
public class ExperimentController(ISender sender,
                                  IMediator mediator) : ApiController(sender)
{

    /// <summary>
    /// To create a new experiment
    /// </summary>
    /// <param name="createExperimentCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloper)]
    [HttpPost("create")]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(CreateExperimentCommandResponse))]
    public async Task<IActionResult> Create([FromBody] CreateExperimentCommand createExperimentCommand, CancellationToken cancellationToken)
    {
        var createResult = await Sender.Send(createExperimentCommand, cancellationToken);
        return createResult.Match(
            createResult => CreatedAtAction(nameof(Create), createExperimentCommand, createResult),
            Problem
            );
    }

    /// <summary>
    /// To create a new experiment template
    /// </summary>
    /// <param name="createExperimentTemplateCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerTemplateCreator)]
    [HttpPost("createTemplate")]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(CreateExperimentTemplateCommandResponse))]
    public async Task<IActionResult> Create([FromBody] CreateExperimentTemplateCommand createExperimentTemplateCommand, CancellationToken cancellationToken)
    {
        var createResult = await Sender.Send(createExperimentTemplateCommand, cancellationToken);
        return createResult.Match(
            createResult => CreatedAtAction(nameof(Create), createExperimentTemplateCommand, createResult),
            Problem
            );
    }

    /// <summary>
    /// To copy experiment template
    /// </summary>
    /// <param name="copyExperimentTemplateCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerTemplateCreator)]
    [HttpPost("copyTemplate")]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(CopyExperimentTemplateCommandResponse))]
    public async Task<IActionResult> CopyTemplate([FromBody] CopyExperimentTemplateCommand copyExperimentTemplateCommand, CancellationToken cancellationToken)
    {
        var createResult = await Sender.Send(copyExperimentTemplateCommand, cancellationToken);
        return createResult.Match(
            createResult => CreatedAtAction(nameof(Create), copyExperimentTemplateCommand, createResult),
            Problem
            );
    }

    /// <summary>
    /// To assign experiment template to other groups.(To share experiment template with other groups)
    /// </summary>
    /// <param name="createAssignedExperimentTemplatesCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerTemplateCreator)]
    [HttpPost("create/assignedExperimentTemplateGroups")]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(CreateAssignedExperimentTemplatesCommandResponse))]
    public async Task<IActionResult> CreateAssignedExperimentTemplateGroups([FromBody] CreateAssignedExperimentTemplateGroupsCommand createAssignedExperimentTemplatesCommand, CancellationToken cancellationToken)
    {
        var createResult = await Sender.Send(createAssignedExperimentTemplatesCommand, cancellationToken);
        return createResult.Match(
            createResult => CreatedAtAction(nameof(CreateAssignedExperimentTemplateGroups), createAssignedExperimentTemplatesCommand, createResult),
            Problem
            );
    }

    /// <summary>
    /// To notify experiment loading wash run
    /// </summary>
    /// <param name="experimentLoadingWashRunNotification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashOperator)]
    [HttpPost("notify/experimentLoadingOfWashRun")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> NotifyExperimentLoadingWashRun([FromBody] ExperimentLoadingWashRunNotification experimentLoadingWashRunNotification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        await mediator.Publish(experimentLoadingWashRunNotification, cancellationToken);
        return Ok(true);
    }

    /// <summary>
    /// To update experiment.
    /// </summary>
    /// <param name="updateExperimentCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndTemplateCreatorAndOperator)]
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateExperimentCommandResponse))]
    public async Task<IActionResult> Update([FromBody] UpdateExperimentCommand updateExperimentCommand, CancellationToken cancellationToken)
    {
        var updateResult = await Sender.Send(updateExperimentCommand, cancellationToken);

        return updateResult.Match(Ok, Problem);
    }


    /// <summary>
    /// To change the status of an experiment.
    /// </summary>
    /// <param name="changeStatusExperimentCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndOperator)]
    [HttpPut("changeStatus")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangeStatusExperimentCommandResponse))]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusExperimentCommand changeStatusExperimentCommand, CancellationToken cancellationToken)
    {
        var updateResult = await Sender.Send(changeStatusExperimentCommand, cancellationToken);

        return updateResult.Match(Ok, Problem);
    }


    /// <summary>
    /// To delete experiment physically.
    /// </summary>
    /// <param name="experimentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloperAndTemplateCreator)]
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteExperimentCommandResponse))]
    public async Task<IActionResult> Delete([FromQuery(Name = "experimentId")] long experimentId, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteExperimentCommand(experimentId), cancellationToken);

        return result.Match(Ok, Problem);
    }

    /// <summary>
    /// To delete the assignments on experiment template.
    /// </summary>
    /// <param name="deleteAssignedExperimentTemplatesCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerTemplateCreator)]
    [HttpDelete("delete/assignedExperimentTemplateGroups")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(DeleteAssignedExperimentTemplateGroupsCommandResponse))]
    public async Task<IActionResult> DeleteAssignedExperimentTemplateGroups(DeleteAssignedExperimentTemplateGroupsCommand deleteAssignedExperimentTemplatesCommand, CancellationToken cancellationToken)
    {
        var deleteResult = await Sender.Send(deleteAssignedExperimentTemplatesCommand, cancellationToken);
        return deleteResult.Match(Ok, Problem);
    }


    /// <summary>
    /// To get all template experiment summary data
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloperAndTemplateCreator)]
    [HttpGet("getAllExperimentTemplatesSummary")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllExperimentTemplatesSummaryQueryResponse))]
    public async Task<IActionResult> GetAllTemplateExperimentsSummary(CancellationToken cancellationToken)
    {
        var experimentTemplatesSummary = await Sender.Send(new GetAllExperimentTemplatesSummaryQuery(), cancellationToken);

        return experimentTemplatesSummary.Match(Ok, Problem);
    }

    /// <summary>
    /// To get all template experiment summary data by user groups
    /// </summary>
    /// <param name="onlyActive"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloper)]
    [HttpGet("GetAllExperimentTemplateSummariesByUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllExperimentTemplateSummariesByUserQueryResponse))]
    public async Task<IActionResult> GetAllExperimentTemplateSummariesByUser([FromQuery(Name = "onlyActive")] bool onlyActive, CancellationToken cancellationToken)
    {
        var experimentTemplatesSummary = await Sender.Send(new GetAllExperimentTemplateSummariesByUserQuery(new GetAllExperimentTemplateSummariesByUserQueryDto(onlyActive)), cancellationToken);

        return experimentTemplatesSummary.Match(Ok, Problem);
    }


    /// <summary>
    /// To get all data of experiment by id
    /// </summary>
    /// <param name="experimentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndOperator)]
    [HttpGet("getExperimentById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExperimentByIdQueryResponse))]
    public async Task<IActionResult> GetExperimentById([FromQuery(Name = "experimentId")] long experimentId, CancellationToken cancellationToken)
    {
        var experiment = await Sender.Send(new GetExperimentByIdQuery(experimentId), cancellationToken);

        return experiment.Match(Ok, Problem);
    }

    /// <summary>
    /// To get all data of experiment template by id
    /// </summary>
    /// <param name="experimentTemplateId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndTemplateCreatorAndOperator)]
    [HttpGet("getExperimentTemplateById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExperimentTemplateByIdQueryResponse))]
    public async Task<IActionResult> GetExperimentTemplateById([FromQuery(Name = "experimentTemplateId")] long experimentTemplateId, CancellationToken cancellationToken)
    {
        var experimentTemplate = await Sender.Send(new GetExperimentTemplateByIdQuery(experimentTemplateId), cancellationToken);

        return experimentTemplate.Match(Ok, Problem);
    }

    /// <summary>
    /// To get experimentTemplateSummary by id.
    /// </summary>
    /// <param name="experimentTemplateId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndTemplateCreatorAndOperator)]
    [HttpGet("getExperimentTemplateSummaryById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExperimentTemplateSummaryByIdQueryResponse))]
    public async Task<IActionResult> GetExperimentTemplateSummaryById([FromQuery(Name = "experimentTemplateId")] long experimentTemplateId, CancellationToken cancellationToken)
    {
        var experiment = await Sender.Send(new GetExperimentTemplateSummaryByIdQuery(experimentTemplateId), cancellationToken);

        return experiment.Match(Ok, Problem);
    }

    /// <summary>
    /// To get experiment summary by experiment id.
    /// </summary>
    /// <param name="experimentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndTemplateCreator)]
    [HttpGet("getExperimentSummaryById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExperimentSummaryByIdQueryResponse))]
    public async Task<IActionResult> GetExperimentSummaryById([FromQuery(Name = "experimentId")] long experimentId, CancellationToken cancellationToken)
    {
        var experiment = await Sender.Send(new GetExperimentSummaryByIdQuery(experimentId), cancellationToken);

        return experiment.Match(Ok, Problem);
    }

    /// <summary>
    /// To get experiments by PD group id and days.
    /// </summary>
    /// <param name="pdGroupId"></param>
    /// <param name="numberOfDays"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("getExperimentCountByProductDeveloperGroup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExperimentsCountByProductDeveloperGroupQueryResponse))]
    public async Task<IActionResult> GetExperimentCountByProductDeveloperGroup([FromQuery(Name = "pdGroupId")] Guid pdGroupId, [FromQuery(Name = "days")] int numberOfDays, CancellationToken cancellationToken)
    {
        var experiment = await Sender.Send(new GetExperimentsCountByProductDeveloperGroupQuery(pdGroupId, numberOfDays), cancellationToken);

        return experiment.Match(Ok, Problem);
    }

    /// <summary>
    /// To get experiment Types by PD group id and days .
    /// </summary>
    /// <param name="pdGroupId"></param>
    /// <param name="numberOfDays"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("getExperimentTypesByProductDeveloperGroup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExperimentTypesByProductDeveloperGroupQueryResponse))]
    public async Task<IActionResult> GetExperimentTypesByProductDeveloperGroup([FromQuery(Name = "pdGroupId")] Guid pdGroupId, [FromQuery(Name = "days")] int numberOfDays, CancellationToken cancellationToken)
    {
        var experiment = await Sender.Send(new GetExperimentTypesByProductDeveloperGroupQuery(pdGroupId, numberOfDays), cancellationToken);

        return experiment.Match(Ok, Problem);
    }
    /// <summary>
    /// To get experiments by PD group id date time.
    /// </summary>
    /// <param name="numberOfDays"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("getExperimentCountForAllProductDeveloperGroups")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExperimentsForAllProductDeveloperGroupsQueryResponse))]
    public async Task<IActionResult> GetExperimentCountForAllProductDeveloperGroups([FromQuery(Name = "days")] int numberOfDays, CancellationToken cancellationToken)
    {
        var experiment = await Sender.Send(new GetExperimentsForAllProductDeveloperGroupsQuery(numberOfDays), cancellationToken);

        return experiment.Match(Ok, Problem);
    }

    /// <summary>
    /// To get the pilling images by experiment.
    /// </summary>
    /// <param name="experimentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.HcbpaManager)]
    [HttpGet("getPillingImagesByExperimentId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPillingImagesByExperimentIdQueryResponse))]
    public async Task<IActionResult> GetPillingImagesByExperimentId([FromQuery(Name = "experimentId")] long experimentId, CancellationToken cancellationToken)
    {
        var pillingImages = await Sender.Send(new GetPillingImagesByExperimentIdQuery(experimentId), cancellationToken);
        return pillingImages.Match(Ok, Problem);
    }
}
