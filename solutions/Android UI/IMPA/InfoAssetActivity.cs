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
            EditText assetPriceText = FindViewById<EditText>(Resource.Id.editAssetPrice);
            EditText assetNumberText = FindViewById<EditText>(Resource.Id.editAssetOwned);
            Button addAsset = FindViewById<Button>(Resource.Id.AddAsset);

            addAsset.Click += delegate {
                Intent myIntent = new Intent(this, typeof(PortfolioActivity));
                myIntent.PutExtra("aname", assetNameText.Text);
                myIntent.PutExtra("aprice", assetPriceText.Text);
                myIntent.PutExtra("anum", assetNumberText.Text);
                SetResult(Result.Ok, myIntent);
                Finish();
            };
        }
    }
}