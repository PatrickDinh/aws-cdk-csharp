using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SecretsManager;
using Constructs;

namespace Infrastructure
{
    public class SecretsStack : Stack
    {
        public Secret TopSecret;

        internal SecretsStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            TopSecret = new Secret(this, $"{id}-TopSecret");
            
            new CfnOutput(this, $"{id}-TopSecretArn", new CfnOutputProps
            {
                ExportName = $"{id}-TopSecretArn",
                Value = TopSecret.SecretArn
            });
        }
    }
}