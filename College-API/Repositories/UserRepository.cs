using AutoMapper;
using AutoMapper.QueryableExtensions;
using College_API.Data;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace College_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CollegeDatabaseContext _context;
        private readonly IMapper _mapper;
        public UserRepository(CollegeDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<UserViewModel>> ListAllUsersAsync()
        {
            return await _context.Users.ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            // return await _context.Users.ToListAsync();
        }

        public async Task<UserViewModel?> GetUserAsync(int id)
        {
            return await _context.Users.Where(c => c.Id == id)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            // return await _context.Users.Where(c => c.Id == id).Select(user => new UserViewModel
            // {//vid 20220428 13.. 1:47:00
            //     UserId = user.Id,
            //     UserName = string.Concat(user.FirstName, " ", user.LastName),
            //     Email = user.Email,
            //     PhoneNumber = user.PhoneNumber,
            //     Address = user.Address
            // }).SingleOrDefaultAsync();
        }

        public async Task<UserViewModel?> GetUserAsync(string userName)
        {
            return await _context.Users.Where(c => c.FirstName!.ToLower() == userName.ToLower() || c.LastName!.ToLower() == userName.ToLower())
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            // return await _context.Users.Where(c => c.FirstName == userName || c.LastName == userName).Select(user => new UserViewModel
            // {//vid 20220428 13.. 1:47:00
            //     UserId = user.Id,
            //     UserName = string.Concat(user.FirstName, " ", user.LastName),
            //     Email = user.Email,
            //     PhoneNumber = user.PhoneNumber,
            //     Address = user.Address
            // }).SingleOrDefaultAsync();
            //FirstOrDefaultAsync(c => c.FirstName == userName || c.LastName == userName);
        }

        public async Task<UserViewModel?> GetUserEmailAsync(string userEmail)
        {
            return await _context.Users.Where(c => c.Email!.ToLower() == userEmail.ToLower())
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
        public async Task AddUserAsync(PostUserViewModel model)
        {

            var userToAdd = _mapper.Map<User>(model);
            await _context.Users.AddAsync(userToAdd);

        }

        public async Task AddUserToCourseAsync(int id)//TODO
        {
            /*Adding a user to a course
    Find the course
        Find the user
    add it in
*/
            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                throw new Exception($"We couldn't find a user with id: {id}");
            }
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(int id, PostUserViewModel model)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                throw new Exception($"We couldn't find a user with id: {id}");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            _context.Users.Update(user);
        }
        public async Task UpdateUserAsync(int id, PatchUserViewModel model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                throw new Exception($"Could not find user with id: {id}");

            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            _context.Users.Update(user);
        }
        public async Task DeleteUserAsync(int id)    //we changed void to Task 220503_09 56:21
        {
            var response = await _context.Users.FindAsync(id);
            if (response is not null)
                _context.Users.Remove(response);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
//throw new NotImplementedException();