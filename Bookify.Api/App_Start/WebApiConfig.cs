﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Bookify.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();



            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

	        config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
		        = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
		}
    }
}
