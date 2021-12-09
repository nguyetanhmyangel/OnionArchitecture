using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.MySpaces.Commands.Update
{
    public class UpdateMySpaceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string SeoAlias { get; set; }

        public string Description { get; set; }

        public string Environment { get; set; }

        public string Problem { get; set; }

        public string StepToReproduce { get; set; }

        public string ErrorMessage { get; set; }

        public string Workaround { get; set; }

        public string Note { get; set; }

        public string Labels { get; set; }

        public int? NumberOfComments { get; set; }

        public int? NumberOfVotes { get; set; }

        public int? NumberOfReports { get; set; }

        public class UpdateMySpaceCommandHandler : IRequestHandler<UpdateMySpaceCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMySpaceRepository _mySpaceRepository;

            public UpdateMySpaceCommandHandler(IMySpaceRepository mySpaceRepository, IUnitOfWork unitOfWork)
            {
                _mySpaceRepository = mySpaceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateMySpaceCommand command, CancellationToken cancellationToken)
            {
                var mySpace = await _mySpaceRepository.GetByIdAsync(command.Id);

                if (mySpace == null)
                {
                    return await Result<int>.FailAsync($"MySpace Not Found.");
                }

               
                mySpace.CategoryId = (command.CategoryId == 0) ? mySpace.CategoryId : command.CategoryId;
                mySpace.Title = command.Title ?? mySpace.Title; ;
                mySpace.SeoAlias = command.SeoAlias ?? mySpace.SeoAlias;
                mySpace.Environment = command.Environment ?? mySpace.Environment;
                mySpace.Description = command.Description ?? mySpace.Description;
                mySpace.Problem = command.Problem ?? mySpace.Problem;
                mySpace.StepToReproduce = command.StepToReproduce ?? mySpace.StepToReproduce;
                mySpace.ErrorMessage = command.ErrorMessage ?? mySpace.ErrorMessage;
                mySpace.Workaround = command.Workaround ?? mySpace.Workaround;
                mySpace.Note = command.Note ?? mySpace.Note;
                mySpace.Labels = command.Labels ?? mySpace.Labels;
                mySpace.NumberOfComments = (command.NumberOfComments == 0) ? mySpace.NumberOfComments : command.NumberOfComments;
                mySpace.NumberOfVotes = (command.NumberOfVotes == 0) ? mySpace.NumberOfVotes : command.NumberOfVotes;
                mySpace.NumberOfReports = (command.NumberOfReports == 0) ? mySpace.NumberOfReports : command.NumberOfReports;
                await _mySpaceRepository.UpdateAsync(mySpace);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(mySpace.Id);
            }
        }
    }
}