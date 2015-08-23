﻿using System;
using System.Linq;
using Specify.Configuration;
using Specify.lib;
using Specify.Logging;
using TestStack.BDDfy.Configuration;

namespace Specify
{
    internal static class Host
    {
        private static readonly SpecifyBootstrapper _configuration;
        private static readonly IApplicationContainer applicationContainer;
        private static readonly TestRunner _testRunner;

        public static void Specify(IScenario testObject, string scenarioTitle = null)
        {
            _testRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
            _configuration = Configure();
            applicationContainer = _configuration.CreateApplicationContainer();
       
            _testRunner = new TestRunner(_configuration, applicationContainer,new BddfyTestEngine());
            _testRunner.LogSpecifyConfiguration();
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _configuration.PerAppDomainActions.ForEach(action => action.Before(applicationContainer));
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            "Host".Log().DebugFormat("Specify - DomainUnload");
            foreach (var action in _configuration.PerAppDomainActions.AsEnumerable().Reverse())
            {
                "Host".Log().DebugFormat("{0} After", action.GetType().Name);
                action.After();
            }
            applicationContainer.Dispose();
        }

        static SpecifyBootstrapper Configure()
        {
            var customConvention = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .FirstOrDefault(type => typeof(SpecifyBootstrapper).IsAssignableFrom(type) && type.IsClass);
            var config = customConvention != null
                ? (SpecifyBootstrapper)Activator.CreateInstance(customConvention)
                : new SpecifyBootstrapper();

            Configurator.Scanners.StoryMetadataScanner = () => new SpecifyStoryMetadataScanner();
            "Host".Log().Debug("Added SpecifyStoryMetadataScanner to BDDfy pipeline.");
            
            if (config.LoggingEnabled)
            {
                Configurator.Processors.Add(() => new LoggingProcessor());
            }

            return config;
        }
    }
}