namespace WackyArchServer.Model
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Passwordhash { get; set; }
    }
}
