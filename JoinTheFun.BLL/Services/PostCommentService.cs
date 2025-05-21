using AutoMapper;
using JoinTheFun.BLL.DTO.Comments;
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
    public class PostCommentService : IPostCommentService
    {
        private readonly IPostCommentRepository _repo;
        private readonly IMapper _mapper;

        public PostCommentService(IPostCommentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostCommentDto>> GetByPostIdAsync(int postId)
        {
            var comments = await _repo.GetByPostIdAsync(postId);
            return _mapper.Map<IEnumerable<PostCommentDto>>(comments);
        }

        public async Task AddAsync(CreatePostCommentDto dto)
        {
            var entity = _mapper.Map<PostComment>(dto);

            //  ЗАБЕЗПЕЧ, що EF не намагається вставити зв’язані сутності
            entity.Post = null;
            entity.User = null;

            await _repo.AddAsync(entity);
        }


        public async Task DeleteAsync(int commentId) => await _repo.DeleteAsync(commentId);
    }
}
