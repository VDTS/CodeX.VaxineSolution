#!/usr/bin/env bash

if [ "$APPCENTER_BRANCH" == "main"];
then

/usr/libexec/plistbuddy -c "Set :CFBundleDisplayName VDTSXamarin.$APPCENTER_BRANCH" "droid/AndroidManifest"

fi

# Changing the variables in VaxineApp
sed -i -e "s/\[GITHUB_API_KEY]/$VSAC_GITHUB_API_KEY/g" -e "s/\[FB_API_KEY]/$VSAC_FB_API_KEY/g; s/\[FB_URL]/$VSAC_FB_URL/g" ../VaxineApp/Constants.cs ../../DataAccessLib/Constants.cs
