using Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CustomerEntity = DataAccess.Data.Customer;
using PostEntity = DataAccess.Data.Post;
using CreatePostsDto = Business.Dtos.CreatePostsDto;
using CreatePostDto = Business.Dtos.CreatePostDTo;
using System.Collections.Generic;

namespace API.Controllers.Post
{
	[Route("[controller]")]
	public class PostController : ControllerBase
	{
		private readonly BaseService<PostEntity> PostService;
		private readonly BaseService<CustomerEntity> CustomerService;
		public PostController(BaseService<PostEntity> postService, BaseService<CustomerEntity> customerService)
		{
			PostService = postService;
			CustomerService = customerService;
		}

		[HttpGet()]
		public IQueryable<PostEntity> GetAll()
		{
			return PostService.GetAll();
		}

		[HttpPost("bulk")]
		public List<PostEntity> CreateBulk([FromBody] CreatePostsDto entity)
		{
			List<PostEntity> result = new List<PostEntity>();
			entity.Posts.ForEach(it =>
			{
				result.Add(CreatePost(new PostEntity(it.Title, it.Body, it.Type, it.CustomerId)));
			});
			return result;
		}

		[HttpPost("single")]
		public PostEntity Create([FromBody] CreatePostDto entity)
		{
			return CreatePost(new PostEntity(entity.Title, entity.Body, entity.Type, entity.CustomerId));
		}
		private PostEntity CreatePost(PostEntity entity)
		{
			_ = CustomerService.GetById(entity.CustomerId) ?? throw new Exception("Customer Inválido");
			if (!string.IsNullOrEmpty(entity.Body) && entity.Body.Length > 97)
				entity.Body = entity.Body.Substring(0, 97) + "...";
			switch (entity.Type)
			{
				case 1:
					entity.Category = "Farándula";
					break;
				case 2:
					entity.Category = "Política";
					break;
				case 3:
					entity.Category = "Fútbol";
					break;
				default:
					break;
			}

			return PostService.Create(entity);

		}

		[HttpPut()]
		public PostEntity Update([FromBodyAttribute] PostEntity entity)
		{
			return PostService.Create(entity);
		}

		[HttpDelete()]
		public PostEntity Delete([FromBodyAttribute] PostEntity entity)
		{
			return PostService.Create(entity);
		}


	}
}
