using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Graphics.Drawables;
using Android.Service.QuickSettings;
using Android.Views;
using Java.Lang;

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