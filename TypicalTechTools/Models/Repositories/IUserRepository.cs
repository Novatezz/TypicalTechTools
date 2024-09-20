using TypicalTechTools.Models.DTOs;
using TypicalTechTools.Models;

namespace TypicalTechTools.Models.Repositories
{
    public interface IUserRepository
    {
        AdminUser Authenticate(LoginDTO loginDTO);
        AdminUser CreateUser(CreateUserDTO createUserDTO);
    }
}
