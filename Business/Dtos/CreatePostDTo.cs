namespace Business.Dtos
{
	public class CreatePostDTo
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public int Type { get; set; }
		public int CustomerId { get; set; }
	}
}
