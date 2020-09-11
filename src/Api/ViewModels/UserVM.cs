using System;
using MediatR;

namespace Api.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserVM : IRequest<UserVM>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateVM : IRequest<LoggedUserVM>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoggedUserVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
