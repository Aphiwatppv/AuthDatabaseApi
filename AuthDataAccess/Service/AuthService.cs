using AuthDataAccess.Models;
using AuthDataAccess.SecurityLogic;
using AuthDataAccess.SqlAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace AuthDataAccess.Service;

public class AuthService : IAuthService
{

    private readonly ISqlAccess _sqlAccess;

 

    public AuthService(ISqlAccess sqlAccess)
    {
        _sqlAccess = sqlAccess;
    }

    public async Task<string> RegisterMethod(RegisteringModel model)
    {
        var hashPass = EncryptPassword.HashPassword(model.Password);

        var parameters = new
        {
            model.FirstName,
            model.LastName,
            model.Email,
            model.PhoneNumber,
            model.IdentityID,
            hashPass.HashPassword,
            hashPass.Salt,
        };


        string result = await _sqlAccess.UpdateAsyncWithReturning<dynamic>(
            storedProcedure: "dbo.spRegisterUser", parameters: parameters);

        ResultModel.ResultInformation = result;

        return result;
    }


    // Return Hash && Salt
    public async Task<ReturnHashSaltModel> ReturningHashSalt(string email)
    {
        var result = await _sqlAccess.LoadSingleAsync<ReturnHashSaltModel, dynamic>(storedProcedure: "dbo.spReturnHashSalt", new
        {
            Email = email
        });

        return result;

    }
    public bool IsHashValid(string hashpass, string salt, string userpass)
    {
        return EncryptPassword.VerifyPassword(userpass, salt, hashpass);
    }

    public async Task<string> LoginAsync(UserInput userInput)
    {
        var result = await ReturningHashSalt(userInput.Email);
        if (result != null)
        {
            if (IsHashValid(result.HashPassword, result.Salt, userInput.Password))
            {

           
                return "Login Successful"; // Replace with Token 
            }
            else
            {
                return "Invalid Email or Password";
            }

        }
        else
        {
            return "Invalid Email or Password";
        }


    }
}
    

