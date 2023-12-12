namespace CrudTZ.Services.UserService
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetAllUsers();
        Task<List<GetUserDto>> AddUser(AddUserDto user);
        Task<GetUserDto>? GetSingleUser(string email);
        Task<GetUserDto>? GetSingleUserById(int id);
        Task<List<GetUserDto>>? UpdateUser(int id, AddUserDto request);
        Task<List<GetUserDto>>? DeleteUser(int id);


    }
}
