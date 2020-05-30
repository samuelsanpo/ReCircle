using System;
using System.Threading.Tasks;
using FireAuth;
using FireAuth.Droid;
using Firebase.Auth;
using Xamarin.Forms;

namespace FireAuth.Droid
{
    public class Login : IAuth
    {
        public Login()
        {
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
