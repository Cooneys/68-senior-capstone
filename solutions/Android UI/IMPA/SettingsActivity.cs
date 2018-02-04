using Android.App;
using Android.Widget;
using Android.OS;

namespace IMPA {
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Settings);
        }
    }
}