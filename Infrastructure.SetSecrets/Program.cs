using System.Diagnostics;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

var cdkOutputsFileContent = File.ReadAllText("cdk-outputs.json");
var cdkOutputs = JsonConvert.DeserializeObject(cdkOutputsFileContent) as dynamic;

var topSecretArn = cdkOutputs["AwsCdkCsharp-Secrets-Stack"]["TopSecretArn"];

Console.WriteLine($"Setting secret for ARN {topSecretArn}");

var credentials = LoadSsoCredentials("sandbox");
var client = new AmazonSecretsManagerClient(credentials, RegionEndpoint.APSoutheast2);
await client.UpdateSecretAsync(new UpdateSecretRequest
{
    SecretId = topSecretArn,
    SecretString = "Please don't tell anyone"
});

Console.WriteLine("Set secrets done");


static AWSCredentials LoadSsoCredentials(string profile)
{
    var chain = new CredentialProfileStoreChain();
    if (!chain.TryGetAWSCredentials(profile, out var credentials))
        throw new Exception($"Failed to find the {profile} profile");

    var ssoCredentials = credentials as SSOAWSCredentials;

    ssoCredentials.Options.ClientName = "Set-Secrets-Local";
    ssoCredentials.Options.SsoVerificationCallback = args =>
    {
        // Launch a browser window that prompts the SSO user to complete an SSO login.
        //  This method is only invoked if the session doesn't already have a valid SSO token.
        // NOTE: Process.Start might not support launching a browser on macOS or Linux. If not,
        //       use an appropriate mechanism on those systems instead.
        Process.Start(new ProcessStartInfo
        {
            FileName = args.VerificationUriComplete,
            UseShellExecute = true
        });
    };

    return ssoCredentials;
}