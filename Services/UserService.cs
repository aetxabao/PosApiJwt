
using PosApiJwt.Data;
using PosApiJwt.Helpers;
using PosApiJwt.Interfaces;
using PosApiJwt.Requests;
using PosApiJwt.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace PosApiJwt.Services
{
    public class UserService : IUserService
    {
        private readonly MessagesDbContext messagesDbContext;
        public UserService(MessagesDbContext messagesDbContext)
        {
            this.messagesDbContext = messagesDbContext;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = messagesDbContext.Users.SingleOrDefault(u => u.Active && u.Username == loginRequest.Username);

            if (user == null)
            {
                return null;
            }
            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, user.PasswordSalt);

            if (user.Password != passwordHash)
            {
                return null;
            }

            var token = await Task.Run(() => TokenHelper.GenerateToken(user));

            return new LoginResponse { Username = user.Username, Token = token };
        }

    }
}
