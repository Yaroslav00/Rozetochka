namespace DataAccess
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Category(string Name)
        {
            this.Name = Name;
        }

        public Category() { }
    }
}
