using Android.App;
using Android.Content;
using Android.Service.QuickSettings;

namespace VaxineApp.Droid.Services
{
    [Service(Name = "com.codex.vaxineapp.VaxineAppService",
             Permission = Android.Manifest.Permission.BindQuickSettingsTile,
             Label = "@string/tile_name",
             Icon = "@drawable/AppIcon")]
    [IntentFilter(new[] { ActionQsTile })]
    public class VaxineAppService : TileService
    {
        // More on https://devblogs.microsoft.com/xamarin/android-nougat-quick-setting-tiles/
    }
}