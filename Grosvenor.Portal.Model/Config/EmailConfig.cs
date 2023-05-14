namespace Grosvenor.Portal.Model.Config
{
    public class EmailConfig
    {
        public bool Enabled { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmailMask { get; set; }
        public string SubjectPrefix { get; set; }
    }
}
