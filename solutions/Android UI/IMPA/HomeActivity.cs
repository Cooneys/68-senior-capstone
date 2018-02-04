using Android.App;
using Android.Widget;
using Android.OS;

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
            Button newPortfolio = new Button(this);

            LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.HomeLinLayout);
            ll.AddView(newPortfolio);
        }
    }
}