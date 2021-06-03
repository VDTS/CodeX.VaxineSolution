if [ -z "$GITHUB_API_KEY_FOR_CREATING_ISSUES" ]
then
    echo "You need define the GITHUB_API_KEY_FOR_CREATING_ISSUES variable in App Center"
    exit
fi

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/VaxineApp/SecretsVault.cs

if [ -e "$APP_CONSTANT_FILE" ]
then
    echo "Updating GithubApiKeyForCreatingIssues to $GITHUB_API_KEY_FOR_CREATING_ISSUES in SecretsVault.cs"
    sed -i '' 's#GithubApiKeyForCreatingIssues = "[-A-Za-z0-9:_./]*"#GithubApiKeyForCreatingIssues = "'$GITHUB_API_KEY_FOR_CREATING_ISSUES'"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi
