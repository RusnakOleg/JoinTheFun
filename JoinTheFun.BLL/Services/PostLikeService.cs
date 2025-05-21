using AutoMapper;
using JoinTheFun.BLL.DTO.Likes;
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
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _repo;
        private readonly IMapper _mapper;

        public PostLikeService(IPostLikeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> IsLikedAsync(int postId, string userId) =>
            _repo.IsLikedAsync(postId, userId);

        public async Task LikeAsync(PostLikeDto dto)
        {
            var entity = _mapper.Map<PostLike>(dto);

            entity.Post = null;
            entity.User = null;

            await _repo.AddAsync(entity);
        }


        public async Task UnlikeAsync(PostLikeDto dto) =>
            await _repo.RemoveAsync(_mapper.Map<PostLike>(dto));
    }
}
