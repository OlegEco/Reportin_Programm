namespace Reportin_Programm.Services.SMTPService
{
    public interface SMTPConfiguration
    {
        public string SenderMail { get; set; }
        public string SenderPassword { get; set; }
        public string SenderName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
