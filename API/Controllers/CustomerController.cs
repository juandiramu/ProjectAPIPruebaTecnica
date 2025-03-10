using Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CustomerEntity = DataAccess.Data.Customer;
using PostEntity = DataAccess.Data.Post;
using CreateCustomerDto = Business.Dtos.CreateCustomerDTo;

namespace API.Controllers.Customer
{
	[Route("[controller]")]
	public class CustomerController : ControllerBase
	{
		private BaseService<CustomerEntity> CustomerService;
		private readonly BaseService<PostEntity> PostService;
		public CustomerController(BaseService<CustomerEntity> customerService, BaseService<PostEntity> postService)
		{
			CustomerService = customerService;
			PostService = postService;
		}


		[HttpGet()]
		public IQueryable<CustomerEntity> GetAll()
		{
			return CustomerService.GetAll();
		}

		[HttpPost()]
		public CustomerEntity Create([FromBodyAttribute] CreateCustomerDto entity)
		{
			CustomerEntity existCustomer = CustomerService.GetByFilter(it => it.Name == entity.Name).FirstOrDefault();
			if (existCustomer != null)
				throw new Exception("Nombre ya registrado");

			return CreateCustomer(new CustomerEntity(entity.Name));
		}

		private CustomerEntity CreateCustomer(CustomerEntity entity)
		{
			return CustomerService.Create(entity);
		}

		[HttpPut()]
		public CustomerEntity Update([FromBody] CustomerEntity entity)
		{
			return CustomerService.Update(entity.CustomerId, entity, out bool changed);
		}

		[HttpDelete()]
		public CustomerEntity Delete([FromQuery] int id)
		{
			CustomerEntity entity = CustomerService.GetById(id);
			var posts = PostService.GetByFilter(it => it.CustomerId == entity.CustomerId);
			foreach (var post in posts)
				PostService.Delete(post);
			PostService.SaveChanges();
			CustomerEntity deletedCustomer = CustomerService.Delete(entity);
			CustomerService.SaveChanges();
			return deletedCustomer;
		}
	}
}
