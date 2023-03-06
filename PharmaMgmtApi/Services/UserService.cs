using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PharmaMgmtApi.Commons.Exceptions;
using PharmaMgmtApi.Commons.Extensions;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.DbContexts;
using PharmaMgmtApi.Interfaces.IRepositories;
using PharmaMgmtApi.Interfaces.Services;
using PharmaMgmtApi.Models;
using PharmaMgmtApi.Repositories;
using PharmaMgmtApi.Security;
using PharmaMgmtApi.ViewModels.Users;

namespace PharmaMgmtApi.Services;

public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UserService(AppDbContext dbContext, 
            IMapper mapper, 
            IFileService fileService, 
            IMemoryCache cache, 
            IEmailService emailService)
        {
            _dbContext = dbContext;
            _userRepository = new UserRepository(_dbContext);
            _mapper = mapper;
            _cache = cache;
            _emailService = emailService;
            _fileService = fileService;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            await _fileService.DeleteImageAsync(user.ImagePath);
            await _userRepository.DeleteAsync(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null,
            PaginationParams? @params = null)
        {
            var users = _userRepository.GetAll(expression).ToPagedAsEnumerable(@params);
            var userViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                var item = _mapper.Map<UserViewModel>(user);
                userViews.Add(item);
            }
            return userViews;
        }

        public async Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public async Task<UserViewModel> UpdateAsync(Guid id, UserCreateViewModel userCreateViewModel)
        {
            var entity = await _userRepository.GetAsync(user => user.Id == id);

            if (entity is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            await _fileService.DeleteImageAsync(entity.ImagePath);

            User user = _mapper.Map<User>(userCreateViewModel);
            var hasherResult = PasswordHasher.Hash(userCreateViewModel.Password);

            user.Id = id;
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;
            user.EmailConfirmed = true;
            user.ImagePath = await _fileService.SaveImageAsync(userCreateViewModel.Image);

            await _userRepository.UpdateAsync(user);
            await _dbContext.SaveChangesAsync();

            var code = RandomNumberGenerator.GetInt32(1000, 9999).ToString();
            _cache.Set(userCreateViewModel.Email, code, TimeSpan.FromMinutes(10));
            await _emailService.SendAsync(userCreateViewModel.Email, code);

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.ImageUrl = user.ImagePath;
            return userViewModel;
        }
    }