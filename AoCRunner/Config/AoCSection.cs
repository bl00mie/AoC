namespace AoCRunner.Config
{
    public class AoCSection : SettingsSection
    {
        public string DefaultYear { get; set; } = "2022";
        public string DefaultDay { get; set; } = "1";
        public string SessionCookie { get; set; } = "";
    }
}
