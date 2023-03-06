using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using PharmaMgmtApi.Commons.Exceptions;
using PharmaMgmtApi.Commons.Extensions;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.DbContexts;
using PharmaMgmtApi.Interfaces.IRepositories;
using PharmaMgmtApi.Interfaces.Services;
using PharmaMgmtApi.Models;
using PharmaMgmtApi.Repositories;
using PharmaMgmtApi.ViewModels.Drugs;

namespace PharmaMgmtApi.Services;

public class DrugService : IDrugService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDrugRepository _drugRepository;
        public DrugService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _drugRepository = new DrugRepository(_dbContext);
        }
        public async Task<DrugViewModel> CreateAsync(DrugCreateViewModel drugCreate)
        {
            var drug = _mapper.Map<Drug>(drugCreate);
            var drugView = await _drugRepository.CreateAsync(drug);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<DrugViewModel>(drugView);
        }

        public async Task<bool> DeleteAsync(Expression<Func<Drug, bool>> expression)
        {
            var drug = await _drugRepository.GetAsync(expression);

            if (drug is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Drug not found");

            await _drugRepository.DeleteAsync(drug);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<DrugViewModel>> GetAllAsync(Expression<Func<Drug, bool>>? expression = null, PaginationParams? @params = null)
        {
            var drugs = _drugRepository.GetAll(expression).ToPagedAsEnumerable(@params).Select(drug => _mapper.Map<DrugViewModel>(drug));
            return Task.FromResult(drugs);
        }

        public async Task<DrugViewModel?> GetAsync(Expression<Func<Drug, bool>> expression)
        {
            var drug = await _drugRepository.GetAsync(expression);

            if (drug is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Drug not found");

            return _mapper.Map<DrugViewModel>(drug);
        }

        public async Task<bool> UpdateAsync(Guid id, DrugCreateViewModel drugUpdate)
        {
            var drug = await _drugRepository.GetAsync(drug => drug.Id == id);

            if (drug is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Drug not found");

            var drugMap = _mapper.Map<Drug>(drugUpdate);
            drugMap.Id = drug.Id;
            drugMap.ImagePath = drugUpdate.ImageUrl;

            await _drugRepository.UpdateAsync(drugMap);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }