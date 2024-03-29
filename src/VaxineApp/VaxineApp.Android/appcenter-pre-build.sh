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
# App Center Certificate : https://help.gamesalad.com/gamesalad-cookbook/publishing/4-android-publishing/4-02-creating-a-keystore/#:~:text=%20To%20create%20a%20Keystore%3A%20%201%20Open,Keystore%2C%20then%20answer%20a%20series%20of...%20More%20

if [ -z "$FIREBASE_API_KEY" ]
then
    echo "You need define the API_URL variable in App Center"
    exit
fi

VAXINE_APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/src/VaxineApp/VaxineApp/Constants.cs

if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating FirebaseApiKey to $FIREBASE_API_KEY in Constants.cs"
    sed -i.bak 's#FirebaseApiKey = "[-A-Za-z0-9:_./]*"#FirebaseApiKey = "'$FIREBASE_API_KEY'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi

if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating FirebaseBaseUrl to $FIREBASE_API_URL in Constants.cs"
    sed -i.bak 's#FirebaseBaseUrl = "[-A-Za-z0-9:_./]*"#FirebaseBaseUrl = "'$FIREBASE_API_URL'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi


# Firebase Private key Constants

if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating AuthProviderX509CertUrlPlaceholder to $Auth_Provider_X509_Cert_Url in Constants.cs"
    sed -i.bak 's#AuthProviderX509CertUrl = "[-A-Za-z0-9:_./]*"#AuthProviderX509CertUrl = "'$Auth_Provider_X509_Cert_Url'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi

if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating AuthUri to $Auth_Uri in Constants.cs"
    sed -i.bak 's#AuthUri = "[-A-Za-z0-9:_./]*"#AuthUri = "'$Auth_Uri'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating ClientEmail to $Client_Email in Constants.cs"
    sed -i.bak 's#ClientEmail = "[-A-Za-z0-9:_./]*"#ClientEmail = "'$Client_Email'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating ClientId to $Client_Id in Constants.cs"
    sed -i.bak 's#ClientId = "[-A-Za-z0-9:_./]*"#ClientId = "'$Client_Id'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating ClientX509CertUrl to $Client_X509_Cert_Url in Constants.cs"
    sed -i.bak 's#ClientX509CertUrl = "[-A-Za-z0-9:_./]*"#ClientX509CertUrl = "'$Client_X509_Cert_Url'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating PrivateKey to $Private_Key in Constants.cs"
    sed -i.bak 's@PrivateKeyPlaceholder@'"$Private_Key"'@' $VAXINE_APP_CONSTANT_FILE
    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating PrivateKeyId to $Private_KeyId in Constants.cs"
    sed -i.bak 's#PrivateKeyId = "[-A-Za-z0-9:_./]*"#PrivateKeyId = "'$Private_KeyId'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating ProjectId to $Project_Id in Constants.cs"
    sed -i.bak 's#ProjectId = "[-A-Za-z0-9:_./]*"#ProjectId = "'$Project_Id'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating TokenUri to $Token_Uri in Constants.cs"
    sed -i.bak 's#TokenUri = "[-A-Za-z0-9:_./]*"#TokenUri = "'$Token_Uri'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi
if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating Type to $Type in Constants.cs"
    sed -i.bak 's#Type = "[-A-Za-z0-9:_./]*"#Type = "'$Type'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi


# End Firebase Private key Constants


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

ANDROID_MANIFEST=$APPCENTER_SOURCE_DIRECTORY/src/VaxineApp/VaxineApp.Android/Properties/AndroidManifest.xml

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

VAXINE_ANDROID_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/src/VaxineApp/VaxineApp.Android/AppConstants.cs

if [ $APPCENTER_BRANCH == "pre-release" ]
then

    echo "You are on pre-release branch, and will apply pre-release branch configurations"
    sed -i.bak 's#AppName = "[-A-Za-z0-9:_./]*"#AppName = "'$Label'"#' $VAXINE_ANDROID_CONSTANT_FILE
    cat $VAXINE_ANDROID_CONSTANT_FILE
fi

if [ $APPCENTER_BRANCH == "main" ]
then

    echo "You are on pre-release branch, and will apply pre-release branch configurations"
    sed -i.bak 's#AppName = "[-A-Za-z0-9:_./]*"#AppName = "'$Label'"#' $VAXINE_ANDROID_CONSTANT_FILE
    cat $VAXINE_ANDROID_CONSTANT_FILE
fi