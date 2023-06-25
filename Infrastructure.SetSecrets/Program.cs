using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

var cdkOutputsFileContent = File.ReadAllText("cdk-outputs.json");
var cdkOutputs = JsonConvert.DeserializeObject(cdkOutputsFileContent) as dynamic;

var topSecretArn = cdkOutputs["AwsCdkCsharp-Secrets-Stack"]["TopSecretArn"];

Console.WriteLine($"Setting secret for ARN {topSecretArn}");

var client = new AmazonSecretsManagerClient(RegionEndpoint.APSoutheast2);
await client.UpdateSecretAsync(new UpdateSecretRequest
{
    SecretId = topSecretArn,
    SecretString = "Please don't tell anyone"
});

Console.WriteLine("Set secrets done");