using Android.App;
using Android.Widget;
using Android.OS;

namespace IMPA {
    [Activity(Label = "PortfolioActivity")]
    public class PortfolioActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Portfolio);
        }
    }
}