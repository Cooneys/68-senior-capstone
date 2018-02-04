using Android.App;
using Android.Widget;
using Android.OS;

namespace IMPA {
    [Activity(Label = "IMPA", MainLauncher = true)]
    public class MainActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);

            EditText usernameText = FindViewById<EditText>(Resource.Id.editUsername);
            EditText passwordText = FindViewById<EditText>(Resource.Id.editPassword);
            Button logIn = FindViewById<Button>(Resource.Id.LogInButton);
            Button guestSignIn = FindViewById<Button>(Resource.Id.guestSignIn);

            guestSignIn.Click += delegate {
                LoginButtonClick();
            };
        }
        public void LoginButtonClick() {
            StartActivity(typeof(HomeActivity));
        }
    }
}

