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
            var lambdaStack = new LambdaStack(app, "AwsCdkCsharp-Lambda-Stack");
            app.Synth();
        }
    }
}
