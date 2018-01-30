using Android.App;
using Android.Widget;
using Android.OS;
using Android;

namespace Phoneword {
    [Activity(Label = "Phone Word", MainLauncher = true)]
    public class MainActivity : Activity{
        protected override void OnCreate(Bundle bundle){
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.TranslatedPhoneWord);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);

            translateButton.Click += (sender, e) => {
                // Translate user’s alphanumeric phone number to numeric
                string translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber)) {
                    translatedPhoneWord.Text = string.Empty;
                }
                else {
                    translatedPhoneWord.Text = translatedNumber;
                }
            };
        }
    }
}

