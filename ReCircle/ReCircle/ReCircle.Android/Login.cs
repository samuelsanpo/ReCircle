using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(FireAuth.Droid.Login))]
namespace FireAuth.Droid
{
    public class Login : IAuth
    {
        async public Task<string> CreateNewUser(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return user.User.Uid;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }

        async public Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);                
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }

        
    }
}
