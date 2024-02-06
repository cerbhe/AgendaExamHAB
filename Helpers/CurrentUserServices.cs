namespace AgendaExamHAB.Helpers
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CurrentUserServices(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetCurrentUserName()
        {
            return httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
