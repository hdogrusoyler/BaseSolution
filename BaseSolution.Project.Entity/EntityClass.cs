using BaseSolution.Core.Entity;

namespace BaseSolution.Project.Entity
{
    public class Title : IEntity
    {
        public Title()
        {
            Text = "";
            Category = new Category();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class Category : IEntity
    {
        public Category()
        {
            Text = "";
            Titles = new List<Title>();
        }        
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Title> Titles { get; set; }
    }

}