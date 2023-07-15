namespace City_Vibe.Services.Base
{
    public class Response
    {
        public string Message { get; set; }
        public string ? ValidationErrors { get; set; }

        public bool? CurrentUser { get; set; }
        public bool? PhotoSucceeded { get; set; }
        public bool? Succeeded { get; set; }
        public bool? CurrentItem { get; set; }
    }
}
