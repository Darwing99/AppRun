using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android.Factories;
using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;

namespace AppRun.Droid
{
    public sealed class CachingNativeBitmapDescriptorFactory : IBitmapDescriptorFactory
    {
        private readonly ConcurrentDictionary<string, AndroidBitmapDescriptor> _cache
            = new ConcurrentDictionary<string, AndroidBitmapDescriptor>();

        public AndroidBitmapDescriptor ToNative(BitmapDescriptor descriptor)
        {
            var defaultFactory = DefaultBitmapDescriptorFactory.Instance;

            if (!string.IsNullOrEmpty(descriptor.Id))
            {
                var cacheEntry = _cache.GetOrAdd(descriptor.Id, _ => defaultFactory.ToNative(descriptor));
                return cacheEntry;
            }

            return defaultFactory.ToNative(descriptor);
        }
    }
}