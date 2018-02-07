using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace IMPA {
    [Activity(Label = "InfoPortfolioActivity")]
    public class InfoPortfolioActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.InfoPortfolio);

            EditText portfolioNameText = FindViewById<EditText>(Resource.Id.editPortfolioName);
            EditText ownersText = FindViewById<EditText>(Resource.Id.AddOwners);
            Button createPortfolio = FindViewById<Button>(Resource.Id.CreatePortfolio);

            createPortfolio.Click += delegate {
                Intent myIntent = new Intent(this, typeof(HomeActivity));
                myIntent.PutExtra("text", portfolioNameText.Text);
                SetResult(Result.Ok, myIntent);
                Finish();
            };
        }
    }
}