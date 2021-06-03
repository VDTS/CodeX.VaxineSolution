#!/usr/bin/env bash

if [ "$APPCENTER_BRANCH" == "main"];
then

/usr/libexec/plistbuddy -c "Set :CFBundleDisplayName VDTSXamarin.$APPCENTER_BRANCH" "droid/AndroidManifest"

fi

sed -i -e "s/\[VSAC_GITHUB_API_KEY]/$VSAC_GITHUB_API_KEY/g" SecretsVault.cs