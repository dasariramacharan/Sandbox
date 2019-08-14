using MediatR;

namespace ServiceLayer.Auth
{

    public class LoginCredentials : IRequest<bool>
    {
        public string UserOAuth { get; set; }
    }

    // if the request is completely synchronous, inherit from the base RequestHandler class
    public class ValidateUserHandler : RequestHandler<LoginCredentials, bool>
    {
        protected override bool Handle(LoginCredentials request)
        {
            //We believe everyone is valid user :)
            return true;
        }
    }
}
