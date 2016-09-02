﻿//
// Copyright 2015 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteelToe.CloudFoundry.Connector.OAuth;
using System;


namespace SteelToe.Security.Authentication.CloudFoundry.Test
{
    public class TestServerStartup
    {

        public static CloudFoundryOptions CloudFoundryOptions { get; set; }

        public static OAuthServiceOptions ServiceOptions { get; set; }

        public TestServerStartup()
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddAuthentication(options => 
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            if (ServiceOptions != null)
            {
                services.AddSingleton(typeof(IOptions<OAuthServiceOptions>), Options.Create(ServiceOptions));
            }

        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddConsole(LogLevel.Debug);

            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    if (context.Response.HasStarted)
                    {
                        throw;
                    }
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(ex.ToString());
                }
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true

            });
            if (CloudFoundryOptions != null)
                app.UseCloudFoundryAuthentication(CloudFoundryOptions);
            else
                app.UseCloudFoundryAuthentication();


            app.Run(async context =>
            {
                await context.Authentication.ChallengeAsync(CloudFoundryOptions.AUTHENTICATION_SCHEME);
                return;

            });

        }

    }

}
