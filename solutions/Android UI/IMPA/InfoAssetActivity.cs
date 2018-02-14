using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace IMPA {
    [Activity(Label = "InfoAssetActivity")]
    public class InfoAssetActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.InfoAsset);

            EditText assetNameText = FindViewById<EditText>(Resource.Id.editAssetName);
            Button addAsset = FindViewById<Button>(Resource.Id.AddAsset);

            addAsset.Click += delegate {
                Intent myIntent = new Intent(this, typeof(PortfolioActivity));
                myIntent.PutExtra("text", assetNameText.Text);
                SetResult(Result.Ok, myIntent);
                Finish();
            };
        }
    }
}