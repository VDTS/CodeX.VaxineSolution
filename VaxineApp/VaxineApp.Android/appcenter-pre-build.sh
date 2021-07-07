#!/usr/bin/env bash

if [ "$APPCENTER_BRANCH" == "main"];
then

/usr/libexec/plistbuddy -c "Set :CFBundleDisplayName VDTSXamarin.$APPCENTER_BRANCH" "droid/AndroidManifest"

fi

# Changing the variables in VaxineApp
sed -i '' -e "s/\[GITHUB_API_KEY]/$VSAC_GITHUB_API_KEY/g" ../VaxineApp/Constants.cs;

# Changing the variables in DataAccessLib
sed -i '' -e "s/\[FB_API_KEY]/$VSAC_FB_API_KEY/g; s/\[FB_URL]/$VSAC_FB_URL/g" ../../DataAccessLib/Constants.cs;