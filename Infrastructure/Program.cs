using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var secretsStack = new SecretsStack(app, "AwsCdkCSharp-Secrets-Stack-Step-3");
            new LambdaStack(app, "AwsCdkCSharp-Lambda-Stack-Step-3", new LambdaStackProps
            {
                TopSecret = secretsStack.TopSecret
            });
            app.Synth();
        }
    }
}