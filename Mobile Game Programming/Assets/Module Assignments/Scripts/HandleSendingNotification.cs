using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using NotificationSamples.Android;
#elif UNITY_IOS
using NotificationSamples.iOS;
#endif

public class HandleSendingNotification : MonoBehaviour
{
    public bool isSent;
    // Start is called before the first frame update
    void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    // Update is called once per frame
    void Update()
    {
        isSent = false;
    }
    void OnApplicationPause(bool pause)
    {
        if (pause && !isSent)
        {
            SendNotificationOnSleep();
            isSent = true;
        }
    }
    void SendNotificationOnSleep()
    {
        var notification = new AndroidNotification();
        notification.Title = "The game misses you!";
        notification.Text = "Come back please!";
        notification.FireTime = System.DateTime.Now.AddMinutes(0.5);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
    public void SendNotificationOnButton()
    {
        var notification = new AndroidNotification();
        notification.Title = "You clicked the fanciest button!";
        notification.Text = "Felt like ages ago, didn't it?";
        notification.FireTime = System.DateTime.Now.AddMinutes(1);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
