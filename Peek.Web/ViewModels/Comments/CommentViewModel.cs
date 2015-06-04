namespace Peek.Web.ViewModels.Comments
{
    using System;
    using AutoMapper;
    using Peek.Models;
    using Peek.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime PostedAt { get; set; }

        public string Username { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(c => c.Username, options => options.MapFrom(cvm => cvm.User.UserName));
        }
    }
}
