namespace RestAPISampleAutoTests.Consts
{
    public static class UserEndpoints
    {
        public const string SingleUserEndpoint = "/api/users/{userId}";
        public const string ListUsersEndpoint = "/api/users";
        public const string ListUsersByPageEndpoint = "/api/users?page={page}";
    }
}