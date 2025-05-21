using AutoMapper;
using JoinTheFun.BLL.DTO.Posts;
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
    public class PostService : IPostService
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;

        public PostService(IPostRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto?> GetByIdAsync(int id)
        {
            var post = await _repo.GetByIdAsync(id);
            return _mapper.Map<PostDto>(post);
        }

        public async Task CreateAsync(CreatePostDto dto)
        {
            var entity = _mapper.Map<Post>(dto);
            await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<IEnumerable<PostDto>> GetPostsByFollowingsAsync(string userId)
        {
            var posts = await _repo.GetPostsByFollowingsAsync(userId);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

    }
}
