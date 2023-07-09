using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.JSII.Runtime.Deputy;
using Constructs;

namespace Infrastructure
{
    public class LambdaStack : Stack
    {
        internal LambdaStack(Construct scope, string id, LambdaStackProps props) : base(scope, id, props)
        {
            var assetCodePath = System.Environment.GetEnvironmentVariable("ASSET_CODE_PATH");

            var lambda = new Function(this, $"{id}-Lambda", new FunctionProps
            {
                FunctionName = $"{id}-Lambda",
                Handler = "Lambda::Lambda.Function::FunctionHandler",
                Code = new AssetCode(assetCodePath ?? @"../Lambda/bin/Release/net6.0/Lambda.zip"),
                Runtime = Runtime.DOTNET_6,
                Timeout = Duration.Seconds(30),
                Environment = new Dictionary<string, string>
                {
                    { "TOP_SECRET_ARN", props.TopSecret.SecretFullArn }
                }
            });
            props.TopSecret.GrantRead(lambda);
        }
    }

    public class LambdaStackProps : DeputyBase, IStackProps
    {
        public Secret TopSecret { get; set; }
    }
}