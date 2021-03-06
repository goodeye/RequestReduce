﻿using System;
using System.Configuration;
using System.Text;
using RequestReduce.Utilities;
using Xunit;
using RequestReduce.ResourceTypes;

namespace RequestReduce.Facts.Utilities
{
    public class WebClientWrapperFacts
    {
        public class DownloadString
        {
            [Fact]
            public void WillNotIncludeUtf8PreambleInstring()
            {
                if (ConfigurationManager.AppSettings["Environment"] == "Test")
                    return;

                var wrapper = new WebClientWrapper();

                var result = wrapper.DownloadString("http://localhost:8877/styles/style1.css");

                Assert.NotEqual(Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble())[0],result[0]);
            }

            [Fact]
            public void WillThrowErrorIfNotCss()
            {
                if (ConfigurationManager.AppSettings["Environment"] == "Test")
                    return;

                var wrapper = new WebClientWrapper();

                var ex = Assert.Throws<InvalidOperationException>(() => wrapper.Download<CssResource>("http://localhost:8877/local.html"));

                Assert.NotNull(ex);
            }

            [Fact]
            public void WillThrowErrorIfNotJavaScript()
            {
                if (ConfigurationManager.AppSettings["Environment"] == "Test")
                    return;

                var wrapper = new WebClientWrapper();

                var ex = Assert.Throws<InvalidOperationException>(() => wrapper.Download<JavaScriptResource>("http://localhost:8877/local.html"));

                Assert.NotNull(ex);
            }

        }
    }
}
