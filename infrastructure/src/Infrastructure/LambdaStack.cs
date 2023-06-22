using Amazon.CDK;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.CDK.AWS.Lambda;
using Constructs;
using System.Collections.Generic;

namespace Infrastructure
{
    public class LambdaStack : Stack
    {
        internal LambdaStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var lambda = new Function(this, "HelloWorldLambda", new FunctionProps
            {
                Handler = "HelloWorld::HelloWorld.Function::FunctionHandler",
                Code = new AssetCode(@"../HelloWorld/src/HelloWorld/bin/Release/net6.0/HelloWorld.zip"),
                Runtime = Runtime.DOTNET_6,
            });
        }
    }
}
