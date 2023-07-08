using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Constructs;

namespace Infrastructure
{
    public class LambdaStack : Stack
    {
        internal LambdaStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            new Function(this, $"{id}-Lambda", new FunctionProps
            {
                FunctionName = $"{id}-Lambda",
                Handler = "Lambda::Lambda.Function::FunctionHandler",
                Code = new AssetCode(@"../Lambda/bin/Release/net6.0/Lambda.zip"),
                Runtime = Runtime.DOTNET_6,
                Timeout = Duration.Seconds(30),
            });
        }
    }
}
