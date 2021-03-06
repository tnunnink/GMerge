using System;
using System.Globalization;
using System.Windows.Controls;
using GClient.Core.Converters;
using GClient.Core.Utilities;
using GClient.Data.Entities;

namespace GClient.Converters
{
    public class ResourceTypeIconConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ResourceType resourceType)
                throw new InvalidOperationException("Value must be of type ResourceType");

            return resourceType switch
            {
                ResourceType.Connection => ResourceFinder.Find<ControlTemplate>("Icon.Connection"),
                ResourceType.Archive => ResourceFinder.Find<ControlTemplate>("Icon.Connection"),
                ResourceType.Directory => ResourceFinder.Find<ControlTemplate>("Icon.Directory"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}