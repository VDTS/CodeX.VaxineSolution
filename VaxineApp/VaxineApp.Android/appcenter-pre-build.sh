#!/usr/bin/env bash
#
# For Xamarin, change some constants located in some class of the app.
# In this sample, suppose we have an AppConstant.cs class in shared folder with follow content:
#
# namespace Core
# {
#     public class AppConstant
#     {
#         public const string FirebaseApiKey = "https://CMS_MyApp-Eur01.com/api";
#     }
# }
# 
# Suppose in our project exists two branches: master and develop. 
# We can release app for production API in master branch and app for test API in develop branch. 
# We just need configure this behaviour with environment variable in each branch :)
# 
# The same thing can be perform with any class of the app.
#
# AN IMPORTANT THING: FOR THIS SAMPLE YOU NEED DECLARE API_URL ENVIRONMENT VARIABLE IN APP CENTER BUILD CONFIGURATION.

if [ -z "$FIREBASE_API_KEY" ]
then
    echo "You need define the API_URL variable in App Center"
    exit
fi

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/DataAccessLib/Constants.cs

if [ -e "$APP_CONSTANT_FILE" ]
then
    echo "Updating FirebaseApiKey to $FIREBASE_API_KEY in Constants.cs"
    sed -i.bak 's#FirebaseApiKey = "[-A-Za-z0-9:_./]*"#FirebaseApiKey = "'$FIREBASE_API_KEY'"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi

if [ -e "$APP_CONSTANT_FILE" ]
then
    echo "Updating FirebaseBaseUrl to $FIREBASE_API_URL in Constants.cs"
    sed -i.bak 's#FirebaseBaseUrl = "[-A-Za-z0-9:_./]*"#FirebaseBaseUrl = "'$FIREBASE_API_URL'"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi

VAXINE_APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/VaxineApp/VaxineApp/Constants.cs

if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating GithubApiKeyForCreatingIssues to $GITHUB_API_KEY in Constants.cs"
    sed -i.bak 's#GithubApiKeyForCreatingIssues = "[-A-Za-z0-9:_./]*"#GithubApiKeyForCreatingIssues = "'$GITHUB_API_KEY'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi


if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating SyncFusionCommunityLicenseKey to $SyncFusionCommunityLicenseKey in Constants.cs"
    sed -i.bak 's#SyncFusionCommunityLicenseKey = "[-A-Za-z0-9:_./]*"#SyncFusionCommunityLicenseKey = "'$SyncFusionCommunityLicenseKey'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi


# Support for multiple Apps (Stable and Dev)

ANDROID_MANIFEST=$APPCENTER_SOURCE_DIRECTORY/VaxineApp/VaxineApp.Android/Properties/AndroidManifest.xml

# Changing App Package Name

if [ $APPCENTER_BRANCH == "pre-release" ]
then

    echo "You are on pre-release branch, and will apply pre-release branch configurations"
    sed -i.bak 's#package="[^"]*"#package="'$Package'"#' $ANDROID_MANIFEST
    cat $ANDROID_MANIFEST
fi

# Changing App Icon
if [ $APPCENTER_BRANCH == "pre-release" ]
then

    echo "You are on pre-release branch, and will apply main pre-release configurations"
    sed -i.bak 's#android:icon="@[-A-Za-z0-9:_./]*"#android:icon="'$Icon'"#' $ANDROID_MANIFEST
    cat $ANDROID_MANIFEST
fi

# Changing App Name

ANDROID_MAIN_ACTIVITY=$APPCENTER_SOURCE_DIRECTORY/VaxineApp/VaxineApp.Android/SplashActivity.cs

if [ $APPCENTER_BRANCH == "pre-release" ]
then

    echo "You are on pre-release branch, and will apply pre-release branch configurations"
    sed -i.bak 's#Label = "[-A-Za-z0-9:_./]*"#Label = "'$Label'"#' $ANDROID_MAIN_ACTIVITY
    cat $ANDROID_MAIN_ACTIVITY
fi