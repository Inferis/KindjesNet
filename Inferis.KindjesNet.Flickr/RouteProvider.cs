﻿using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Mvc;

namespace Inferis.KindjesNet.Flickr
{
    [Export(typeof(IRouteProvider))]
    public class RouteProvider : DefaultRouteProvider
    {
        public RouteProvider() : base("Flickr")
        {
            
        }

        public override void MapAdditionalRoutes(RouteCollection routes)
        {
        }
    }
}