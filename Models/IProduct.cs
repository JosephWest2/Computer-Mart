namespace Computer_Mart.Models
{
    public interface IProduct
    {
        public float Price { get; set; }
        public string Name { get; set; }
        public string pictureUrl { get; set; }
        public int Id { get; set; }
    }
}
