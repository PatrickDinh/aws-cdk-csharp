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

var credentials = LoadSsoCredentials("default");
var client = new AmazonSecretsManagerClient(credentials, RegionEndpoint.APSoutheast2);
await client.UpdateSecretAsync(new UpdateSecretRequest
{
    SecretId = topSecretArn,
    SecretString = "Please don't tell anyone!"
});

Console.WriteLine("Set secrets done");


static AWSCredentials LoadSsoCredentials(string profile)
{
    var chain = new CredentialProfileStoreChain();
    if (!chain.TryGetAWSCredentials(profile, out var credentials))
        throw new Exception($"Failed to find the {profile} profile");

    var ssoCredentials = credentials as SSOAWSCredentials;

    if (ssoCredentials == null)
    {
        throw new Exception($"Cannot load credentials for ${profile}, try aws sso login command first");
    }

    return ssoCredentials;
}