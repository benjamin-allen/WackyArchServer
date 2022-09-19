namespace WackyArchServer.Model
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Passwordhash { get; set; }
    }
}
