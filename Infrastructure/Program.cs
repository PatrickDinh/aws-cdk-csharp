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
            new LambdaStack(app, "AwsCdkCSharp-Lambda-Stack-Step-1");
            app.Synth();
        }
    }
}
