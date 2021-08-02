#!/usr/bin/env bash

APP_FILE=$APPCENTER_SOURCE_DIRECTORY/VaxineApp/VaxineApp/App.xaml.cs

if [ -e "$APP_FILE" ]
then
    echo "Updating App center keys in App.xaml.cs"
    sed -i.bak 's#android=[AndroidAppCenterKey];"#android='$AndroidAppCenterKey';"#' $APP_FILE

    echo "File content:"
    cat $APP_FILE
fi


