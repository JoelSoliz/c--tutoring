namespace WatchTrackerAPI.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<UserMediaProgress> Progresses { get; set; } = new List<UserMediaProgress>(); //initialize directly

        public bool IsDeleted { get; set; } = false;
    }
}
