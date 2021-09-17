#!/usr/bin/env bash


VAXINE_APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/src/VaxineApp/VaxineApp/Constants.cs

if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating AppCenterAndroidXamarinKeyPlaceholder to $AndroidAppCenterKey in Constants.cs"
    sed -i.bak 's#AppCenterAndroidXamarinKey = "[-A-Za-z0-9:_./]*"#AppCenterAndroidXamarinKey = "'$AndroidAppCenterKey'"#' $VAXINE_APP_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_APP_CONSTANT_FILE
fi

VAXINE_ANDROID_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/src/VaxineApp/VaxineApp.Android/AppConstants.cs

if [ -e "$VAXINE_APP_CONSTANT_FILE" ]
then
    echo "Updating AppCenterAndroidXamarinKeyPlaceholder to $AndroidAppCenterKey in Constants.cs"
    sed -i.bak 's#AppCenterAndroidXamarinKey = "[-A-Za-z0-9:_./]*"#AppCenterAndroidXamarinKey = "'$AndroidAppCenterKey'"#' $VAXINE_ANDROID_CONSTANT_FILE

    echo "File content:"
    cat $VAXINE_ANDROID_CONSTANT_FILE
fi