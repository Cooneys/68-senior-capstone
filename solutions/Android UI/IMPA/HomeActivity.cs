using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;


namespace IMPA {
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Home);
            Button accountSettings = FindViewById<Button>(Resource.Id.AccountSettings);
            Button newPortfolio = FindViewById<Button>(Resource.Id.newPortfolio);

            newPortfolio.Click += delegate {
                NewPortfolioClick();
            };

            accountSettings.Click += delegate {
                GotoSettingsButtonClick();
            };
        }
        public void GotoSettingsButtonClick() {
            StartActivity(typeof(SettingsActivity));
        }

        public void NewPortfolioClick() {
            var i = new Intent(this, typeof(InfoPortfolioActivity));
            StartActivityForResult(i, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok) {
                Button newPortfolio = new Button(this) {
                    Text = data.GetStringExtra("text")
                };

                LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.HomeLinLayout);
                ll.AddView(newPortfolio);
            }
        }
    }
}