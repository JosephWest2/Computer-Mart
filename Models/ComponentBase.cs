using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Computer_Mart.Models
{
	public abstract class ComponentBase
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[DataType(DataType.Currency)]
		[Required]
		public float Price { get; set; }
		[Required]
		[DisplayName("Image URL")]
		public string pictureUrl { get; set; }
	}
}
