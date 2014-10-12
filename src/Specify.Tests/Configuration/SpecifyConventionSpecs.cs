﻿using FluentAssertions;
using NUnit.Framework;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    public class SpecifyConventionSpecs
    {
        [Test]
        public void should_provide_default_conventions()
        {
            var sut = new SpecifyConventions();
            sut.HtmlReport.Header.Should().Be("Specify");
            sut.HtmlReport.Name.Should().Be("SpecifySpecs.html");
            sut.HtmlReport.Type.Should().Be(HtmlReportConfiguration.ReportType.Html);
        }

        [Test]
        public void should_be_able_to_provide_custom_conventions()
        {
            var sut = new CustomConventions();
            sut.HtmlReport.Description.Should().Be("Description1");
            sut.HtmlReport.Header.Should().Be("Header1");
            sut.HtmlReport.Name.Should().Be("Name1.html");
            sut.HtmlReport.Type.Should().Be(HtmlReportConfiguration.ReportType.Metro);
        }

        private class CustomConventions : SpecifyConventions
        {
            public CustomConventions()
            {
                HtmlReport = new HtmlReportConfiguration()
                {
                    Description = "Description1",
                    Header = "Header1",
                    Name = "Name1.html",
                    Type = HtmlReportConfiguration.ReportType.Metro
                };
            }
        }
    }
}
