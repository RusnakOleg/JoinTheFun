using AutoMapper;
using JoinTheFun.BLL.DTO.Folowers;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.DAL.Entities;
using JoinTheFun.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _repo;
        private readonly IMapper _mapper;

        public FollowService(IFollowRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> IsFollowingAsync(string followerId, string followedId) =>
            _repo.IsFollowingAsync(followerId, followedId);

        public async Task FollowAsync(FollowDto dto) =>
            await _repo.AddAsync(_mapper.Map<Follow>(dto));

        public async Task UnfollowAsync(FollowDto dto) =>
            await _repo.RemoveAsync(_mapper.Map<Follow>(dto));
    }
}
