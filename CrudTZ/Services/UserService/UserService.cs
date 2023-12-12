
using CrudTZ.Models;

namespace CrudTZ.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetUserDto>> AddUser(AddUserDto user)
        {
            user.CreateDate = DateTime.Now;
            _context.Users.Add(_mapper.Map<User>(user));
            await _context.SaveChangesAsync();
            var listUsers = await _context.Users.ToListAsync();
            return listUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
        }
        
        public async Task<List<GetUserDto>>? DeleteUser(int id)
        {
            var deleteUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (deleteUser is null)
                return null;
            _context.Users.Remove(deleteUser);
            await _context.SaveChangesAsync();
            var listUsers= await _context.Users.ToListAsync();
            return listUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
        }

        public async Task<List<GetUserDto>> GetAllUsers()
        {
            var dbUsers = await _context.Users.ToListAsync();
            return dbUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
        }

        public async Task<GetUserDto>? GetSingleUser(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(p => p.Email == email);
            if (result is null)
                return null;
            return _mapper.Map<GetUserDto>(result);
        }

        public async Task<GetUserDto>? GetSingleUserById(int id)
        {
            var result = await _context.Users.FindAsync(id);
            if (result is null)
            {
                return null;
            }
            return _mapper.Map<GetUserDto>(result);
        }

        public async Task<List<GetUserDto>>? UpdateUser(int id, AddUserDto request)
        {
            var updateUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (updateUser is null)
                return null;
            updateUser.NickName = request.NickName;
            updateUser.Email = request.Email;
            updateUser.Comments = request.Comments;
            await _context.SaveChangesAsync();

            var listUsers = await _context.Users.ToListAsync();
            return listUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
        }
    }
}
