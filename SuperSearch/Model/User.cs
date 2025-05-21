namespace SuperSearch.model
{
    public class User
    {
        public int userId { get; set; }

        public string username { get; set; }
        
        public string email { get; set; }

        public string password { get; set; }

        public long phone_no { get; set; }

        public DateTime registerdate { get; set; }

        public string Isconfirm { get; set; }

    }
}
