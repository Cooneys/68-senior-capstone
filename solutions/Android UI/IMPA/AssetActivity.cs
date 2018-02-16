using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;


namespace IMPA {
    [Activity(Label = "AssetActivity")]
    public class AssetActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Asset);
            var aName = FindViewById<TextView>(Resource.Id.AssetName);
            var aPrice = FindViewById<TextView>(Resource.Id.AvgPurchasePrice);
            var aNum = FindViewById<TextView>(Resource.Id.AmountOwned);

            aName.Text = (Intent.GetStringExtra("AssetName") + ": *missing ticket*") ?? "Asset Name Not Found";
            aPrice.Text = ("Average Purchase Price: " + Intent.GetStringExtra("AssetPrice")) ?? "Asset Price Not Found";
            aNum.Text = ("Amount Owned: " + Intent.GetStringExtra("AssetNumber")) ?? "Amount Owned Not Found";

        }
    }
}