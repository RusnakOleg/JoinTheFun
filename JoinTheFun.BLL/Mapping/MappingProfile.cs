using AutoMapper;
using JoinTheFun.BLL.DTO.Comments;
using JoinTheFun.BLL.DTO.Events;
using JoinTheFun.BLL.DTO.Posts;
using JoinTheFun.BLL.DTO.Profiles;
using JoinTheFun.BLL.DTO.Interests;
using JoinTheFun.BLL.DTO.Likes;
using JoinTheFun.BLL.DTO.Folowers;
using JoinTheFun.BLL.DTO.UserInterest;
using JoinTheFun.BLL.DTO.Event_Participants;
using JoinTheFun.DAL.Entities;

namespace JoinTheFun.BLL.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // Event
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.CreatorId))
                .ForMember(dest => dest.CreatorUsername, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.ParticipantCount, opt => opt.MapFrom(src => src.Participants.Count));

            CreateMap<CreateEventDto, Event>();

            // Post
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count));

            CreateMap<CreatePostDto, Post>();

            // Profile
            CreateMap<DAL.Entities.Profile, ProfileDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Interests, opt => opt.MapFrom(src =>
                src.Interests != null
                ? src.Interests.Select(i => i.Interest.Name).ToList()
                : new List<string>()
                ))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.ApplicationUser.UserName));

            CreateMap<UpdateProfileDto, DAL.Entities.Profile>()
                .ForMember(x => x.AvatarUrl, opt => opt.MapFrom((src) => ConvertBase64ToBytes(src.AvatarUrl)))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Comment
            CreateMap<PostComment, PostCommentDto>()
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<CreatePostCommentDto, PostComment>();

            // Interest
            CreateMap<Interest, InterestDto>();
            CreateMap<CreateInterestDto, Interest>();

            // Like
            CreateMap<PostLikeDto, PostLike>();
            CreateMap<PostLike, PostLikeDto>();

            // Follow
            CreateMap<FollowDto, Follow>();
            CreateMap<Follow, FollowDto>();

            // UserInterest
            CreateMap<AddUserInterestDto, UserInterest>();
            CreateMap<UserInterest, UserInterestDto>()
                .ForMember(dest => dest.InterestName, opt => opt.MapFrom(src => src.Interest.Name));

            // EventParticipant
            CreateMap<EventParticipant, EventParticipantDto>();
            CreateMap<EventParticipantDto, EventParticipant>();
            CreateMap<RemoveEventParticipantDto, EventParticipant>();


        }

        public static byte[] ConvertBase64ToBytes(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return null;

            // If the UI sends "data:image/png;base64,XXXX", strip the header
            var commaIndex = base64.IndexOf(',');

            if (commaIndex >= 0)
                base64 = base64.Substring(commaIndex + 1);

            return Convert.FromBase64String(base64);
        }
    }
}
