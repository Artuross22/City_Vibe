namespace CustomAPI.Models
{
    public class BoardStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BoardCollectionName { get; set; } = null!;
    }
}
