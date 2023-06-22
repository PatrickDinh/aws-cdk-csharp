using Amazon.CDK;
using Amazon.CDK.AWS.SecretsManager;
using Constructs;

namespace Infrastructure
{
    public class SecretsStack : Stack
    {
        public Secret TopSecret;

        internal SecretsStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            TopSecret = new Secret(this, "ThisIsTopSecret", new SecretProps() {
                SecretStringValue = new SecretValue("Do not tell anyone")
            });
        }
    }
}
