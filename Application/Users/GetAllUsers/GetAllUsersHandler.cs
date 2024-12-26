using Application.Core;
using Application.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.GetAllUsers;

public class GetAllUsersHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<PagedList<UserDto>>>
{
    public async Task<Result<PagedList<UserDto>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var currentUsername = userAccessor.GetCurrentUserUsername();

        var usersQuery = unitOfWork.UsersRepository
            .GetAllUsersQuery(currentUsername, request.UsersParams.SearchTerm);

        var users = await PagedList<UserDto>
            .CreateAsync(usersQuery.ProjectTo<UserDto>(mapper.ConfigurationProvider),
                request.UsersParams.PageNumber,
                request.UsersParams.PageSize);


        return Result<PagedList<UserDto>>.Success(users);
    }
}
