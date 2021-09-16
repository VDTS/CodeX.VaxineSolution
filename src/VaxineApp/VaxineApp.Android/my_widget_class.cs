using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using System;

namespace VaxineApp.Droid
{
    [BroadcastReceiver(Label = "Widget Button Click")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [IntentFilter(new string[] { "com.codex.vaxineapp.WidgetButtonClick.ACTION_WIDGET_TURNON" })]
    [IntentFilter(new string[] { "com.codex.vaxineapp.WidgetButtonClick.ACTION_WIDGET_TURNOFF" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/my_widget_provider")]
    public class my_widget_class : AppWidgetProvider
    {
        public static String ACTION_WIDGET_TURNON = "Button 1 click";
        public static String ACTION_WIDGET_TURNOFF = "Button 2 clickedddddd";
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            //Update Widget layout
            //Run when create widget or meet update time
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(my_widget_class)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));
        }

        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            //Build widget layout
            var widgetView = new RemoteViews(context.PackageName, Resource.Layout.my_widget);

            //Change text of element on Widget
            SetTextViewText(widgetView);

            //Handle click event of button on Widget
            RegisterClicks(context, appWidgetIds, widgetView);

            return widgetView;
        }

        private void SetTextViewText(RemoteViews widgetView)
        {
            widgetView.SetTextViewText(Resource.Id.textView1, "HelloAppWidget");
        }

        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            var intent = new Intent(context, typeof(my_widget_class));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

            //Button 1
            widgetView.SetOnClickPendingIntent(Resource.Id.buttonHi, GetPendingSelfIntent(context, ACTION_WIDGET_TURNOFF));

            //Button 2
            widgetView.SetOnClickPendingIntent(Resource.Id.buttonHello, GetPendingSelfIntent(context, ACTION_WIDGET_TURNON));
        }

        private PendingIntent GetPendingSelfIntent(Context context, string action)
        {
            var intent = new Intent(context, typeof(my_widget_class));
            intent.SetAction(action);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            // Check if the click is from the "ACTION_WIDGET_TURNOFF or ACTION_WIDGET_TURNON" button
            if (ACTION_WIDGET_TURNOFF.Equals(intent.Action))
            {
                Toast.MakeText(context, "Show me the button 1", ToastLength.Short).Show();
            }
            if (ACTION_WIDGET_TURNON.Equals(intent.Action))
            {
                Toast.MakeText(context, "Hello button 2", ToastLength.Short).Show();
            }

        }
    }
}