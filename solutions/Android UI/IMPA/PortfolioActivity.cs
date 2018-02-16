using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;


namespace IMPA {
    [Activity(Label = "PortfolioActivity")]
    public class PortfolioActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Portfolio);
            var pName = FindViewById<TextView>(Resource.Id.PortfolioName);
            Button newAsset = FindViewById<Button>(Resource.Id.newAsset);

            pName.Text = Intent.GetStringExtra("text") ?? "Portfolio Name Not Found";

            newAsset.Click += delegate {
                NewAssetClick();
            };
        }
        public void NewAssetClick() {
            var i = new Intent(this, typeof(InfoAssetActivity));
            StartActivityForResult(i, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok) {
                Button newAsset = new Button(this) {
                    Text = data.GetStringExtra("aname"),                  
                };
           
                LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.AssetLayout);
                ll.AddView(newAsset);
                var n = data.GetStringExtra("aname");
                var o = data.GetStringExtra("anum");
                var p = data.GetStringExtra("aprice");

                newAsset.Click += delegate {
                    AssetClicked(n, o, p);
                };
            }
        }
        public void AssetClicked(string n, string o, string p) {
            Intent myIntent = new Intent(this, typeof(AssetActivity));
            myIntent.PutExtra("AssetName", n);
            myIntent.PutExtra("AssetNumber", o);
            myIntent.PutExtra("AssetPrice", p);
            StartActivity(myIntent);
        }
    }
}