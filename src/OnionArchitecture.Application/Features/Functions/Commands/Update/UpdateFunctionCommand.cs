using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Functions.Commands.Update
{
    public class UpdateFunctionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public int SortOrder { get; set; }

        public int ParentId { get; set; }

        public string Icon { get; set; }

        public class UpdateFunctionCommandHandler : IRequestHandler<UpdateFunctionCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFunctionRepository _functionRepository;

            public UpdateFunctionCommandHandler(IFunctionRepository functionRepository, IUnitOfWork unitOfWork)
            {
                _functionRepository = functionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateFunctionCommand command, CancellationToken cancellationToken)
            {
                var function = await _functionRepository.GetByIdAsync(command.Id);

                if (function == null)
                {
                    return await Result<int>.FailAsync($"Function Not Found.");
                }

                function.Name = command.Name ?? function.Name;
                function.Url = command.Url ?? function.Url;
                function.Icon = command.Icon ?? function.Icon;
                function.ParentId = (command.ParentId == 0) ? function.ParentId : command.ParentId;
                function.SortOrder = (command.SortOrder == 0) ? function.SortOrder : command.SortOrder;
                await _functionRepository.UpdateAsync(function);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(function.Id);
            }
        }
    }
}