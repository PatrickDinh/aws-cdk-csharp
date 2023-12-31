using Amazon.Lambda.Core;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda;

public class Function
{
    public async Task<string> FunctionHandler(string input)
    {
        var topSecretValue = await GetTopSecret();
        return $"{input.ToUpper()} - {topSecretValue}";
    }
    
    private async Task<string> GetTopSecret()
    {
        var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        if (environmentName != "Development")
        {
            var topSecretArn = Environment.GetEnvironmentVariable("TOP_SECRET_ARN");
            var client = new AmazonSecretsManagerClient();
            var secret = await client.GetSecretValueAsync(new GetSecretValueRequest
            {
                SecretId = topSecretArn,
            });
            
            return secret.SecretString;
        }

        return ":)";
    }
}
